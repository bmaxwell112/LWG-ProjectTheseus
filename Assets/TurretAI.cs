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

    }

    // Update is called once per frame
    void Update()
    {

        DefineRotation();
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rolo = GetComponent<RobotLoadout>();
        StartCoroutine(SpawnBullets(rolo.loadout[3]));
    }

    private void DefineRotation()
    { 
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
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
                bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, firingArc, "Player", gameObject.tag);
            }
            yield return new WaitForSeconds(rw.rangeWeaponRateOfFire + 0.5f);
        }
    }
}