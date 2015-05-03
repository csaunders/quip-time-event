using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuipSystem : MonoBehaviour {

	public static QuipSystem GodObject;
	
	public Text quipPlayer;
	public Text quipGuide;
	public Text successText;
	public Text debugLog;
	public float deltaModifier = 10.0f;
	public float characterRenderSpeed = 0.30f;
	public float accuracyBuffer = 0.60f;

	public string EventAButton, EventBButton, EventXButton, EventYButton;
	public GameObject AButton, BButton, XButton, YButton;
	public Canvas ButtonOverlay;
	
	private string phrase = "some peo%ple just wa%nt to wat%ch th%e world bu%rn.";
	private QuickTimeTracker tracker;
	public QuickTimeTracker Tracker { get { return tracker; } }

	private Hashtable lookup = new Hashtable();
	private Hashtable eventsForPrefab = new Hashtable();

	public class KeyValuePair
	{
		public string Key { get; set; }
		public GameObject Value { get; set; }

		public KeyValuePair(string key, GameObject value)
		{
			Key = key;
			Value = value;
		}
	}

	// Use this for initialization
	void Start () {
		GodObject = this;
		eventsForPrefab [AButton] = EventAButton;
		eventsForPrefab [BButton] = EventBButton;
		eventsForPrefab [XButton] = EventXButton;
		eventsForPrefab [YButton] = EventYButton;
		Reset (true);
	}

	public void Reset(bool drawOverlayButtons) {
		foreach (DictionaryEntry de in lookup) {
			if (de.Value is GameObject) {
				Object.Destroy((GameObject)de.Value);
			}
		}
		lookup.Clear ();
		phrase = RandomQTEMessage ();

		tracker = new QuickTimeTracker (phrase, characterRenderSpeed, accuracyBuffer);
		if (drawOverlayButtons) {
			StartCoroutine (addOverlayButtons ());
		}

		quipPlayer.text = "";
		quipGuide.text = tracker.FullMessage ();
		successText.text = "--- Nothing ---";
	}
	
	// Update is called once per frame
	void Update () {
		handleInput ();
		tracker.Update (Time.deltaTime * deltaModifier);
		quipPlayer.text = tracker.PartialMessage();
		debugData ();
	}

	private void handleInput()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Reset (true);
			return;
		}
	}

	private void debugData()
	{
		debugLog.text = "";
		debugLog.text += string.Format ("Timer: {0}\n", tracker.Timer);
		debugLog.text += string.Format ("Closest Time: {0}\n", tracker.ClosestTime);
		debugLog.text += string.Format ("Score: {0}\n", tracker.Score(false));
		debugLog.text += string.Format ("All Times: {0}\n", tracker.AllTimes);

		foreach (string qte in tracker.QuickTimeStrings()) {
			drawCursor(qte, quipPlayer);
		}
	}

	//http://answers.unity3d.com/questions/37987/gui-text-object-width.html
	private void drawCursor(string msg, Text txt)
	{
		GUIStyle style = new GUIStyle ();
		style.font = txt.font;
		style.fontSize = txt.fontSize;
		Vector3 position = txt.transform.position;
		Rect rect = txt.rectTransform.rect;
		Vector2 size = style.CalcSize(new GUIContent(msg));

		Vector2 sizeOfPercentSymbol = style.CalcSize (new GUIContent (""));
		float positionX = position.x + rect.x + size.x - (sizeOfPercentSymbol.x / 2);
		float positionY = position.y;
		Debug.DrawLine(new Vector3(positionX, positionY + 10, 100), new Vector3(positionX, positionY - 10, 100), Color.white);
	}

	private GameObject buildButton(string msg, Text txt, GameObject prefab)
	{
		GUIStyle style = new GUIStyle ();
		style.font = txt.font;
		style.fontSize = txt.fontSize;
		Vector3 position = txt.transform.position;
		Rect rect = txt.rectTransform.rect;
		Vector2 size = style.CalcSize(new GUIContent(msg));
		
		Vector2 sizeOfPercentSymbol = style.CalcSize (new GUIContent (""));
		float positionX = position.x + rect.x + size.x - (sizeOfPercentSymbol.x / 2);
		float positionY = position.y + rect.height;
		
		GameObject go = (GameObject) Instantiate (prefab, new Vector3 (positionX, positionY, 100), Quaternion.identity);

		return go;
	}

	private IEnumerator FailedToHitButton(GameObject obj, float firesInSeconds)
	{
		Animator anim = (Animator)obj.GetComponent<Animator> ();
		yield return new WaitForSeconds (firesInSeconds/2);
		anim.SetTrigger ("Miss");
		Debug.Log ("Miss called");
		yield return 0;
	}

	private IEnumerator addOverlayButtons()
	{
		float i = 0.0f;
		foreach (QuickTimeTracker.MessageTimingPair pair in tracker.MessageTimingPairs()) {
			yield return new WaitForSeconds(i);
			GameObject[] buttons = new GameObject[]{AButton, BButton, XButton, YButton};
			GameObject prefab = buttons[Random.Range (0, buttons.Length)];
			GameObject item = buildButton(pair.Message, quipPlayer, prefab);
			item.transform.parent = quipGuide.transform;
			item.SetActive(true);
			lookup.Add (pair.Message, item);
			lookup.Add (item, eventsForPrefab[prefab]);

			i += 0.100f;
		}
		yield return 0;
	}

	public KeyValuePair NextButton {
		get {
			string msg = tracker.messageAt (tracker.ClosestTime);
			GameObject obj = (GameObject) lookup [msg];
			string button = (string) lookup[obj];
			return new KeyValuePair(button, obj);
		}
	}

	private string RandomQTEMessage()
	{
		int quipId = (int) Random.Range(0, Quiptionary.Quips.Length);
		return Quiptionary.Quips[quipId];
	}
}
