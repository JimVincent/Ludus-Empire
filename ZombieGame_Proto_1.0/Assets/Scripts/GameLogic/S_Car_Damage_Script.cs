using UnityEngine;
using System.Collections;

public class S_Car_Damage_Script : MonoBehaviour {


	public int zombieDamage;

	void OnTriggerEnter(Collider col){
		if(col.tag == "Attack"){
			if(S_Car_Maintenance.carHealth > 0)
			{
				S_Car_Maintenance.carHealth -= zombieDamage;
				if(S_Car_Maintenance.carHealth <= 0)
				{
					PlayerPrefs.SetInt("victory",0);
					Application.LoadLevel("End_Game");
				}
			}


		}
	}
}
