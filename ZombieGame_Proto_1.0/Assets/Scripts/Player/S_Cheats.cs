using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Cheats : MonoBehaviour {

	public List<GameObject> pickupList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			for (int i = 0; i < pickupList.Count; i++){
				Instantiate(pickupList[i],transform.position, pickupList[i].transform.rotation);
			}
		}

		if(Input.GetKeyDown(KeyCode.M)){
			gameObject.GetComponent<S_PlayerController>().inCar = true;
		}
	}
}
