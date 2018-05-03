using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour {

    private RobotLoadout rolo;
    private SpriteRenderer spRender;

	// Use this for initialization
	void Start () {
        rolo = GetComponent<RobotLoadout>();
        spRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rolo.hitPoints <= 50)
        {
            spRender.color = new Color(0, 0, 0);

            if (collision.gameObject.tag == "FriendlyBullets")
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
        }
    }
}
