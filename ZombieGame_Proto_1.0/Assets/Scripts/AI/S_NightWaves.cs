using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_NightWaves : MonoBehaviour {

	public GameObject runnerPrefab;
	public int quantityIncreasePerDay = 3;
	public int wavesPerNight = 4;
	public float latestSpawnWave = 10.0f;
	public float spawnRate = 10.0f;
	
	private List<GameObject> zombies = new List<GameObject>();


	// starting amount
	private int quantity = 5;
	private float timer = 0.0f;

	//For Spawn Position
	private Vector3 zombiePos;
	private float offsetX, offsetZ;

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
			if(S_DayNightCycle.tUntilNewDay > latestSpawnWave)
			{
				// spawn at rate of x
				timer += Time.deltaTime;
				if(timer >= spawnRate)
				{
					for(int i = 0; i < quantity; i++)
					{
						offsetX = transform.position.x + Random.Range(0,5);
						offsetZ = transform.position.z + Random.Range(0,5);
						zombiePos = new Vector3(offsetX, transform.position.y, offsetZ);
						zombies.Add(((GameObject)Instantiate(runnerPrefab, zombiePos, Quaternion.identity)));
					}
					timer = 0.0f;
				}
			}
		}
		else
		{
			// clear any active runners
			if(zombies != null && zombies.Count > 0)
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
	}
}
