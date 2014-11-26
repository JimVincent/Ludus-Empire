using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Building_Manager : MonoBehaviour 
{

	public C_Building[] town;
	

	// Use this for initialization
	void Awake () 
	{
			C_Building temp = town[0];
			town[0] = town[1];
			town[1] = temp;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
