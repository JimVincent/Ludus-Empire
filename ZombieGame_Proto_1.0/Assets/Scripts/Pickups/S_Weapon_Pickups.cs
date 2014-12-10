using UnityEngine;
using System.Collections;

public class S_Weapon_Pickups : MonoBehaviour {

	public enum pickupType{
		rifle,
		flamethrower,
		gLauncher
	}

	public pickupType weaponType;
	public GunShoot gunScript;
	public AudioClip pickupSFX;

	void Start(){
		gunScript = GameObject.Find("P_Player_Gun_Placeholder").GetComponentInChildren<GunShoot>();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			if (weaponType == pickupType.rifle){
				S_HUD_Manager.inst.gotAssault = true;
				S_HUD_Manager.inst.mGunObj.SetActive(true);

				if(gunScript.machgunbullets < gunScript.maxMachBullets){
					gunScript.machgunbullets += 30;

					if(gunScript.machgunbullets > gunScript.maxMachBullets){
						gunScript.machgunbullets = gunScript.maxMachBullets;
					}
				}
			}
			else if (weaponType == pickupType.flamethrower){
				S_HUD_Manager.inst.gotFlame = true;
				S_HUD_Manager.inst.flameThrowerObj.SetActive(true);

				if(gunScript.flamefuel < gunScript.maxFlameFuel){
					col.GetComponentInChildren<GunShoot>().flamefuel += 3;

					if(gunScript.flamefuel > gunScript.maxFlameFuel){
						gunScript.flamefuel = gunScript.maxFlameFuel;
					}
				}
			}
			else{
				S_HUD_Manager.inst.gotlauncher = true;
				S_HUD_Manager.inst.gLauncherObj.SetActive(true);

				if(gunScript.grenadenumber < gunScript.maxGrenadeAmmo){
					col.GetComponentInChildren<GunShoot>().grenadenumber += 2;

					if(gunScript.grenadenumber > gunScript.maxGrenadeAmmo){
						gunScript.grenadenumber = gunScript.maxGrenadeAmmo;
					}
				}

				if(S_HUD_Manager.inst.equipedWeapon == selectedWeapon.grenadeLauncher){
					S_HUD_Manager.inst.GrenadeCount();
				}
			}
			AudioSource.PlayClipAtPoint(pickupSFX,transform.position);
			Destroy(gameObject);
		}
	}
}
