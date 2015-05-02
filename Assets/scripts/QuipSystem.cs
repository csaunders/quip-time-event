using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuipSystem : MonoBehaviour {
	
	public Text quipPlayer;
	public float flickerLimit = 1.0f;
	public float renderSpeedModifier = 1.0f;
	
	madlib.Template template;
	public float timer;
	public string phrase = "some people just |qte,want| to watch the world |qte, burn|.";

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime * renderSpeedModifier;
		if (timer > flickerLimit) {
			timer = 0;
			quipPlayer.enabled = !quipPlayer.enabled;
		}
	}
}
