﻿using UnityEngine;
using System.Collections;

public class goToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButton("A")){
			Application.LoadLevel (4);
			
		}
	
	}
}