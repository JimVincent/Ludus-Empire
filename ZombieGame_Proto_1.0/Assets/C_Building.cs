using UnityEngine;
using System.Collections;

public class C_Building : MonoBehaviour 
{
	public enum Building
	{
		Hospital, 
		Workshop, 
		Convinience, 
		Hardware, 
		PetrolStation, 
		MilitaryCheckpoint, 
		RedneckHome, 
		CampSite
	};

	[System.Serializable]
	public class Item
	{
		public GameObject prefab;
		[Range(1,100)]
		public int chance;
	}

	public Building building;
	public Item[] stock;
	public float itemSpawnradius = 1.0f;

	private Vector3 itemSpawnPos;

	void Start()
	{
		// puts spawn radius in front of building without overlap
		itemSpawnPos = new Vector3(transform.position.x, 0.0f, transform.position.z + (transform.localScale.z / 2) + itemSpawnradius);
	}

	// spawns random itme within random pos restraints
	public void SpawnItem()
	{
		// pick a random pos within radius
		Vector3 tempV = itemSpawnPos + Random.insideUnitSphere * itemSpawnradius;
		Vector3 spawnPos = new Vector3(tempV.x, itemSpawnPos.y, tempV.z);

		Instantiate(NewStock(), spawnPos, Quaternion.identity);

	}

	// returns a GO from the given stock based on chance
	public GameObject NewStock()
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