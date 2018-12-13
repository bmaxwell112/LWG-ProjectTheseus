using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGravity : MonoBehaviour {

    private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.gravityScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
