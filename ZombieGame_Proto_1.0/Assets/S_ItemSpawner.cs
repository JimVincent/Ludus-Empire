using UnityEngine;
using System.Collections;

public class S_ItemSpawner : MonoBehaviour 
{
	// spawn box vars
	public Vector2 spawnBoxPos;
	public float spawnBoxWidth = 1.0f;
	public float spawnBoxLength = 1.0f;

	[System.Serializable]
	public class item
	{
		public GameObject prefab;
		public int chance;

		[System.NonSerialized]
		public int value;
	}
	
	public item[] items;

	private GameObject activeItem;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// returns a prefab determined by chance
	public GameObject NewItem()
	{
		int diceSide = 0;

		// default object
		GameObject chosen = GameObject.CreatePrimitive(PrimitiveType.Cube);
		chosen.renderer.material.color = Color.magenta;

		for(int i = 0; i < items.Length; i++)
		{
			diceSide += Mathf.FloorToInt((100 / items[i].chance));
			items[i].value = diceSide;
		}

		int diceRoll = Random.Range(1, diceSide + 1);

		for(int i = 0; i < items.Length; i++)
		{
			// check if smaller than value
			if(diceRoll < items[i].value)
				chosen = items[i].prefab;
		}

		return chosen;
	}
}
