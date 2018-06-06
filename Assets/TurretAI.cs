using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{

    Transform player;
    [SerializeField] Transform firingArc;
    private RobotLoadout rolo;

    // Use this for initialization
    void Start()
    {
        rolo = GetComponent<RobotLoadout>();
        rolo.hitPoints[1] = 40;
        StartCoroutine(DefineRotation());
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
                bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, firingArc);
            }
            yield return new WaitForSeconds(rw.rangeWeaponRateOfFire + 2f);
        }
    }
}
