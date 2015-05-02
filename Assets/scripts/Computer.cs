using UnityEngine;
using System.Collections;

public class Computer : HealthSystem {

	// Use this for initialization
	private Player _player;
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

		inflictRandomPlayerDamage ();
		EndTurn ();
	}

	private void updateReferences()
	{
		if (_player != null)
		{
			return;
		}

		_player = (Player) GameObject.Find ("Player").GetComponent (typeof(Player));
	}

	private void inflictRandomPlayerDamage()
	{
		float damage = Random.Range(100, 200);
		_player.InflictDamage (damage);
	}

	override public HealthSystem Other() {
		return _player;
	}

	public override void BeforeTurnStart ()
	{

	}


}
