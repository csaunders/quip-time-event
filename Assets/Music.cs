using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioSource source;
	public float fadeTime;
	private float timer;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
		if( timer <1)
		{
			timer += Time.deltaTime / fadeTime;
		}

		source.volume = timer;
	}
}