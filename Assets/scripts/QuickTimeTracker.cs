// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;

public class QuickTimeTracker
{
	public static char QTE_IDENTIFIER = '%';
	public static float ACCURACY_SCORE = 300;
	public static float PERFECT_ACCURACY = 0.100f;

	private string _phrase;
	private float _charsPerS;
	private float _accuracyBuffer;
	private ArrayList _timings;

	private float _timer;
	public float Timer { get { return _timer; } }
	public QuickTimeTracker (string phrase, float charsPerS, float accuracyBuffer)
	{
		_timer = 0.0f;

		_phrase = phrase;
		_charsPerS = charsPerS;
		_accuracyBuffer = accuracyBuffer;
		_timings = calculateTimings ();
	}

	public string FullMessage()
	{
		string message = "";
		foreach (char c in _phrase) {
			if (c != QTE_IDENTIFIER || IsDebug()) {
				message += c;
			}
		}
		return message;
	}

	public string PartialMessage()
	{
		return messageAt (_timer);
	}

	public string messageAt(float time)
	{
		string message = "";
		int count = 0;
		int position = positionFor (time);
		foreach (char c in _phrase) {
			if (count > position) {
				break;
			}
			
			if (c != QTE_IDENTIFIER || IsDebug()) {
				message += c;
			}
			count++;
		}
		return message;
	}

	public void Update(float dt)
	{
		_timer += dt;
	}

	private int positionFor(float time)
	{
		return (int)(time * _charsPerS);
	}

	public float Score(bool remove)
	{
		float time = findClosestTime (_timer);
		float delta = (float) Math.Abs(time - _timer);
		if (remove) {
			_timings.Remove (time);
		}

		if (delta > _accuracyBuffer) {
			return 0.0f;
		}

		if (delta <= PERFECT_ACCURACY) {
			return ACCURACY_SCORE;
		}

		return ACCURACY_SCORE *  (1.0f - (delta / _accuracyBuffer));
	}

	public ArrayList calculateTimings()
	{
		ArrayList timings = new ArrayList ();
		for (int i = 0; i < _phrase.Length; i++) {
			if (_phrase[i] == QTE_IDENTIFIER){
				timings.Add(i / _charsPerS);
			}
		}
		return timings;
	}

	public string AllTimes {
		get {
			string times = "[";
			foreach(float time in _timings) {
				times += time;
				times += ",";
			}
			times += "]";
			return times;
		}
	}
	
	public float ClosestTime { get { return findClosestTime (_timer); } }
	private float findClosestTime(float forTime)
	{
		float closest = -1.0f;
		double delta = 10000.0f;
		foreach (float time in _timings) {
			double nextDelta = Math.Abs(time - Timer);
			if (nextDelta < delta) {
				closest = time;
				delta = nextDelta;
			}
		}
		return closest;
	}

	public bool DonePhrase {
		get {
			return positionFor (_timer) >= FullMessage ().Length;
		}
	}

	public class MessageTimingPair
	{
		public string Message { get; set; }
		public float Timing { get; set; }
		public MessageTimingPair(string msg, float time){
			Message = msg;
			Timing = time;
		}

		public string ToString()
		{
			return Message + ":" + Timing;
		}
	}

	public ArrayList MessageTimingPairs()
	{
		ArrayList mtPairs = new ArrayList ();
		foreach (float time in _timings) {
			mtPairs.Add (new QuickTimeTracker.MessageTimingPair(messageAt (time), time + (_accuracyBuffer/2)));
		}
		return mtPairs;
	}

	public ArrayList QuickTimeStrings()
	{
		ArrayList qtStrings = new ArrayList ();
		foreach (float time in _timings) {
			qtStrings.Add (messageAt (time));
		}
		return qtStrings;
	}

	private bool IsDebug() {
		return UnityEngine.Debug.isDebugBuild && false;
	}
}

