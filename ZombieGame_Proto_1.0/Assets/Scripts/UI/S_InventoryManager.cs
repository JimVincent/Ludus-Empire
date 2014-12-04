using UnityEngine;
using System.Collections;

public class S_InventoryManager : MonoBehaviour 
{
	// health bars
	public GameObject carBarObj;
	public GameObject healthBarObj;

	// weapon objects
	public GameObject assaultRObj;
	public GameObject flameTObj;
	public GameObject gLauncherObj;
	public GameObject handGunObj;

	// ammo Objects
	public GameObject bulletObj;
	public GameObject infBulletObj;
	public GameObject flameFuelObj;
	public GameObject clip;
	public GameObject grenadeObj;

	// request locations objects
	public GameObject hospitalObj;
	public GameObject shopObj;
	public GameObject hardwareObj;
	public GameObject gasStationObj;
	public GameObject gunShopObj;
	public GameObject armyBaseObj;
	public GameObject backYardObj;

	// request items
	public GameObject cofeeObj;
	public GameObject magazine;
	public GameObject tobacco;
	public GameObject tape;
	public GameObject screwDriverObj;

	// weapon ammo
	private int assaultRAmmo = 0;
	private float flameTAmmo = 0.0f;
	private int gLauncherAmmo = 0;

	// aquired weapons
	public bool gotAssault = false;
	public bool gotFlame = false;
	public bool gotlauncher = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}




}
