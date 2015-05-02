using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuipSystem : MonoBehaviour {

	public static QuipSystem GodObject;
	
	public Text quipPlayer;
	public Text quipGuide;
	public Text successText;
	public Text debugLog;
	public float characterRenderSpeed = 0.30f;
	public float accuracyBuffer = 0.60f;
	
	private string phrase = "some peo%ple just wa%nt to wat%ch th%e world bu%rn.";
	private QuickTimeTracker tracker;
	public QuickTimeTracker Tracker { get { return tracker; } }

	// Use this for initialization
	void Start () {
		GodObject = this;
		Reset ();
	}

	public void Reset() {
		tracker = new QuickTimeTracker (phrase, characterRenderSpeed, accuracyBuffer);

		quipPlayer.text = "";
		quipGuide.text = tracker.FullMessage ();
		successText.text = "--- Nothing ---";
	}
	
	// Update is called once per frame
	void Update () {
		handleInput ();
		tracker.Update (Time.deltaTime);
		quipPlayer.text = tracker.PartialMessage();
		debugData ();
	}

	private void handleInput()
	{
		if (Input.GetKeyDown (KeyCode.R)) {
			Reset ();
		}
		if (Input.GetKeyDown (KeyCode.F)) {
		}
	}

	private void debugData()
	{
		debugLog.text = "";
		debugLog.text += string.Format ("Timer: {0}\n", tracker.Timer);
		debugLog.text += string.Format ("Closest Time: {0}\n", tracker.ClosestTime);
		debugLog.text += string.Format ("Score: {0}\n", tracker.Score());
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

		Vector2 sizeOfPercentSymbol = style.CalcSize (new GUIContent ("%"));
		Vector3 start = new Vector3 (position.x + rect.x + size.x - (sizeOfPercentSymbol.x / 2), position.y - 10, 100);
		Vector3 end = new Vector3(position.x + rect.x + size.x - (sizeOfPercentSymbol.x / 2), position.y + 10, 100);
		Debug.DrawLine(start, end, Color.white);
	}
}
