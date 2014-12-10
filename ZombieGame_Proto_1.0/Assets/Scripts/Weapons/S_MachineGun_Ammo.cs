using UnityEngine;
using System.Collections;

public class S_MachineGun_Ammo : MonoBehaviour {

	public GunShoot gunscript;
	public int ammoAmount;
	public AudioClip ammoPickup, ammoFull;

	// Use this for initialization
	void Start () {
		gunscript = GameObject.Find ("P_Player_Gun_Placeholder").GetComponent<GunShoot> ();
	}
	
	// Update is called once per frame
	void Update () {
	}	
	void OnTriggerEnter (Collider col)
	{
		if(col.transform.tag == "Player" && gunscript.machgunbullets < gunscript.maxMachBullets)
		{
			AudioSource.PlayClipAtPoint(ammoPickup, transform.position);
			gunscript.machgunbullets += ammoAmount;
			if (gunscript.machgunbullets > gunscript.maxMachBullets)
			{
				gunscript.machgunbullets = gunscript.maxMachBullets;
			}

			Destroy(gameObject);
		}
		else if(col.transform.tag == "Player" && gunscript.machgunbullets >= gunscript.maxMachBullets)
		{
			AudioSource.PlayClipAtPoint(ammoFull,transform.position);
		}
	}
}
