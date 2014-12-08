using UnityEngine;
using System.Collections;

public class S_Grenade_Ammo : MonoBehaviour {

	public GunShoot gunscript;
	public int ammoAmount;

	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("P_Player_PlaceHolder").GetComponentInChildren<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.grenadenumber < gunscript.maxGrenadeAmmo)
		{
			gunscript.grenadenumber += ammoAmount;
			if(gunscript.grenadenumber > gunscript.maxGrenadeAmmo){
				gunscript.grenadenumber = gunscript.maxGrenadeAmmo;
			}

			Destroy(gameObject);
		}
	}
}
