using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour {

    public enum Directions { Up, Down, Left, Right };
    [SerializeField] Directions directionEnum = Directions.Up;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        StopCoroutine(StopForce(other));

        switch (directionEnum)
        {
            case Directions.Up:
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0.5f));
                break;

            case Directions.Down:
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.5f));
                break;

            case Directions.Left:
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.5f, 0));
                break;

            case Directions.Right:
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0));
                break;

            default:
                Debug.Log("Conveyor Belt doesn't have a direction!!!");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(StopForce(other));
    }

    IEnumerator StopForce(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        
    }
}
