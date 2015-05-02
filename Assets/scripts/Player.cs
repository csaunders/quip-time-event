using UnityEngine;
using System.Collections;

public class Player : HealthSystem {
	
	private float _damage;
	private Computer _computer;
	private QuipSystem system;

	void Start () {
		base.Start ();
		_damage = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		updateReferences ();
		if (system == null) {
			system = QuipSystem.GodObject;
		}

		if (system.Tracker.DonePhrase) {
			_computer.InflictDamage(_damage);
			_damage = 0.0f;
		}

		if (Input.GetButtonDown(system.Tracker.NextButton))
		{
			_damage += system.Tracker.Score();
		}
	}

	private void updateReferences()
	{
		if (_computer != null) {
			return;
		}
		Debug.Log ("Computer is still null");
		_computer = (Computer) GameObject.Find ("Computer").GetComponent (typeof(Computer));
	}
}
