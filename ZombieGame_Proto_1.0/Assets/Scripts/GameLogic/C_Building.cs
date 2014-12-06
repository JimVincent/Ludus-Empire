using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class C_Building : MonoBehaviour 
{
	public enum ItemType {item, carPart, requestItem};

	[System.Serializable]
	public class Item
	{
		public GameObject prefab;
		[Range(1,100)]
		public int chance;
	}

	// user vars
	public Item[] items;
	public Item[] carParts;
	public Item[] requestItems;

	public GameObject walkerPrefab;
	public float walkerSpawnRadius;
	public int easyZombCount, mediumZombCount, hardZombCount;
	private List<GameObject> activeWalkers = new List<GameObject>();

	[System.NonSerialized]
	public GameObject activeItem;

	public GameObject itemSpawner;
	private GameObject currentItem;
	private float itemSpawnradius = 3.0f;


	// spawns the passed item within random pos restraints
	public void SpawnItem(ItemType type)
	{
		// destroy any existing item
		if(currentItem != null)
			Destroy(currentItem);

		// set active item
		switch(type)
		{
		case ItemType.item:
			activeItem = NewStock(items);
			break;

		case ItemType.carPart:
			activeItem = NewStock(carParts);
			break;

		case ItemType.requestItem:
			activeItem = NewStock(requestItems);
			break;

		default:
			Debug.Log("Something gone done broke!");
			break;
		}

		// pick a random pos within radius
		Vector3 tempV = itemSpawner.transform.position + Random.insideUnitSphere * itemSpawnradius;
		Vector3 spawnPos = new Vector3(tempV.x, itemSpawner.transform.position.y, tempV.z);

		// spawn obj sitting on ground level
		currentItem = (GameObject)Instantiate(activeItem, spawnPos, Quaternion.identity);
		Vector3 pos = currentItem.transform.position;
	}

	// halves the left over zombies
	public void HalveZombies()
	{
		// check for active zombies : leave 1
		if(activeWalkers.Count > 1)
		{
			// even count
			if(activeWalkers.Count % 2 == 0)
			{
				// remove half
				for(int i = 0; i < activeWalkers.Count / 2; i++)
				{
					Destroy(activeWalkers[i]);
					activeWalkers.RemoveAt(i);
				}
			}
			else // odd count
			{
				// remove half and round to nearest int
				for(int i = 0; i < Mathf.RoundToInt(activeWalkers.Count / 2); i++)
				{
					Destroy(activeWalkers[i]);
					activeWalkers.RemoveAt(i);
				}
			}
		}
	}

	public void SpawnZombies()
	{
		// remove any active zombies
		if(activeWalkers.Count > 0)
		{
			// destroy all
			for(int i = 0; i < activeWalkers.Count; i++)
			{
				Destroy(activeWalkers[i]);
				activeWalkers.RemoveAt(i);
			}
		}

		// spawn Zombies dependent on part value
		int zombieCount;
		if(activeItem.tag == "HardPart")
			zombieCount = hardZombCount;
		else if(activeItem.tag == "MediumPart")
			zombieCount = mediumZombCount;
		else
			zombieCount = easyZombCount;

		for(int i = 0; i < zombieCount; i++)
		{
			Vector3 tempVect = itemSpawner.transform.position + Random.insideUnitSphere * walkerSpawnRadius;
			Vector3 tempPos = new Vector3(tempVect.x, transform.position.y, tempVect.z);
			GameObject tempObj = (GameObject)Instantiate(walkerPrefab, tempPos, Quaternion.identity);
			activeWalkers.Add(tempObj);
		}
	}

	// returns a GO from the given stock based on chance
	public GameObject NewStock(Item[] stock)
	{
		// Error item returned as pink cube
		GameObject tempGO = GameObject.CreatePrimitive(PrimitiveType.Cube);
		tempGO.renderer.material.color = Color.magenta;

		int diceRoll = Mathf.RoundToInt(Random.Range(1, 100));
		int tempI = 0;

		for(int i = 0; i < stock.Length; i++)
		{
			tempI += stock[i].chance;

			if(diceRoll < tempI)
				tempGO = stock[i].prefab;
		}

		return tempGO;
	}
}