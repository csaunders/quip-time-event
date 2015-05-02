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
	
	private string phrase = "some people just wa%nt to watch the world bu%rn.";
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
	}
}
