using UnityEngine;
using System.Collections;

public class S_Car_Pickups : MonoBehaviour {


	public float repairValue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			col.GetComponent<S_Player_Items>().carPartValue += repairValue;
			Destroy(gameObject);
		}
	}
}
