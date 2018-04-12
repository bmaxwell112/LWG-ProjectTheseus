using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour {

    public bool doorWall;
    SpriteRenderer spriteRenderer;
	[SerializeField] GameObject open, close;

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
			if (close)
			{
				close.SetActive(false);
			}
			else
			{
				spriteRenderer.color = Color.green;
			}
		}
        else
        {
			if (open)
			{
				open.SetActive(false);
			}
			else
			{
				spriteRenderer.color = Color.red;
			}
		}
    }
}
