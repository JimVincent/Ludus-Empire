using UnityEngine;
using System.Collections;

public class S_End_Arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(S_PlayerController.inst.inCar){
			gameObject.renderer.enabled = true;
		}
	}
}
