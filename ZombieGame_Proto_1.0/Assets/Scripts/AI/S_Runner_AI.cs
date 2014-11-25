using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(NavMeshAgent))]
public class S_Runner_AI : MonoBehaviour 
{
	// movement
	public float moveSpeed = 2.0f;
	public float attackRange = 1.0f;

	// targets
	private Vector3 targetPos;
	private Vector3 playerPos;
	private Vector3 carPos;
	private NavMeshAgent navAgent;
	private S_Zombie_Health sHealth;
	private int coin;


	// Use this for initialization
	void Start () 
	{
		carPos = S_PosTracker.carPos;
		playerPos = S_PosTracker.playerPos;
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.speed = moveSpeed;
		sHealth = GetComponent<S_Zombie_Health>();

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
			////////// run attack code here////////////////////////////////////////////////////

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
