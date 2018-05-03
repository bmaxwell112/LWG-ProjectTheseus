using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {

    Transform player;
    [SerializeField] Transform firingArc;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DefineRotation()
    {
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();

        firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
    }
}
