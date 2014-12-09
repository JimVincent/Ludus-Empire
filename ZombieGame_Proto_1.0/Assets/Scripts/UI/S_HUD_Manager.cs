using UnityEngine;
using System.Collections;

public enum selectedWeapon {handGun, machineGun, flameThrower, grenadeLauncher};

public enum building
{
	Hospital,
	WorkShop,
	ConvinienceStore,
	Hardware,
	FuelStation,
	Outpost,
	GunShop,
	House,
}

public class S_HUD_Manager : MonoBehaviour 
{
	public static S_HUD_Manager inst;

	// health bars
	public GameObject carBarObj;
	public GameObject healthBarObj;

	// weapon objects
	public GameObject handGunObj;
	public GameObject mGunObj;
	public GameObject flameThrowerObj;
	public GameObject gLauncherObj;
	public GameObject redHighLight;

	// ammo Objects
	public GameObject mGunBulletsBar;
	public GameObject handGunBulletBar;
	public GameObject flameFuelBar;
	public GameObject grenadeBar;
	public GameObject[] grenadeObjs;

	// request locations objects
	public GameObject hospitalObj;
	public GameObject shopObj;
	public GameObject hardwareObj;
	public GameObject fuelStationObj;
	public GameObject gunShopObj;
	public GameObject outPostObj;
	public GameObject houseObj;
	public GameObject workShopObj;

	// weapon ammo
	public int assaultRAmmo = 0;
	public float flameTAmmo = 0.0f;
	public int gLauncherAmmo = 0;
	public int assaultMaxAmmo;
	public float fameTMaxAmmo;
	public int gLaunchMaxAmmo;

	// acquired weapons
	public bool gotAssault = false;
	public bool gotFlame = false;
	public bool gotlauncher = false;
	
	public GameObject requestItemPrefab;
	public selectedWeapon equipedWeapon;
	public building requestLocation;
	public bool activeRequest;

	public float playerHealth;
	public float carHealth;

	private GameObject requestBuildObj;

    //health and ammo bars
    private float healthBarFull;
    private float ammoBarFull;
	
	public void Awake()
	{
		inst = this;
	}
	
	// Use this for initialization
	void Start () 
	{
        healthBarFull = healthBarObj.transform.localScale.x;
        ammoBarFull = flameFuelBar.transform.localScale.x;

		mGunObj.SetActive(false);
		flameThrowerObj.SetActive(false);
		gLauncherObj.SetActive(false);
		
		handGunBulletBar.SetActive(true);
		flameFuelBar.SetActive(false);
		mGunBulletsBar.SetActive(false);
		grenadeBar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
        // update fuel ammo bar
        if(flameFuelBar.activeSelf == true)
			flameFuelBar.transform.localScale = new Vector3(ammoBarFull * (flameTAmmo / 100), flameThrowerObj.transform.localScale.y, flameThrowerObj.transform.localScale.z);

        // update machine gun ammo bar
        if(mGunBulletsBar.activeSelf == true)
			mGunBulletsBar.transform.localScale = new Vector3(ammoBarFull * (assaultMaxAmmo / 100), mGunBulletsBar.transform.localScale.y, mGunBulletsBar.transform.localScale.z);

		// update player healh bar
		if(playerHealth > 0.0f)
			healthBarObj.transform.localScale = new Vector3(healthBarFull * (playerHealth / 100), healthBarObj.transform.localScale.y, healthBarObj.transform.localScale.z);

		// update car health
		if(carHealth > 0.0f)
			carBarObj.transform.localScale = new Vector3(healthBarFull * (carHealth / 100), carBarObj.transform.localScale.y, carBarObj.transform.localScale.z);

        // active request state
		if(activeRequest)
		{
			requestItemPrefab.SetActive(true);
			requestBuildObj.SetActive(true);
			
			switch (requestLocation)
			{
				case building.Hospital:
					requestBuildObj = hospitalObj;
				break;
				
				case building.WorkShop:
					requestBuildObj = workShopObj;
				break;
				
				case building.ConvinienceStore:
					requestBuildObj = shopObj;
				break;
				
				case building.Outpost:
					requestBuildObj = outPostObj;
				break;
				
				case building.House:
					requestBuildObj = houseObj;
				break;
				
				case building.FuelStation:
					requestBuildObj = fuelStationObj;
				break;
				
				case building.Hardware:
					requestBuildObj = hardwareObj;
				break;
				
				case building.GunShop:
					requestBuildObj = gunShopObj;
				break;
				
				default:
					Debug.Log("Something broke");
				break;
			}
		}
		else
		{
			requestItemPrefab.SetActive(true);
			requestBuildObj.SetActive(true);
		}
	
		// assign active weapon
		switch(equipedWeapon)
		{
			case selectedWeapon.handGun:
				redHighLight.transform.position = handGunObj.transform.position;
				handGunBulletBar.SetActive(true);
				flameFuelBar.SetActive(false);
				mGunBulletsBar.SetActive(false);
				grenadeBar.SetActive(false);
				
			break;
			
			case selectedWeapon.machineGun:
				redHighLight.transform.position = mGunObj.transform.position;
				handGunBulletBar.SetActive(false);
				flameFuelBar.SetActive(false);
				mGunBulletsBar.SetActive(true);
				grenadeBar.SetActive(false);
			break;
			
			case selectedWeapon.flameThrower:
				redHighLight.transform.position = flameThrowerObj.transform.position;
				handGunBulletBar.SetActive(false);
				flameFuelBar.SetActive(true);
				mGunBulletsBar.SetActive(false);
				grenadeBar.SetActive(false);
			break;
			
			case selectedWeapon.grenadeLauncher:
				redHighLight.transform.position = gLauncherObj.transform.position;
				handGunBulletBar.SetActive(false);
				flameFuelBar.SetActive(false);
				mGunBulletsBar.SetActive(false);
				grenadeBar.SetActive(true);

			break;
			
			default:
				Debug.Log("something broke");
			break;
		}
	}

	public void GrenadeCount()
	{
		//update grenade count
		for (int i = 0; i < gLaunchMaxAmmo; i++)
		{
			//grenadeObjs[i].SetActive(true);
		}
	}


}
