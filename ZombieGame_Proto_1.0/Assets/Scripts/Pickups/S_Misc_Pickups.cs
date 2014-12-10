using UnityEngine;
using System.Collections;

public class S_Misc_Pickups : MonoBehaviour {

	public AudioClip pickupSFX;

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player" && !col.GetComponent<S_Player_Items>().hasMisc){
			AudioSource.PlayClipAtPoint(pickupSFX,transform.position);
			col.GetComponent<S_Player_Items>().hasMisc = true;
			Destroy(gameObject);
		}
	}
}
