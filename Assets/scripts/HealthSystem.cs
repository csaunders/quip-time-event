using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class HealthSystem : MonoBehaviour {

	public static float MAX_HEALTH = 2000.0f;
	public Text healthDisplay;
	public bool IsTurn;
	
	private float _currentHealth;

	// Use this for initialization
	public void Start () {
		_currentHealth = MAX_HEALTH;	
	}

	public void Update() {
		healthDisplay.text = "HP: " + _currentHealth;
	}

	public void InflictDamage(float amount)
	{
		_currentHealth -= amount;
	}

	public bool IsDead(){
		return _currentHealth <= 0.0f;
	}

	public void EndTurn() {
		Other ().IsTurn = true;
		Other ().BeforeTurnStart ();

		IsTurn = false;
	}

	public abstract HealthSystem Other ();
	public abstract void BeforeTurnStart();
}
