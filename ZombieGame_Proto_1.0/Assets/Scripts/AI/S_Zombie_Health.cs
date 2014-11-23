using UnityEngine;
using System.Collections;

public class S_Zombie_Health : MonoBehaviour 
{

	public ParticleSystem eFire;
	public ParticleSystem eDead;
	public ParticleSystem eHit;
	public float burnRate = 20.0f;

	public float health = 100;
	public bool onFire = false;

	private bool dead = false;

	void Start()
	{
		// set particle loops
		eFire.loop = true;
		eHit.loop = false;
		eDead.loop = false;
	}

	// Update is called once per frame
	void Update () 
	{
		// check health status
		if(health <= 0.0f)
		{
			OnDeath();
		}

		if(onFire)
		{
			eFire.Play();
			health -= burnRate * Time.deltaTime;
		}

		if(dead)
			OnDeath();
	}

	// receive damage
	public void OnHit(int damage)
	{
		eHit.Play();
		health -= (float)damage;
	}

	// check for flame hit
	void OnTriggerEnter(Collider otherObj)
	{
		if(otherObj.tag == "Flame")
			onFire = true;
	}

	public void OnDeath()
	{
		if(!dead)
		{
			eDead.Play();
			dead = true;

			// check if player killed
			if(health < 100.0f)
			{// add to kill count
			}
		}

		// let particles playout
		if(!eDead.isPlaying)
			Destroy(gameObject);
	}
}
