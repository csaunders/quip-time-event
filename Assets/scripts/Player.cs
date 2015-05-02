using UnityEngine;
using System.Collections;

public class Player : HealthSystem {
	
	private float _damage;
	private Computer _computer;
	private QuipSystem system;

	new void Start () {
		base.Start ();
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
			_computer.InflictDamage(_damage);
			_damage = 0.0f;
			EndTurn();
			return;
		}

		if (Input.GetButtonDown(system.Tracker.NextButton))
		{
			_damage += system.Tracker.Score();
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

	public override void BeforeTurnStart ()
	{
		system.Reset ();
	}

	override public HealthSystem Other() {
		return _computer;
	}
}
