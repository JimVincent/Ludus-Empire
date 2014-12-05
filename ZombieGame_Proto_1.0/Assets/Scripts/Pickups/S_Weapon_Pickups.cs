using UnityEngine;
using System.Collections;

public class S_Weapon_Pickups : MonoBehaviour {

	public enum pickupType{
		rifle,
		flamethrower,
		gLauncher
	}

	public pickupType weaponType;

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			if (weaponType == pickupType.rifle){
				col.GetComponent<S_InventoryManager>().gotAssault = true;
				col.GetComponentInChildren<GunShoot>().machgunbullets += 30;
			}
			else if (weaponType == pickupType.flamethrower){
				col.GetComponent<S_InventoryManager>().gotFlame = true;
				col.GetComponentInChildren<GunShoot>().flamefuel += 3;
			}
			else{
				col.GetComponent<S_InventoryManager>().gotlauncher = true;
				col.GetComponentInChildren<GunShoot>().grenadenumber += 2;
			}

			Destroy(gameObject);
		}
	}
}
