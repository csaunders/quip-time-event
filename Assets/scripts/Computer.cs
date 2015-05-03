using UnityEngine;
using System.Collections;

public class Computer : HealthSystem {

	// Use this for initialization
	private Player _player;
	private QuipSystem system;
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		updateReferences ();

		if (!IsTurn) {
			return;
		}

		if (system.Tracker.DonePhrase) {
			animator.SetTrigger ("Idle");
			inflictRandomPlayerDamage();
			EndTurn ();
		}
	}

	private void updateReferences()
	{
		if (_player != null)
		{
			return;
		}

		Debug.Log ("Player is still null");

		_player = (Player) GameObject.Find ("Player").GetComponent (typeof(Player));
		system = QuipSystem.GodObject;
	}

	private void inflictRandomPlayerDamage()
	{
		float damage = Random.Range(QuickTimeTracker.ACCURACY_SCORE - 100, QuickTimeTracker.ACCURACY_SCORE + 100);
		_player.InflictDamage (damage);
	}

	override public HealthSystem Other() {
		return _player;
	}

	public override void BeforeTurnStart ()
	{
		system.Reset (false);
		animator.SetTrigger ("Talking");
	}


}
