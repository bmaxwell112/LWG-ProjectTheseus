using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmsAnim : MonoBehaviour {

	RobotLoadout roLo;
	Animator anim;
	RobotAnimationController animCont;
	// 2 left 3 right
	[SerializeField] int armLocation;
	RobotAttack attack;
	int rotationNum;
	bool isPlayer;

	void Start()
	{
		attack = GetComponentInChildren<RobotAttack>();
		animCont = GetComponentInParent<RobotAnimationController>();
		anim = GetComponent<Animator>();
		roLo = GetComponentInParent<RobotLoadout>();
		isPlayer = GetComponentInParent<PlayerController>();
	}

	void Update()
	{
		if (isPlayer)
		{
			PlayerTracking();
		}
		else
		{
			EnemyTracking();
		}
		if (anim.GetBool("attackingMelee") && isPlayer)
		{
			if ((int)animCont.currentFacing != rotationNum)
			{
				DoneAttacking();
			}
		}
		else
		{
			rotationNum = (int)animCont.currentFacing;
		}
	}

	public void DoneAttacking()
	{
		anim.SetBool("attackingMelee", false);
		anim.SetBool("attackingRange", false);
		attack.meleeAttacking = false;
	}
	

	public void EnemyTracking()
	{
		anim.SetInteger("facing", (int)animCont.currentFacing);
		if (roLo.loadout[armLocation].itemType == ItemType.range && (roLo.power[armLocation] > 0 && roLo.hitPoints[armLocation] > 0))
		{
			anim.SetBool("attackingRange", true);
		}
		else
		{
			anim.SetBool("attackingRange", false);
		}		
		if (roLo.attackLeft && (roLo.loadout[armLocation].itemType == ItemType.melee || (roLo.power[armLocation] <= 0 || roLo.hitPoints[armLocation] <= 0)))
		{
			anim.SetBool("attackingMelee", true);
		}
		else
		{
			anim.SetBool("attackingMelee", false);
		}
	}

	bool PlayerMelee()
	{
		anim.SetInteger("facing", (int)animCont.currentFacing);
		if (armLocation == 2)
		{
			return InputCapture.fireLeftDown;
		}
		else
		{
			return InputCapture.fireRightDown;
		}
	}
	bool PlayerRanged()
	{
		if (armLocation == 2)
		{
			return InputCapture.firingLeft;
		}
		else
		{
			return InputCapture.firingRight;
		}
	}
	public void PlayerTracking()
	{
		if (PlayerMelee() && (roLo.loadout[armLocation].itemType == ItemType.melee || (roLo.power[armLocation] <= 0 || roLo.hitPoints[armLocation] <= 0)))
		{
			anim.SetBool("attackingMelee", true);
		}
		if (PlayerRanged() && roLo.loadout[armLocation].itemType == ItemType.range && (roLo.power[armLocation] > 0 && roLo.hitPoints[armLocation] > 0))
		{
			print("doing this");
			anim.SetBool("attackingRange", true);
		}
		else if ((!PlayerRanged() && roLo.loadout[armLocation].itemType == ItemType.range) || (roLo.power[armLocation] <= 0 || roLo.hitPoints[armLocation] <= 0))
		{
			anim.SetBool("attackingRange", false);
		}
	}

	public void MeleeAttack()
	{
		attack.MeleeAttack();
	}
}
