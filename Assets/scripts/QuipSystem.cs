using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuipSystem : MonoBehaviour {
	
	public Text quipPlayer;
	public Text quipGuide;
	public Text successText;
	public Text debugLog;
	public float characterRenderSpeed = 0.30f;
	public float accuracyBuffer = 0.60f;
	
	private string phrase = "some peo%ple just wa%nt to wat%ch th%e world bu%rn something something something something %something.";
	private QuickTimeTracker tracker;

	// Use this for initialization
	void Start () {
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

		//http://answers.unity3d.com/questions/37987/gui-text-object-width.html

		foreach (string qte in tracker.QuickTimeStrings()) {
			drawCursor(qte, quipPlayer);
		}
	}

	private void drawCursor(string msg, Text txt)
	{
		
		GUIStyle style = new GUIStyle ();
		style.font = txt.font;
		Vector3 position = txt.transform.localPosition;
		Rect rect = txt.rectTransform.rect;
		Vector2 size = style.CalcSize(new GUIContent(msg));
		Vector3 start = new Vector3 (rect.x + size.x, position.y - 10, 100);
		Vector3 end = new Vector3(rect.x + size.x, position.y + 10, 100);
		Debug.Log ("Size:" + size);
		Debug.Log ("Start:" + start);
		Debug.Log ("End: " + end);
		Debug.DrawLine(start, end, Color.white);
	}
}
