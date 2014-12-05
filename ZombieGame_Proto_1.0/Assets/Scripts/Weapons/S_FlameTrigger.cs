using UnityEngine;
using System.Collections;

public class S_FlameTrigger : MonoBehaviour {

	public float flameDamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnTriggerEnter (Collider col) {
//		print (col.transform.name);
//		if(col.transform.tag == "Enemy"){
//			col.gameObject.GetComponent<S_Zombie_Health>().burnRate += flameDamage;
//		}
//	}

//	void OnTriggerExit(Collider col){
//		if(col.transform.tag == "Enemy"){
//			col.gameObject.GetComponent<S_Zombie_Health>().burnRate -= flameDamage;
//		}
//	}
}
