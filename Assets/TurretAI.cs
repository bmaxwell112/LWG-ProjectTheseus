using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    Transform player;
    [SerializeField] Transform firingArc;
    private RobotLoadout rolo;

    bool firing, localCooldown;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rolo = GetComponent<RobotLoadout>();
    }
	
	// Update is called once per frame
	void Update () {

        DefineRotation();
        RangedAttack();
	}

    private void DefineRotation()
    {
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
    }

    public void RangedAttack()
    {
        //print("Ranged Attack");
        if (!localCooldown)
        {
            //print("Cool down false");
            StartCoroutine(SpawnBullets(rolo.loadout[3]));
        }
    }

    IEnumerator SpawnBullets(Item weapon)
    {
        //print("Start Co-routine");
        RangedWeapon rw = Database.instance.ItemsRangedWeapon(weapon);
        localCooldown = true;
        firing = true;
        while (firing)
        {
            GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
            bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, firingArc, "Player", gameObject.tag);
            yield return new WaitForSeconds(rw.rangeWeaponRateOfFire);
            yield return new WaitForSeconds(0.5f);
            firing = false;
        }
        //print("End Co-routine");
        localCooldown = false;
    }
}
