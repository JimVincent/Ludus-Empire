using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(NavMeshAgent))]
public class S_Runner_AI : MonoBehaviour 
{
	// movement
	public float moveSpeed = 2.0f;
	public float attackRange = 1.0f;
	public float attackRate = 2.0f;
	public Collider attackArm;

	// targets
	private Vector3 targetPos;
	private Vector3 playerPos;
	private Vector3 carPos;
	private NavMeshAgent navAgent;
	private S_Zombie_Health sHealth;
	private int coin;

	// timers
	private float attackTimer = 0.0f;

	// Use this for initialization
	void Start () 
	{
		carPos = S_PosTracker.carPos;
		playerPos = S_PosTracker.playerPos;
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = moveSpeed;
		sHealth = GetComponent<S_Zombie_Health>();

		attackArm.enabled = false;
		attackArm.isTrigger = true;

		// random target 50/50
		coin = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update () 
	{
		// always know playerPos
		playerPos = S_PosTracker.playerPos;

		if(coin == 1)
			targetPos = playerPos;
		else
			targetPos = carPos;

		// hunt down target
		navAgent.SetDestination(targetPos);

		// switch target to player if taken damage
		if(targetPos == carPos && sHealth.health < 100.0f)
			targetPos = playerPos;

		// closer enough to attack
		if(Vector3.Distance(transform.position, targetPos) <= attackRange)
		{
			attackTimer += Time.deltaTime;
			if(attackTimer > attackRate)
			{
				attackTimer = 0.0f;
				attackArm.enabled = true;
			}
			else
				attackArm.enabled = false;

		}
	}

	void OnTriggerEnter(Collider otherObj)
	{
		// target player
		if(otherObj.tag == "SoundWave")
			targetPos = playerPos;
	}

	void OnCollisionEnter(Collision otherObj)
	{
		// target player
		if(otherObj.collider.tag == "Player")
			targetPos = playerPos;
	}
}
