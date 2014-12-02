using UnityEngine;
using System.Collections;

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
	public float itemSpawnradius = 1.0f;

	[System.NonSerialized]
	public GameObject activeItem;

	private Vector3 itemSpawnPos;
	
	void Start()
	{
		// puts spawn radius in front of building without overlap
		float zDistance =(transform.localScale.z / 2) + itemSpawnradius;
		itemSpawnPos = transform.position;
		itemSpawnPos += transform.forward * zDistance;
	}

	// spawns the passed item within random pos restraints
	public void SpawnItem(ItemType type)
	{
		// destroy any existing item
		if(activeItem != null)
			Destroy(activeItem);

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
		Vector3 tempV = itemSpawnPos + Random.insideUnitSphere * itemSpawnradius;
		Vector3 spawnPos = new Vector3(tempV.x, itemSpawnPos.y, tempV.z);

		// spawn obj sitting on ground level
		activeItem = (GameObject)Instantiate(activeItem, spawnPos, Quaternion.identity);
		Vector3 pos = activeItem.transform.position;
		activeItem.transform.position = new Vector3(pos.x, pos.y + activeItem.transform.localScale.y, pos.z);
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

	// show the item spawn pos
	void OnDrawGizmosSelected() 
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(itemSpawnPos, itemSpawnradius);
	}
}