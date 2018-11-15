using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour {

    public int configNumber;
    bool alreadySpawned;
    private RoomManager wController;

    // Use this for initialization
    void Start () {
        alreadySpawned = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetConfigurationNumber(int cNumber)
    {
        //This will run during room generation
        configNumber = cNumber;
    }

    void RunConfiguration()
    {
        if(RoomManager.gameSetupComplete && !alreadySpawned && isActiveAndEnabled)
        {


        if(configNumber == 0)
        {
            alreadySpawned = true;
        }
        if (configNumber == 1)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);

            alreadySpawned = true;
        }
        if (configNumber == 2)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);

            alreadySpawned = true;
        }
        if(configNumber == 3)
        {
            //If there are issues it's probably this one
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 4)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 5)
        {
            wController = FindObjectOfType<RoomManager>();

            //walls
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);

            //enemies
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 6)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, transform);

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.17f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.17f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber ==7)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 8)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 9)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 10)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 11)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.turretAI, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 4.36f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 4.36f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 12)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.turretAI, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 13)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.turretAI, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.turretAI, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 14)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 5.45f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 5.45f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x + 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 15)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.turretAI, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 16)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 17)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x + 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x - 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 18)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x - 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 3.27f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x + 5.45f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 19)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

            Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y - 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 3.27f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 4.36f, transform.position.y - 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x + 5.45f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.internalWall, new Vector3(transform.position.x - 5.45f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.hole, new Vector3(transform.position.x + 6.54f, transform.position.y + 0, 0), Quaternion.identity, transform);
            Instantiate(wController.hole, new Vector3(transform.position.x - 6.54f, transform.position.y + 0, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 20)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 21)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 22)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 23)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);

            Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if(configNumber == 24)
        {
            wController = FindObjectOfType<RoomManager>();

            Instantiate(wController.turretAI, new Vector3(transform.position.x + 6.54f, transform.position.y + 0f, 0), Quaternion.identity, transform);
            Instantiate(wController.turretAI, new Vector3(transform.position.x - 6.54f, transform.position.y + 0f, 0), Quaternion.identity, transform);

            Instantiate(wController.pillar, new Vector3(transform.position.x - 5.45f, transform.position.y + .55f, 0), Quaternion.identity, transform);
            Instantiate(wController.pillar, new Vector3(transform.position.x + 5.45f, transform.position.y - .55f, 0), Quaternion.identity, transform);
            alreadySpawned = true;
        }
        if (configNumber == 25)
        {
            //THIS NEEDS TO BE THE ENDING ROOM
            alreadySpawned = true;
        }

        }
    }


}
