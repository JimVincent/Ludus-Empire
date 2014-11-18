using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_Randomise_Buildings : MonoBehaviour {
	public List<GameObject> buildings = new List<GameObject>();
	List<GameObject> buildingsLeft = new List<GameObject>();

	// Use this for initialization
	void Start () {
		int randomIndex;
		Transform currentChild;

		buildingsLeft = buildings.GetRange(0,buildings.Count);

		for(int i = 0; i < buildings.Count; i++){
			randomIndex = Mathf.RoundToInt(Random.Range(0,buildingsLeft.Count));

			currentChild = transform.GetChild(i);
			Instantiate (buildingsLeft[randomIndex],currentChild.position, currentChild.rotation);
			buildingsLeft.RemoveAt(randomIndex);
		}

		for(int j = 0; j < transform.childCount; j++){
			Destroy(transform.GetChild(j).gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
