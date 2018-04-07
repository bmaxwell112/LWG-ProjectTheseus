using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	Transform player;
	bool knockback, attacking;
	[SerializeField] float speed, knockbackDistance;	
	[SerializeField] GameObject drop;
	[SerializeField] LayerMask playerMask;
	Color orange = new Color(0.82f, 0.55f, 0.16f);
	int hitPoints, attack;
	[SerializeField] Item[] loadout = new Item[7];
	Database db;

	void Start()
	{
		db = FindObjectOfType<Database>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		loadout[0] = db.RandomItemOut(ItemLoc.head);
		loadout[1] = db.RandomItemOut(ItemLoc.body);
		loadout[2] = db.RandomItemOut(ItemLoc.leftArm);
		loadout[3] = db.RandomItemOut(ItemLoc.rightArm);
		loadout[4] = db.RandomItemOut(ItemLoc.legs);
		loadout[5] = db.RandomItemOut(ItemLoc.back);
		loadout[6] = db.RandomItemOut(ItemLoc.core);
		hitPoints = Mathf.RoundToInt(loadout[1].itemHitpoints/4);
		attack = Mathf.RoundToInt((loadout[2].itemValue + loadout[2].itemValue) / 4);
		speed = loadout[4].itemValue - 0.5f;
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
			transform.position += transform.up * speed * Time.deltaTime;
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
			hit.collider.gameObject.GetComponent<PlayerController>().TakeDamage(attack);
		}
		attacking = false;
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();

		transform.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
	}

	public void TakeDamage(int damage)
	{
		hitPoints -= damage;
		StartCoroutine(ChangeColor(Color.red, 0));
		StartCoroutine(ChangeColor(orange, 0.25f));
		StartCoroutine(EnemyKnockback());
		if (hitPoints <= 0)
		{
			EnemyDrop();
			Destroy(gameObject);
		}
	}

	private void EnemyDrop()
	{
		int rand = Random.Range(0, 100);
		if (rand <= 33)
		{			
			int rand2 = Random.Range(0, 7);
			GameObject tempDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
			print(rand2);
			tempDrop.GetComponent<Drops>().databaseItemID = loadout[rand2].itemID;
		}
	}

	IEnumerator EnemyKnockback()
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

	IEnumerator ChangeColor(Color color, float t)
	{
		SpriteRenderer body = transform.Find("Body").GetComponent<SpriteRenderer>();
		yield return new WaitForSeconds(t);
		body.color = color;
	}
}
