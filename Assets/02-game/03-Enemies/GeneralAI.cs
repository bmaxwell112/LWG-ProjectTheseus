using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAI : MonoBehaviour {

	public enum Behaviour { random };
	[SerializeField] Behaviour behaviour = Behaviour.random;
	[SerializeField] float baseSpeed = 1;
	[SerializeField] float randomDirectionChangeSpeed = 2;	
	bool gameLoaded, stopped = true;
	Vector2 direction = Vector2.down;	
	Collider2D col;
	RoomGeneration myRoom;

	// Update is called once per frame
	void Update () {
		if (RoomManager.gameSetupComplete && !gameLoaded && !GameManager.paused)
		{
			LoadObject();
		}
		if (gameLoaded && !GameManager.paused)
		{
			if (stopped)
			{
				// Start coroutines back.
				stopped = false;
				StartCoroutine(RandomObjectMovement());					
			}
			if (behaviour == Behaviour.random)
			{				
				float xSpeed = direction.x * baseSpeed * Time.deltaTime;
				float ySpeed = direction.y * baseSpeed * Time.deltaTime;
				transform.position += new Vector3(xSpeed, ySpeed, 0);
			}
		}
		else if (gameLoaded && GameManager.paused)
		{
			// Stop any coroutines
			stopped = true;
		}
	}

	// Loads object when in the same room as object.
	private void LoadObject()
	{
		col = GetComponent<Collider2D>();
		myRoom = GetComponentInParent<RoomGeneration>();
		gameLoaded = true;		
	}

	IEnumerator RandomObjectMovement()
	{
		Vector2[] allDirection = new Vector2[4] {
			Vector2.up,
			Vector2.left,
			Vector2.right,
			Vector2.down
		};
		Vector2 oldDirection = direction;
		while (!stopped)
		{
			yield return new WaitForSeconds(randomDirectionChangeSpeed);			
			if (transform.position.x < myRoom.transform.position.x - 4f)
			{
				direction = Vector2.right;
				print("Moving Right");
			}
			else if (transform.position.x > myRoom.transform.position.x + 4f)
			{
				direction = Vector2.left;
				print("Moving Left");
			}
			else if (transform.position.y > myRoom.transform.position.y + 2.5)
			{
				direction = Vector2.down;
				print("Moving Down");
			}
			else if (transform.position.y < myRoom.transform.position.y - 2.5)
			{
				direction = Vector2.up;
				print("Moving Up");
			}
			else
			{
				bool goingBack = true;
				int newDirection = 0;
				while (goingBack)
				{
					newDirection = Random.Range(0, 4);
					if (allDirection[newDirection] != oldDirection)
					{
						goingBack = false;
					}
				}
				oldDirection = direction;
				direction = allDirection[newDirection];				
			}			
		}
	}

    public void EnemySetup(float bSpeed, float randSpeed, Behaviour behave)
    {
        baseSpeed = bSpeed;
        randomDirectionChangeSpeed = randSpeed;
        behaviour = behave;
    }

}
