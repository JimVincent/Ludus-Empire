﻿using UnityEngine;
using System.Collections;

public class S_Win_Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && S_PlayerController.inst.inCar){
			//trigger win state
			print ("YOU WIN!");
			PlayerPrefs.SetInt("victory",1);
			Application.LoadLevel("End_Game");
		}
	}
}
