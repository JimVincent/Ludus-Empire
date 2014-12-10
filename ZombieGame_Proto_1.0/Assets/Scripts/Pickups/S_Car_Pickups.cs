using UnityEngine;
using System.Collections;

public class S_Car_Pickups : MonoBehaviour {


	public float repairValue;
	public AudioClip pickupSFX;

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			AudioSource.PlayClipAtPoint(pickupSFX,transform.position);
			col.GetComponent<S_Player_Items>().carPartValue += repairValue;
			Destroy(gameObject);
		}
	}
}
