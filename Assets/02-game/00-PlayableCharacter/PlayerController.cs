using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] LayerMask drop, enemyMask;
	[SerializeField] GameObject bullets;
	[SerializeField] RobotAttack leftArm, rightArm;
	// DETERMINE HOW THIS IS SET
	[SerializeField] float dodgeCooldown = 1;
	NotificationsPanel np;
	public Transform firingArc;
	RobotLoadout roLo;
	PlayerSpecial special;
	Vector3 rotation;
	bool fireLeft, fireRight, blockDodge, stationary, dodgeAvailable, playerDead;
    public bool activeDodge, activeBlock;
    Rigidbody2D rb;

	void Start()
	{		
		roLo = GetComponent<RobotLoadout>();
		special = GetComponent<PlayerSpecial>();
		np = FindObjectOfType<NotificationsPanel>();
        rb = GetComponent<Rigidbody2D>();		
		PlayerSpawn();
		dodgeAvailable = true;
	}

	private void PlayerSpawn()
	{
		Database db = Database.instance;
		if (!GameManager.instance.playerAlive)
		{
			roLo.InitializeLoadout(db.items[0], db.items[1], db.items[2], db.items[3], db.items[4], db.items[5], db.items[6]);
			GameManager.instance.playerAlive = true;			
		}
		else
		{
			roLo.InitializeLoadout(
				GameManager.playerLoadout[0],
				GameManager.playerLoadout[1],
				GameManager.playerLoadout[2],
				GameManager.playerLoadout[3],
				GameManager.playerLoadout[4],
				GameManager.playerLoadout[5],
				GameManager.playerLoadout[6]
				);
			foreach (Item gear in roLo.loadout)
			{
				if (gear.itemSpecial)
				{
					special.ActivateSpecialPassive(gear);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (RoomManager.gameSetupComplete && !roLo.dead)
		{
			if (GameManager.mouseInput)
			{
				InputCapture.MouseAim(MouseDistanceFromPlayer());
			}
			else
			{
				InputCapture.ControllerAim();
			}

			if (!GameManager.paused && !ItemWheel.active)
			{
                BlockDodgeCheck();
                if (!activeBlock && !activeDodge && !roLo.stopped)
                {
                    MovementCheck();
                    AimAndFireCheck();
                }
                
			}
		}
		if (roLo.dead && !playerDead)
		{
			Invoke("LoadToHub", 2);
			playerDead = true;
		}
	}

	void LoadToHub()
	{
		LevelManager.LOADLEVEL("03c Subscribe");
	}
	
	// Movement
    public void BlockDodgeCheck()
    {
        float xDodge = InputCapture.hThrow;
        float yDodge = InputCapture.vThrow;

		bool blockDodge = InputCapture.block;

        activeDodge = false;
		activeBlock = false;

		//stationary is found in the movement check
		if (blockDodge && stationary)
		{
			activeBlock = true;
			if (xDodge != 0 || yDodge != 0)
			{
				activeBlock = false;
				activeDodge = true;
				print("block into dodge");
			}			
		}

        if(blockDodge && !stationary && !activeDodge)
        {
            //tie running dodge into animation
        }

        if (activeDodge)
        {

			if (dodgeAvailable && InputCapture.JoystickOverThreshold(0.5f))
            {
                CalcDodge();
                dodgeAvailable = false;
                activeDodge = false;
                print("Dodging");
                //set this to have a proper cooldown
            }
        }
    }
	public void CalcDodge()
    {
		Facing moveDir = InputCapture.JoystickDirection();

		float dodgeRoll = 200f;
		print(moveDir);
		if (moveDir == Facing.right)
        {
            //right dodge
            StartCoroutine(PerformDodge(new Vector2(dodgeRoll, 0)));
        }
        if (moveDir == Facing.upperRight)
        {
            //upright dodge
            StartCoroutine(PerformDodge(new Vector2(dodgeRoll, dodgeRoll)));
        }
        if (moveDir == Facing.lowerRight)
        {
            //downright dodge
            StartCoroutine(PerformDodge(new Vector2(dodgeRoll, -dodgeRoll)));
        }
        if (moveDir == Facing.left)
        {
            //left dodge
            StartCoroutine(PerformDodge(new Vector2(-dodgeRoll, 0)));
        }
        if (moveDir == Facing.upperLeft)
        {
            //upleft dodge
            StartCoroutine(PerformDodge(new Vector2(-dodgeRoll, dodgeRoll)));
        }
        if (moveDir == Facing.lowerLeft)
        {
            //downleft dodge
            StartCoroutine(PerformDodge(new Vector2(-dodgeRoll, -dodgeRoll)));
        }
        if (moveDir == Facing.up)
        {
            //up dodge
            StartCoroutine(PerformDodge(new Vector2(0, dodgeRoll)));
        }
        if (moveDir == Facing.down)
        {
            //down dodge
            StartCoroutine(PerformDodge(new Vector2(0, -dodgeRoll)));
        }
    }
	IEnumerator PerformDodge(Vector2 dodgeForce)
    {
        rb.AddForce(dodgeForce);
        yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector2.zero;
		dodgeAvailable = true;
		//yield return new WaitForSeconds(dodgeCooldown);
	}
	private void MovementCheck()
	{
		float speed = 1.5f;
		// Sets players speed
		if (roLo.hitPoints[(int)ItemLoc.legs] > 0)
		{
			speed = roLo.loadout[(int)ItemLoc.legs].itemSpeed + roLo.loadout[(int)ItemLoc.body].itemSpeed;
		}
		// applies movement
		float xSpeed = InputCapture.hThrow * speed * Time.deltaTime;

		float ySpeed = InputCapture.vThrow * speed * Time.deltaTime;
		transform.position += new Vector3(xSpeed, ySpeed, transform.position.z);

		if (xSpeed == 0 && ySpeed == 0)
		{
			stationary = true;
		}
		else
		{
			stationary = false;
		}
	}

	// Attacking
	private void AimAndFireCheck()
	{
            if ((InputCapture.hAim > 0.5f || InputCapture.hAim < -0.5f) || (InputCapture.vAim > 0.5f || InputCapture.vAim < -0.5f))
            {
                rotation = MovementFunctions.LookAt2D(transform, InputCapture.hAim, InputCapture.vAim);
            }
            firingArc.eulerAngles = rotation;
            // ATTACKING LEFT ARM
            if (InputCapture.triggerLeft && !fireLeft)
            {
                if (roLo.loadout[2].itemType == ItemType.range && roLo.power[2] > 0)
                {
                    leftArm.RangedAttack(roLo.loadout[2]);
                }
                fireLeft = true;
            }
            else if (!InputCapture.triggerLeft)
            {
                fireLeft = false;
            }
            // ATTACKING RIGHT ARM
            if (InputCapture.triggerRight && !fireRight)
            {
                if (roLo.loadout[3].itemType == ItemType.range && roLo.power[3] > 0)
                {
                    rightArm.RangedAttack(roLo.loadout[3]);
                }
                fireRight = true;
            }
            else if (!InputCapture.triggerRight)
            {
                fireRight = false;
            }
            rightArm.GetComponent<RobotAttack>().FiringCheck(fireRight);
            leftArm.GetComponent<RobotAttack>().FiringCheck(fireLeft);
	}

	// For Mouse Controls
	Vector2 MouseDistanceFromPlayer()
	{
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;

		Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
		Camera cam = FindObjectOfType<Camera>();
		Vector2 worldPos = cam.ScreenToWorldPoint(weirdTriplet);

		Vector2 distFromPlayer = new Vector2(transform.position.x - worldPos.x, transform.position.y - worldPos.y);
		return distFromPlayer;
	}		
}
