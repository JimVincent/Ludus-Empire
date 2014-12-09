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
	private int assaultRAmmo = 0;
	private float flameTAmmo = 0.0f;
	private int gLauncherAmmo = 0;

	// acquired weapons
	public bool gotAssault = false;
	public bool gotFlame = false;
	public bool gotlauncher = false;
	
	public GameObject requestItemPrefab;
	public selectedWeapon equipedWeapon;
	public building requestLocation;
	public bool activeRequest;

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
        healthBarFull = healthBarObj.localscale.x;
        ammoBarFull = flameFuelBar.localscale.x;

		mGunObj.setActive(false);
		flameThrowerObj.setActive(false);
		gLauncherObj.setActive(false);
		
		handGunBulletBar.setActive(true);
		flameFuelBar.setActive(false);
		mGunBulletsBar.setActive(false);
		grenadeObjs.setActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
        //update machine gun ammo bar
        if (flameFuelBar.activeSelf == true)
        {
            flameFuelBar.transform.localscale = new vector3 (ammoBarFull * (flameTAmmo / 100), flameThrowerObj.localscale.y, flameThrowerObj.localscale.z);
        }

        //update machine gun ammo bar
        if (flameFuelBar.activeSelf == true)
        {
            flameFuelBar.transform.localscale = new vector3 (ammoBarFull * (flameTAmmo / 100), flameThrowerObj.localscale.y, flameThrowerObj.localscale.z);
        }

        // active request state
		if(activeRequest)
		{
			requestItemPrefab.setActive(true);
			requestBuildObj.setActive(true);
			
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
					debug.log("Something broke");
				break;
			}
		}
		else
		{
			
		}
	
		// assign active weapon
		switch(equipedWeapon)
		{
			case selectedWeapon.handGun:
				redHighLight.transform.position = handgun.transform.position;
				handGunBulletBar.setActive(true);
				flameFuelBar.setActive(false);
				mGunBulletsBar.setActive(false);
				grenadeBar.setActive(false);
				
			break;
			
			case selectedWeapon.machineGun:
				redHighLight.transform.position = mGunObj.transform.position;
				handGunBulletBar.setActive(false);
				flameFuelBar.setActive(false);
				mGunBulletsBar.setActive(true);
				grenadeBar.setActive(false);
			break;
			
			case selectedWeapon.flameThrower:
				redHighLight.transform.position = flameThrowerObj.transform.position;
				handGunBulletBar.setActive(false);
				flameFuelBar.setActive(true);
				mGunBulletsBar.setActive(false);
				grenadeBar.setActive(false);
			break;
			
			case selectedWeapon.grenadeLauncher:
				redHighLight.transform.position = gLauncherObj.transform.position;
				handGunBulletBar.setActive(false);
				flameFuelBar.setActive(false);
				mGunBulletsBar.setActive(false);
				grenadeBar.setActive(true);

                //update grenade count
                for (int i = 0; i < gLauncherAmmo; i++)
                {
                    grenadeObjs[i].setActive(true);
                }

			break;
			
			default:
				debug.log("something broke");
			break;
		}
	}




}
