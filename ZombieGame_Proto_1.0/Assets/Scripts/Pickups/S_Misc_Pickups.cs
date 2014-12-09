using UnityEngine;
using System.Collections;

public class S_Misc_Pickups : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player" && !col.GetComponent<S_Player_Items>().hasMisc){
			col.GetComponent<S_Player_Items>().hasMisc = true;
			Destroy(gameObject);
		}
	}
}
