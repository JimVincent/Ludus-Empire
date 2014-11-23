using UnityEngine;
using System.Collections;

public class S_Player_Torch : MonoBehaviour 
{
	public float torchHeight = 50.0f;
	[Range(0.01f, 0.5f)]
	public float maxTorchIntensity = 0.5f;
	[Range(0.1f, 1.0f)]
	public float torchDimSpeed = 1.0f;
	public float blinkDuration = 0.18f;
	
	private GameObject torch;
	private S_DayNightCycle.DayState dayState;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () 
	{
		// set up torch holder
		torch = GameObject.CreatePrimitive(PrimitiveType.Cube);
		torch.renderer.enabled = false;
		torch.collider.enabled = false;
		Vector3 pPos = transform.position;
		torch.transform.position = new Vector3(pPos.x, pPos.y + torchHeight, pPos.z);
		torch.transform.LookAt(pPos);
		torch.transform.parent = transform;

		// attach light and setup
		torch.AddComponent<Light>();
		torch.light.type = LightType.Spot;
		torch.light.range = 150.0f;
		torch.light.intensity = 0.0f;
	}

	void Update()
	{
		dayState = S_DayNightCycle.dayState;

		// check for change of light
		if(S_DayNightCycle.dayState == S_DayNightCycle.DayState.day)
		{
			// turn light off
			if(torch.light.spotAngle > 35.0f)
			{
				SwitchLightOn(false);
				timer = 0.0f;
			}
		}
		else
		{
			if(timer < blinkDuration)							// flash on
			{
				torch.light.intensity = maxTorchIntensity / 4;
			}
			else if(timer > blinkDuration && timer < blinkDuration * 2)	//flash off
			{
				torch.light.intensity = 0.0f;
			}
			else if(timer > blinkDuration * 2 && timer < blinkDuration * 3)	// flash on
			{
				torch.light.intensity = maxTorchIntensity / 4;
			}
			else if(timer > blinkDuration * 3 && timer < blinkDuration * 4)
			{
				torch.light.intensity = 0.0f;
			}
			else if(timer > blinkDuration * 4)					// off and fade in
			{
				SwitchLightOn(true);
			}

			timer += Time.deltaTime;
		}
	}

	public void SwitchLightOn(bool on)
	{
		float lSpeed;

		if(on)
		{
			lSpeed = torchDimSpeed;

			// turn light on
			if(torch.light.intensity < maxTorchIntensity)
			{
				torch.light.intensity += lSpeed * Time.deltaTime;
			}
			
			// grow light
			if(torch.light.spotAngle < 150.0f)
			{
				torch.light.spotAngle += (lSpeed * 150) * Time.deltaTime;
			}
		}
		else
		{
			lSpeed = -torchDimSpeed;

			// turn light on
			if(torch.light.intensity > 0.0f)
			{
				torch.light.intensity += lSpeed * Time.deltaTime;
			}
			
			// grow light
			if(torch.light.spotAngle > 35.0f)
			{
				torch.light.spotAngle += (lSpeed * 150) * Time.deltaTime;
			}
		}
	}
}