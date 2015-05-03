using UnityEngine;
using System.Collections;

public class Player : HealthSystem {
	
	private float _damage;
	private Computer _computer;
	private QuipSystem system;

	new void Start () {
		base.Start ();
//		animator.SetTrigger ("Talking");
		_damage = 0.0f;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (!IsTurn) {
			return;
		}
		updateReferences ();

		if (system.Tracker.DonePhrase) {
			animator.SetTrigger("Idle");
			_computer.InflictDamage(_damage);
			_damage = 0.0f;
			EndTurn();
			return;
		}

		QuipSystem.KeyValuePair pair = system.NextButton;
		if (Input.GetButtonDown (pair.Key)) {
			float score = system.Tracker.Score (true);
			changeAnimationState (pair.Value, score);
			_damage += score;
		}
	}

	private void updateReferences()
	{
		if (_computer != null && system != null) {
			return;
		}
		Debug.Log ("Computer is still null");
		_computer = (Computer) GameObject.Find ("Computer").GetComponent (typeof(Computer));
		system = QuipSystem.GodObject;
	}

	private void changeAnimationState(GameObject obj, float score)
	{
		Animator anim = (Animator)obj.GetComponent<Animator> ();
		if (score == 0) {
			anim.SetTrigger ("Failure");
		} else {
			anim.SetTrigger ("Success");
		}
	}

	public override void BeforeTurnStart ()
	{
		animator.SetTrigger ("Talking");
		system.Reset (true);
	}

	override public HealthSystem Other() {
		return _computer;
	}
}
