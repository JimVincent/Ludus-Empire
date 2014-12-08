﻿using UnityEngine;
using System.Collections;

public class S_MachineGun_Ammo : MonoBehaviour {

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
		if(col.transform.tag == "Player" && gunscript.machgunbullets < gunscript.maxMachBullets)
		{
			gunscript.machgunbullets += ammoAmount;
			if (gunscript.machgunbullets > gunscript.maxMachBullets)
			{
				gunscript.machgunbullets = gunscript.maxMachBullets;
			}

			Destroy(gameObject);
		}
	}
}
