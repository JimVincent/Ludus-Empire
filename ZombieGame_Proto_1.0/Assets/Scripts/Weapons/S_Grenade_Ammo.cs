using UnityEngine;
using System.Collections;

public class S_Grenade_Ammo : MonoBehaviour {

	public GunShoot gunscript;
	public int ammoAmount;
	public AudioClip ammoPickUp, ammoFull;

	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("P_Player_Gun_Placeholder").GetComponent<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.grenadenumber < gunscript.maxGrenadeAmmo)
		{
			AudioSource.PlayClipAtPoint(ammoPickUp, transform.position);
			gunscript.grenadenumber += ammoAmount;
		
			if(gunscript.grenadenumber > gunscript.maxGrenadeAmmo)
			{
				gunscript.grenadenumber = gunscript.maxGrenadeAmmo;
			}

			if(S_HUD_Manager.inst.equipedWeapon == selectedWeapon.grenadeLauncher){
				S_HUD_Manager.inst.GrenadeCount();
			}

			Destroy(gameObject);
		}
		else if(col.transform.tag == "Player" && gunscript.grenadenumber >= gunscript.maxGrenadeAmmo)
		{
			AudioSource.PlayClipAtPoint(ammoFull,transform.position);
		}
	}
}
