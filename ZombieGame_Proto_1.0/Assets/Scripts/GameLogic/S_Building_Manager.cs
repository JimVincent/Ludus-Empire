using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Randomise_Buildings : MonoBehaviour 
{
	[System.NonSerialized]
	static public GameObject activeRequest;

	public List<GameObject> buildings = new List<GameObject>();
	public int maxPartsPerDay;
		
	private bool ready = true;
	private int partCount = 0;
	private List<GameObject> buildingsLeft = new List<GameObject>();
	private List<C_Building> cBuilding = new List<C_Building>();

	// Use this for initialization
	void Awake() 
	{
		int randomIndex;
		Transform currentChild;

		buildingsLeft = buildings.GetRange(0,buildings.Count);

		for(int i = 0; i < buildings.Count; i++)
		{
			randomIndex = Mathf.RoundToInt(Random.Range(0,buildingsLeft.Count));
			currentChild = transform.GetChild(i);
			Instantiate (buildingsLeft[randomIndex],currentChild.position, currentChild.rotation);
			buildingsLeft.RemoveAt(randomIndex);
		}

		for(int j = 0; j < transform.childCount; j++)
		{
			Destroy(transform.GetChild(j).gameObject);
			cBuilding.Add(buildings[j].GetComponent<C_Building>());
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
			for(int i = 0; i < cBuilding.Count; i++)
			{
				cBuilding[i].SpawnItem(C_Building.ItemType.item);
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
					int randomBuild = Mathf.RoundToInt(Random.Range(0, cBuilding.Count));
					
					// check if it can spawn parts
					if(cBuilding[randomBuild].carParts.Length > 0)
					{
						found = true;
						cBuilding[randomBuild].SpawnItem(C_Building.ItemType.carPart);
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
			if(cBuilding[randomBuild].requestItems.Length > 0)
			{
				found = true;
				cBuilding[randomBuild].SpawnItem(C_Building.ItemType.requestItem);
			}
		}
	}
}