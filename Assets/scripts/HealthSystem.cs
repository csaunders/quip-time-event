using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour {

	public static int MAX_HEALTH = 2000;

	public int _currentHealth;
	public Text healthDisplay;

	// Use this for initialization
	public void Start () {
		_currentHealth = MAX_HEALTH;	
	}

	public void Update() {
		healthDisplay = "HP: " + _currentHealth;
	}

	public void InflictDamage(float amount)
	{
		_currentHealth -= amount;
	}

	public bool IsDead(){
		return _currentHealth <= 0.0f;
	}
}
