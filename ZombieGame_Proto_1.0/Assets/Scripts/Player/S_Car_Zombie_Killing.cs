using UnityEngine;
using System.Collections;

public class S_Car_Zombie_Killing : MonoBehaviour {

	public AudioClip zombieImpact;

	void OnTriggerEnter(Collider col){
		if(col.tag == "Enemy"){
			AudioSource.PlayClipAtPoint(zombieImpact,transform.position);
			col.GetComponent<S_Zombie_Health>().OnHit(100);
			Destroy(col.gameObject);
		}
	}
}
