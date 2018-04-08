using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	Transform player;
	bool knockback, attacking;
	[SerializeField] float knockbackDistance;	
	[SerializeField] GameObject drop;
	[SerializeField] LayerMask playerMask;
	RobotLoadout roLo;
	int attack;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		roLo = GetComponent<RobotLoadout>();
		BasicEnemySetup();
		attack = Mathf.RoundToInt((roLo.loadout[(int)ItemLoc.rightArm].itemValue + roLo.loadout[(int)ItemLoc.leftArm].itemValue) / 2);
		roLo.hitPoints = Mathf.RoundToInt(roLo.hitPoints / 2);
	}

	private void BasicEnemySetup()
	{
		Database db = FindObjectOfType<Database>();
		roLo.InitializeLoadout(
			db.RandomItemOut(ItemLoc.head),
			db.RandomItemOut(ItemLoc.body),
			db.RandomItemOut(ItemLoc.leftArm),
			db.RandomItemOut(ItemLoc.rightArm),
			db.RandomItemOut(ItemLoc.legs),
			db.RandomItemOut(ItemLoc.back),
			db.RandomItemOut(ItemLoc.core)
			);
	}

	// Update is called once per frame
	void Update ()
	{		
		if (player)
		{
			DefineRotation();
			EnemyMovement();
		}
	}
	void FixedUpdate()
	{
		if (!attacking)
		{
			EnemyAttackCheck();
		}
	}

	private void EnemyMovement()
	{
		if (!knockback)
		{
			transform.position += transform.up * roLo.loadout[(int)ItemLoc.legs].itemValue * Time.deltaTime;
		}
	}

	private void EnemyAttackCheck()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.55f, playerMask);
		if (hit.collider != null)
		{
			attacking = true;
			Invoke("EnemyAttack", 0.5f);
		}
	}
	private void EnemyAttack()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.55f, playerMask);
		if (hit.collider != null)
		{
			hit.collider.gameObject.GetComponent<RobotLoadout>().TakeDamage(attack, Color.red, Color.green);
		}
		attacking = false;
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();

		transform.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
	}

	public void EnemyDrop()
	{
		int rand = Random.Range(0, 100);
		if (rand <= 33)
		{			
			int rand2 = Random.Range(0, 7);
			GameObject tempDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
			print(rand2);
			tempDrop.GetComponent<Drops>().databaseItemID = roLo.loadout[rand2].itemID;
		}
	}

	public IEnumerator EnemyKnockback()
	{
		Vector3 endLocation = transform.position - transform.up;
		float dist = Vector3.Distance(endLocation, transform.position);
		float startTime = Time.time;
		knockback = true;
		while (dist > 0.2f)
		{
			transform.position = Vector3.Lerp(transform.position, endLocation, (Time.time - startTime) / 1f);
			dist = Vector3.Distance(endLocation, transform.position);
			yield return null;
		}
		knockback = false;
	}
}
