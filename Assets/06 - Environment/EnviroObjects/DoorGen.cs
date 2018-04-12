using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour {

    public bool doorWall;
    SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
        RandomizeWall();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SpawnDoor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void RandomizeWall()
    {
        if(Random.value > 0.5)
        {
            doorWall = true;
        }
        else
        {
            doorWall = false;
        }
    }

    void SpawnDoor()
    {
        if (doorWall)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.red;
        }
    }
}
