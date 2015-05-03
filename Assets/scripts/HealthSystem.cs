using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class HealthSystem : MonoBehaviour {

	public static float MAX_HEALTH = 2000.0f;
	public Text healthDisplay;
	public Animator animator;
	public GameObject healthBar;
	public int Direction = 1;
	public bool IsTurn;
	
	private float _currentHealth;
	private Vector3 animStart, animEnd;
	private float distanceToCover, startTime;

	// Use this for initialization
	public void Start () {
		animStart = healthBar.transform.position;
		animEnd = healthBar.transform.position;
		distanceToCover = 1.0f;
		_currentHealth = MAX_HEALTH;	
	}

	public void Update() {
		healthDisplay.text = "HP: " + _currentHealth;
		if (distanceToCover != 0) {
			float distCovered = (Time.time - startTime) * 100;
			float fracJourney = distCovered / distanceToCover;
			healthBar.transform.position = Vector3.Lerp (animStart, animEnd, fracJourney);
		}
	}

	public void InflictDamage(float amount)
	{
		_currentHealth -= amount;
		float percentageDamage = amount / MAX_HEALTH;
		animStart = healthBar.transform.position;
		Debug.Log (animStart);
		animEnd = new Vector3 (animStart.x - 269.0f * percentageDamage * Direction, animStart.y, animStart.z);
		Debug.Log (animEnd);
		distanceToCover = Vector3.Distance (animStart, animEnd);
		startTime = Time.time;
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
