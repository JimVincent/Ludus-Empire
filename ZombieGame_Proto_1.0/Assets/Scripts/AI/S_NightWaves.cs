using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_NightWaves : MonoBehaviour {

	public GameObject runnerPrefab;
	public int quantityIncreasePerDay = 3;
	public float latestSpawnWave = 10.0f;
	public float spawnRate = 10.0f;

	public Transform[] spawnPos;
	
	private List<GameObject> zombies = new List<GameObject>();


	// starting amount
	private int quantity = 5;
	private float timer = 0.0f;
	private bool callOnce;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		// spawn time frame

		if(S_DayNightCycle.dayState == S_DayNightCycle.DayState.night)
		{
			callOnce = true;

			if(S_DayNightCycle.tUntilNewDay > latestSpawnWave)
			{
				// spawn at rate of x
				timer += Time.deltaTime;
				if(timer >= spawnRate)
				{
					int dice = Random.Range(0, spawnPos.Length);

					for(int i = 0; i < quantity; i++)
					{
						zombies.Add(((GameObject)Instantiate(runnerPrefab, spawnPos[dice].position, Quaternion.identity)));
					}
					timer = 0.0f;
				}
			}
		}
		else
		{
			// clear any active runners
			if(zombies != null && zombies.Count > 0 && !callOnce)
				ClearZombies();
		}

	
	}

	// clears all active runners
	public void ClearZombies()
	{
		for(int i = 0; i < zombies.Count; i++)
		{
			zombies[i].GetComponent<S_Zombie_Health>().OnDeath();
			zombies.Remove(zombies[i]);
		}

		// increase zombie amount for next night
		quantity += quantityIncreasePerDay;
		callOnce = true;
	}
}
