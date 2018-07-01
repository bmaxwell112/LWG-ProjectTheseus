using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{

    Transform player;
    [SerializeField] Transform firingArc;
    private RobotLoadout rolo;
	[SerializeField] Vector2[] bulletSpawnLocations;
	Vector3 bulletSpawnLocation;
	Facing turretFacing;
	[SerializeField] Sprite[] turretSprites;
	[SerializeField] SpriteRenderer turretBody;

    // Use this for initialization
    void Start()
    {
        rolo = GetComponent<RobotLoadout>();
		rolo.InitializeLoadout(
			new Item(),
			Database.instance.items[1],
			new Item(),
			Database.instance.items[18],
			new Item(),
			new Item(),
			new Item()
			);
		StartCoroutine(DefineRotation());

		StartCoroutine(SpawnBullets(rolo.loadout[3]));
	}

    // Update is called once per frame
    void Update()
    {
		
	}

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rolo = GetComponent<RobotLoadout>();
        StartCoroutine(SpawnBullets(rolo.loadout[3]));
    }

    IEnumerator DefineRotation()
    {
        while (true)
        {
            Vector3 diff = player.transform.position - transform.position;
            diff.Normalize();
            // delay to rotate in seconds
            yield return new WaitForSeconds(0.5f);
            Quaternion toLoc = Quaternion.Euler(MovementFunctions.LookAt2D(transform, diff.x, diff.y));
            Quaternion fromLoc = firingArc.rotation;
            // Speed of rotation
            while (firingArc.rotation != toLoc)
            {
				if (RoomManager.allActive)
                {
					TurretRotationAnimationAndBulletSpawn();
					firingArc.rotation = Quaternion.Lerp(fromLoc, toLoc, Time.time * 0.5f);
                    float angle = Quaternion.Angle(firingArc.rotation, toLoc);
                    // if angle diffence is less than 5 set it to end location to break loop.
                    if (angle < 5)
                    {
                        firingArc.rotation = toLoc;
                    }
					
				}
                yield return null;
            }
            yield return null;
        }
    }

    IEnumerator SpawnBullets(Item weapon)
    {
        //print("Start Co-routine");
        RangedWeapon rw = Database.instance.ItemsRangedWeapon(weapon);
        while (true)
        {
            if (RoomManager.allActive)
            {
                GameObject bullet = Instantiate(Resources.Load("bulletEnemy", typeof(GameObject))) as GameObject;
                bullet.GetComponent<BulletWeapon>().BulletSetup(rw, bulletSpawnLocation, firingArc);
            }
            yield return new WaitForSeconds(rw.rangeWeaponRateOfFire + 2f);
        }
    }

	void TurretRotationAnimationAndBulletSpawn()
	{
		if (RobotFunctions.FacingDirection(firingArc) != turretFacing)
		{
			turretFacing = RobotFunctions.FacingDirection(firingArc);
			switch (turretFacing)
			{
				case Facing.upperLeft:
					turretBody.sprite = turretSprites[0];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[0]);
					break;
				case Facing.left:
					turretBody.sprite = turretSprites[1];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[1]);
					break;
				case Facing.lowerLeft:
					turretBody.sprite = turretSprites[2];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[2]);
					break;
				case Facing.down:
					turretBody.sprite = turretSprites[3];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[3]);
					break;
				case Facing.lowerRight:
					turretBody.sprite = turretSprites[4];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[4]);
					break;
				case Facing.right:
					turretBody.sprite = turretSprites[5];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[5]);
					break;
				case Facing.upperRight:
					turretBody.sprite = turretSprites[0];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[6]);
					break;
				case Facing.up:
					turretBody.sprite = turretSprites[0];
					bulletSpawnLocation = SpawnLocationFromOffser(bulletSpawnLocations[7]);
					break;
			}
		}
	}

	Vector3 SpawnLocationFromOffser(Vector2 offset)
	{
		return new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, 0);		
	}
}
