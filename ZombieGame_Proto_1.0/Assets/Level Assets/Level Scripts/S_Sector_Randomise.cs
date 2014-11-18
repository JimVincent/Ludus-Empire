using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Sector_Randomise : MonoBehaviour {
	public List<GameObject> buildings = new List<GameObject>();
	public GameObject smallObstacle, mediumObstacle, largeObstacle;
	public int numberOfLarge, numberOfMedium, numberOfSmall;
	public float offsetFromLarge, offsetFromMedium;

	Vector3 objectPosition;
	float xPos, zPos, degreesOfRotation;
	Quaternion objectRotation;
	int randomIndex;
	List<Transform> largeObsInWorld = new List<Transform>();
	List<Transform> mediumObsInWorld = new List<Transform>();

	// Use this for initialization
	void Start () {
		PlaceKeyBuilding();
		PlaceLargeObstacles();
		PlaceMediumObstacles();
		PlaceSmallObstacles();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void PlaceKeyBuilding(){
		xPos = Random.Range(transform.position.x - (transform.localScale.x/2),transform.position.x + (transform.localScale.x/2));
		zPos = Random.Range(transform.position.z - (transform.localScale.z/2),transform.position.z + (transform.localScale.z/2));
		objectPosition = new Vector3(xPos,1,zPos);

		//degreesOfRotation = Random.Range(0,360);
		objectRotation.eulerAngles = new Vector3(0,degreesOfRotation,0);

		randomIndex = Mathf.RoundToInt(Random.Range(0,buildings.Count));

		Instantiate(buildings[randomIndex],objectPosition, objectRotation);
	}

	void PlaceLargeObstacles(){
		GameObject currentLarge;

		for (int i = 0; i < numberOfLarge; i++){
			xPos = Random.Range(transform.position.x - (transform.localScale.x/2),transform.position.x + (transform.localScale.x/2));
			zPos = Random.Range(transform.position.z - (transform.localScale.z/2),transform.position.z + (transform.localScale.z/2));
			objectPosition = new Vector3(xPos,1,zPos);

			//randomIndex = Mathf.RoundToInt(Random.Range(0,buildings.Count));

			currentLarge = Instantiate(largeObstacle,objectPosition, Quaternion.identity) as GameObject;
			largeObsInWorld.Add(currentLarge.transform);
		}
	}

	void PlaceMediumObstacles(){
		for (int i = 0; i < largeObsInWorld.Count; i++){
			for (int  j = 0; j < numberOfMedium; j++){
				xPos = Random.Range(largeObsInWorld[i].position.x - offsetFromLarge, largeObsInWorld[i].position.x + offsetFromLarge);
				zPos = Random.Range(largeObsInWorld[i].position.z - offsetFromLarge, largeObsInWorld[i].position.z + offsetFromLarge);
				objectPosition = new Vector3(xPos,1,zPos);
				
				//randomIndex = Mathf.RoundToInt(Random.Range(0,buildings.Count));
				
				Instantiate(mediumObstacle,objectPosition, Quaternion.identity);
			}
		}
	}

	void PlaceSmallObstacles(){
		for (int i = 0; i < numberOfSmall; i++){
			xPos = Random.Range(transform.position.x - (transform.localScale.x/2),transform.position.x + (transform.localScale.x/2));
			zPos = Random.Range(transform.position.z - (transform.localScale.z/2),transform.position.z + (transform.localScale.z/2));
			objectPosition = new Vector3(xPos,1,zPos);
			
			//randomIndex = Mathf.RoundToInt(Random.Range(0,buildings.Count));
			
			Instantiate(smallObstacle,objectPosition, Quaternion.identity);
		}
	}
}
