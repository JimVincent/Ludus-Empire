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
				S_HUD_Manager.inst.gotAssault = true;
				col.GetComponentInChildren<GunShoot>().machgunbullets += 30;
			}
			else if (weaponType == pickupType.flamethrower){
				S_HUD_Manager.inst.gotFlame = true;
				col.GetComponentInChildren<GunShoot>().flamefuel += 3;
			}
			else{
				S_HUD_Manager.inst.gotlauncher = true;
				col.GetComponentInChildren<GunShoot>().grenadenumber += 2;
			}

			Destroy(gameObject);
		}
	}
}
