using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Building_Manage : MonoBehaviour 
{
	public List<GameObject> buildingObj = new List<GameObject>();
	[Range(1, 3)]
	public int carPartsPerDay = 2;

	public List<C_Building> buildings = new List<C_Building>();
	public List<C_Building> tents = new List<C_Building>();
	private bool ItemsSpawned = false;

	// Use this for initialization
	void Start ()
	{
		Shuffle (buildingObj);

		// assign the building class list
		for(int i = 0; i < buildingObj.Count; i++)
		{
			buildings.Add(buildingObj[i].GetComponent<C_Building>());
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
		{
			// once per day
			if(!ItemsSpawned)
			{
				// stock items for each building
				for(int i = 0; i < buildings.Count; i++)
				{
					buildings[i].SpawnItem(C_Building.ItemType.item);
					buildings[i].HalveZombies();
				}

				// tents spawn only itmes
				for(int i = 0; i < tents.Count; i++)
				{
					tents[i].SpawnItem(C_Building.ItemType.item);
				}
				
				// stock parts
				int partsToSpawn = Random.Range(1, carPartsPerDay + 1);
				bool found = false;

				// find a valid building
				for(int i = 0; i < partsToSpawn; i++)
				{
					int usedBuilding;
					while(!found)
					{
						int randomBuilding = Random.Range(0, buildings.Count);

						// check can spawn parts
						if(buildings[randomBuilding].carParts.Length > 0)
						{
							buildings[randomBuilding].SpawnItem(C_Building.ItemType.carPart);
							buildings[randomBuilding].SpawnZombies();
							found = true;
						}
						
					}
				}

				ItemsSpawned = true;
			}
		}
		else
			ItemsSpawned = false;
	}

	public void Shuffle(List<GameObject> list)
	{
		for(int i = list.Count - 1; i > 0; i--)
		{
			Vector3 tempPos;
			Quaternion tempRot;

			int ranNum = Random.Range(0, i - 1);

			tempPos = list[i].transform.position;
			tempRot = list[i].transform.rotation;

			list[i].transform.position = list[ranNum].transform.position;
			list[i].transform.rotation = list[ranNum].transform.rotation;

			list[ranNum].transform.position = tempPos;
			list[ranNum].transform.rotation = tempRot;
		}
	}
}
