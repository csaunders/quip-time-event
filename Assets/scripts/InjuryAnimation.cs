using UnityEngine;
using System.Collections;

public class InjuryAnimation : MonoBehaviour {
	public AnimationCurve curveX;
	public int StartX, Direction, Width;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posn = transform.position;
		float delta = curveX.Evaluate (Time.time);
		float newPosition = Width * Direction * (1.0f - delta);
//		posn.x = newPosition > EndX ? newPosition : EndX;
		posn.x = newPosition;
		transform.position = posn;
	}

	public void SetCurves(AnimationCurve cX) {
		curveX = cX;
	}
}
