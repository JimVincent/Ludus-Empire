using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_ItemSpawner : MonoBehaviour 
{
	public int maxPartsPerDay;

	[System.NonSerialized]
	static public GameObject activeRequest;
	static public string activeRequestTag; 

	private bool ready = true;
	private int partCount = 0;
	private List<C_Building> buildings;

	// Use this for initialization
	void Start () 
	{
		// assign buildings class's
		for(int i = 0; i < S_Randomise_Buildings.inst.buildings.Count; i++)
		{
			buildings.Add(S_Randomise_Buildings.inst.buildings[i].GetComponent<C_Building>());
		}
	}

	// Update is called once per frame
	void Update () 
	{
		// replnish once per day
		if(ready && S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
		{
			ready = false;

			// tell list of buildings to spawn  items
			for(int i = 0; i < buildings.Count; i++)
			{
				buildings[i].SpawnItem(C_Building.ItemType.item);
				print (buildings[i].transform.position);
			}

			// how many parts to spawn
			partCount = Mathf.RoundToInt(Random.Range(1, maxPartsPerDay));

			// spawn parts
			for(int i = 0; i < partCount; i++)
			{
				// pick a random building
				bool found = false;
				while(!found)
				{
					int randomBuild = Mathf.RoundToInt(Random.Range(0, buildings.Count));

					// check if it can spawn parts
					if(buildings[randomBuild].carParts.Length > 0)
					{
						found = true;
						buildings[randomBuild].SpawnItem(C_Building.ItemType.carPart);
					}
				}
			}
		}
	}

	// picks random building and allows spawn
	public void SpawnRequest()
	{
		// pick a random building
		bool found = false;
		while(!found)
		{
			int randomBuild = Mathf.RoundToInt(Random.Range(0, buildings.Count));
			
			// check if it can spawn parts
			if(buildings[randomBuild].requestItems.Length > 0)
			{
				found = true;
				buildings[randomBuild].SpawnItem(C_Building.ItemType.requestItem);
			}
		}
	}
}