using UnityEngine;
using System.Collections;

public class S_Player_Items : MonoBehaviour {

	public float carPartValue = 0;
	public bool hasMisc = false;

	private S_Car_Maintenance fixScript;

	// Use this for initialization
	void Start () {
		fixScript = GameObject.Find("GameLogic").GetComponent<S_Car_Maintenance>();
	}

	public void OnMechanicReturn()
	{
		fixScript.FixCar(carPartValue);
		carPartValue = 0;

		if(S_Mechanic_AI.activeRequest && hasMisc)
		{
			S_Mechanic_AI.activeRequest = false;
			hasMisc = false;
		}
	}
}
