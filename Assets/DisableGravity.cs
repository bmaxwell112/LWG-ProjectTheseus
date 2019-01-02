using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGravity : MonoBehaviour {
    // !! DO NOT USE THIS FOR ANYTHING THAT ISN'T JUNK, PERIPHETES LOOKS FOR THIS !!
    // This has specific code for despawning junk, please do not forget this if you reuse this

    private bool destroyMe;
    private Vector2 movementThreshold;
    private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.gravityScale = 0;
        movementThreshold = new Vector2(0.5f, 0.5f);
        destroyMe = false;
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Waiting());

        if(destroyMe)
        {
            GameObject.Destroy(this);
        }
	}

    IEnumerator Waiting()
    {
        if(rBody.velocity.x < movementThreshold.x && rBody.velocity.y < movementThreshold.y)
        {
            yield return new WaitForSeconds(40);
            destroyMe = true;
        }

    }
}
