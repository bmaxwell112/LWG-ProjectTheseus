using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour {

	[SerializeField] SpriteRenderer[] SpriteLoadOut;
	RobotLoadout roLo;
	[SerializeField] Transform firingArc;
	Transform player;
	Animator anim;
	public static bool UpdatePlayerSprites, layerAbovePlayer;
	enum Facing { upperLeft, left, lowerLeft, down, lowerRight, right, UpperRight, up }
	Facing currentFacing;
	// Use this for initialization
	void Start () {
		roLo = GetComponent<RobotLoadout>();		
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameManager.paused)
		{
			DetectFacingAndSetOrder();
			if (GetComponent<PlayerController>())
			{
				PlayerTracking();
			}
			else
			{
				EnemyTracking();
			}
			LayerChange();
		}
	}

	private void DetectFacingAndSetOrder()
	{
		if (firingArc.eulerAngles.z >= 22.5f && firingArc.eulerAngles.z < 67.5f)
		{
			// facing upper left
			currentFacing = Facing.upperLeft;
			int[] order = new int[] { 4, 7, 1, 4, 3 }; // head, left arm, right arm, left leg, right leg
			UpdateSprites(SpriteSetter(3, 2, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 67.5f && firingArc.eulerAngles.z < 112.5f)
		{
			// facing left
			currentFacing = Facing.left;
			int[] order = new int[] { 6, 7, 1, 4, 3 };
			UpdateSprites(SpriteSetter(2, 1, 4), order);
		}
		else if (firingArc.eulerAngles.z >= 112.5f && firingArc.eulerAngles.z < 157.5f)
		{
			// facing lower left
			currentFacing = Facing.lowerLeft;
			int[] order = new int[] { 6, 6, 4, 4, 3 };
			UpdateSprites(SpriteSetter(1, 0, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 157.5f && firingArc.eulerAngles.z < 202.5f)
		{
			// facing down
			int[] order = new int[] { 6, 4, 4, 4, 3 };
			currentFacing = Facing.down;
			UpdateSprites(SpriteSetter(0, 0, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 202.5f && firingArc.eulerAngles.z < 247.5f)
		{
			// facing lower right
			int[] order = new int[] { 6, 2, 7, 3, 4 };
			currentFacing = Facing.lowerRight;
			UpdateSprites(SpriteSetter(7, 0, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 247.5f && firingArc.eulerAngles.z < 292.5f)
		{
			// facing right
			int[] order = new int[] { 6, 1, 7, 3, 4 };
			currentFacing = Facing.right;
			UpdateSprites(SpriteSetter(6, 1, 4), order);
		}
		else if (firingArc.eulerAngles.z >= 292.5f && firingArc.eulerAngles.z < 337.5f)
		{
			// facing upper right
			int[] order = new int[] { 4, 2, 6, 3, 4 };
			currentFacing = Facing.UpperRight;
			UpdateSprites(SpriteSetter(5, 2, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 337.5f || firingArc.eulerAngles.z < 22.5f)
		{
			// facing up
			int[] order = new int[] { 4, 3, 3, 4, 3 };
			currentFacing = Facing.up;
			UpdateSprites(SpriteSetter(4, 2, 3), order);
		}
	}

	private void EnemyTracking()
	{
		anim.SetInteger("facing", (int)currentFacing);
		if (roLo.loadout[2].itemType == ItemType.range)
		{
			anim.SetBool("leftRangeAttack", true);
		}
		if (roLo.loadout[3].itemType == ItemType.range)
		{
			anim.SetBool("rightRangeAttack", true);
		}
		if (roLo.attackLeft && roLo.loadout[2].itemType == ItemType.melee)
		{
			anim.SetBool("leftAttack", true);
		}
		else
		{
			anim.SetBool("leftAttack", false);
		}
		if (roLo.attackRight && roLo.loadout[3].itemType == ItemType.melee)
		{
			anim.SetBool("righAttack", true);
		}
		else
		{
			anim.SetBool("righAttack", false);
		}
		if (roLo.walk)
		{
			anim.SetInteger("action", 1);
		}
		else
		{
			anim.SetInteger("action", 0);
		}
	}

	private void PlayerTracking()
	{
		anim.SetInteger("facing", (int)currentFacing);
		if (InputCapture.hThrow != 0 || InputCapture.vThrow != 0)
		{
			anim.SetInteger("action", 1);
		}
		else
		{
			anim.SetInteger("action", 0);
		}
		if (currentFacing == Facing.down || currentFacing == Facing.up)
		{
			anim.SetFloat("speed", InputCapture.vThrow);
		}
		else
		{
			anim.SetFloat("speed", InputCapture.hThrow);
		}
		if (InputCapture.fireRightDown && roLo.loadout[3].itemType == ItemType.melee)
		{
			anim.SetBool("rightAttack", true);
		}
		if (InputCapture.fireLeftDown && roLo.loadout[2].itemType == ItemType.melee)
		{
			anim.SetBool("leftAttack", true);
		}
		if (InputCapture.firingLeft && roLo.loadout[2].itemType == ItemType.range)
		{
			anim.SetBool("leftRangeAttack", true);
		}
		else if (!InputCapture.firingLeft && roLo.loadout[2].itemType == ItemType.range)
		{
			anim.SetBool("leftRangeAttack", false);
		}
		if (InputCapture.firingRight && roLo.loadout[3].itemType == ItemType.range)
		{
			anim.SetBool("rightRangeAttack", true);
		}
		else if (!InputCapture.firingRight && roLo.loadout[3].itemType == ItemType.range)
		{
			anim.SetBool("rightRangeAttack", false);
		}
	}



	Sprite[] SpriteSetter(int bodyAndHead, int leg, int foot)
	{
		return new Sprite[] {
				roLo.loadout[0].itemSprite[bodyAndHead],
				roLo.loadout[1].itemSprite[bodyAndHead],
				roLo.loadout[2].itemSprite[2],
				roLo.loadout[2].itemSprite[0],
				roLo.loadout[2].itemSprite[1],
				roLo.loadout[3].itemSprite[2],
				roLo.loadout[3].itemSprite[0],
				roLo.loadout[3].itemSprite[1],
				roLo.loadout[4].itemSprite[foot],
				roLo.loadout[4].itemSprite[leg],
				roLo.loadout[4].itemSprite[foot],
				roLo.loadout[4].itemSprite[leg],
				};
	}

	void UpdateSprites(Sprite[] sprites, int[] order)
	{
		for (int i = 0; i < SpriteLoadOut.Length; i++)
		{
			SpriteLoadOut[i].sprite = sprites[i];
			switch (i)
			{
				case 0:
					SpriteLoadOut[i].sortingOrder = order[0];
					break;
				case 2:
					SpriteLoadOut[i].sortingOrder = order[1];
					SpriteLoadOut[i + 1].sortingOrder = order[1] - 1;
					SpriteLoadOut[i + 2].sortingOrder = order[1];
					break;
				case 5:
					SpriteLoadOut[i].sortingOrder = order[2];
					SpriteLoadOut[i + 1].sortingOrder = order[2] - 1;
					SpriteLoadOut[i + 2].sortingOrder = order[2];
					break;
				case 8:
					SpriteLoadOut[i].sortingOrder = order[3];
					SpriteLoadOut[i + 1].sortingOrder = order[3]-1;
					break;
				case 10:
					SpriteLoadOut[i].sortingOrder = order[4];
					SpriteLoadOut[i + 1].sortingOrder = order[4]-1;
					break;
				default:
					break;
			}
		}
	}

	public void DoneAttacking()
	{
		anim.SetBool("rightAttack", false);
		anim.SetBool("rightRangeAttack", false);
	}
	public void DoneAttackingLeft()
	{
		anim.SetBool("leftAttack", false);
		anim.SetBool("leftRangeAttack", false);
	}

	public void LayerChange()
	{
		if (!GetComponent<PlayerController>())
		{
			if (!player)
			{
				if (FindObjectOfType<PlayerController>())
				{
					player = FindObjectOfType<PlayerController>().transform;
				}
				else
				{
					return;
				}
			}
			
			if (transform.position.y < player.transform.position.y && layerAbovePlayer)
			{
				foreach (SpriteRenderer sr in SpriteLoadOut)
				{
					sr.sortingLayerName = "BelowPlayer";
				}
				layerAbovePlayer = false;
			}
			else if (transform.position.y > player.transform.position.y && !layerAbovePlayer)
			{
				foreach (SpriteRenderer sr in SpriteLoadOut)
				{
					sr.sortingLayerName = "AbovePlayer";
				}
				layerAbovePlayer = true;
			}
		}
	}
}
