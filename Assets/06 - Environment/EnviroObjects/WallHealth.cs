using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour {

    private RobotLoadout rolo;
    private SpriteRenderer spRender;

	// Use this for initialization
	void Start () {
        rolo = GetComponent<RobotLoadout>();
        rolo.hitPoints[1] = 100;
        spRender = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        WallMaint();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rolo.hitPoints[1] <= 50)
        {
            if (collision.gameObject.tag == "FriendlyBullets")
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
        }
    }

    void WallMaint()
    {
        if (rolo.hitPoints[1] <= 50)
        {
            rolo.hitPoints[1] = 49;
            spRender.color = new Color(0, 0, 0);
        }
    }
}
