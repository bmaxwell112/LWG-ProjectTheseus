using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    Transform player;
    [SerializeField] Transform firingArc;
    private RobotLoadout rolo;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rolo = GetComponent<RobotLoadout>();
		StartCoroutine(SpawnBullets(rolo.loadout[3]));
	}
	
	// Update is called once per frame
	void Update () {

        DefineRotation();
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
            GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
            bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, firingArc, "Player", gameObject.tag);
            yield return new WaitForSeconds(rw.rangeWeaponRateOfFire);
            yield return new WaitForSeconds(0.5f);
            firing = false;
        }
    }
}
