using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmsAnim : MonoBehaviour {

	RobotLoadout roLo;
	Animator anim;
	RobotAnimationController animCont;
	//AnimatorOverrideController aoc;
	// 2 left 3 right
	[SerializeField] int armLocation;	
	RobotAttack attack;
	int rotationNum;
	bool isPlayer;
	protected List<KeyValuePair<AnimationClip, AnimationClip>> overrides;
	protected AnimatorOverrideController aoc;

	void Start()
	{
		attack = GetComponentInChildren<RobotAttack>();
		animCont = GetComponentInParent<RobotAnimationController>();
		anim = GetComponent<Animator>();
		roLo = GetComponentInParent<RobotLoadout>();
		isPlayer = GetComponentInParent<PlayerController>();
		aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);
		anim.runtimeAnimatorController = aoc;
		//print(aoc.overridesCount);
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
		attack.StartMovement();
		animCont.DetectFacingAndSetOrder();
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
			return InputCapture.triggerLeft;
		}
		else
		{
			return InputCapture.triggerRight;
		}
	}
	public void PlayerTracking()
	{
        bool blockDodge = Input.GetButton("BlockDodge");

        if (!blockDodge)
        {

            if (PlayerMelee())
            {
                anim.SetBool("attackingMelee", true);
                attack.StopMovementCheck();
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
	}

	public void MeleeAttack()
	{
		attack.MeleeAttack();
	}

	public void SwapWeapons(AnimatorOverrideController rac)
	{		
		//anim.runtimeAnimatorController = rac;		
		overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(rac.overridesCount);
		rac.GetOverrides(overrides);
		for (int i = 0; i < overrides.Count; ++i)
		{
			overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, overrides[i].Value);
		}		
		aoc.ApplyOverrides(overrides);
		anim.Update(0f);
	}
}
