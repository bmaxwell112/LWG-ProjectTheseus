using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour {

	[SerializeField] SpriteRenderer[] SpriteLoadOut;
	RobotLoadout roLo;
	[SerializeField] Transform firingArc;
	Transform player;
	[SerializeField] Animator leftArm;
	Animator anim;
	Animator[] arms;
	public static bool UpdatePlayerSprites;	
	public Facing currentFacing;	
	bool isPlayer, attacking, layerAbovePlayer;
	// Use this for initialization
	void Start () {
		roLo = GetComponent<RobotLoadout>();		
		anim = GetComponent<Animator>();
		arms = GetComponentsInChildren<Animator>();
		isPlayer = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!GameManager.paused && RoomManager.allActive && !roLo.dead)
		{
			if (arms[1].GetInteger("action") == 0 || arms[1].GetInteger("action") == 3)
			{
				if (arms[2].GetInteger("action") == 0 || arms[2].GetInteger("action") == 3)
				{
					DetectFacingAndSetOrder();
				}
			}
			if (isPlayer)
			{
				PlayerTracking();
			}
			else
			{
				EnemyTracking();
			}
			LayerChange();
		}
		if (roLo.dead)
		{
			anim.SetInteger("action", -1);
			arms[1].SetInteger("action", -1);
			arms[2].SetInteger("action", -1);
		}
	}

	public void DetectFacingAndSetOrder()
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
			int[] order = new int[] { 6, 8, 1, 6, 3 };
			UpdateSprites(SpriteSetter(2, 1, 4), order);
		}
		else if (firingArc.eulerAngles.z >= 112.5f && firingArc.eulerAngles.z < 157.5f)
		{
			// facing lower left
			currentFacing = Facing.lowerLeft;
			int[] order = new int[] { 6, 7, 4, 4, 3 };
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
			int[] order = new int[] { 6, 2, 8, 3, 4 };
			currentFacing = Facing.lowerRight;
			UpdateSprites(SpriteSetter(7, 0, 3), order);
		}
		else if (firingArc.eulerAngles.z >= 247.5f && firingArc.eulerAngles.z < 292.5f)
		{
			// facing right
			int[] order = new int[] { 6, 1, 8, 3, 4 };
			currentFacing = Facing.right;
			UpdateSprites(SpriteSetter(6, 1, 4), order);
		}
		else if (firingArc.eulerAngles.z >= 292.5f && firingArc.eulerAngles.z < 337.5f)
		{
			// facing upper right
			int[] order = new int[] { 4, 2, 7, 3, 4 };
			currentFacing = Facing.upperRight;
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
		if (anim.GetInteger("action") != 2)
		{
			if (!anim.GetBool("hit"))
			{
				anim.SetInteger("facing", (int)currentFacing);
			}
			if (roLo.walk && !roLo.AreYouStopped())
			{
				anim.SetInteger("action", 1);
			}
			else
			{
				anim.SetInteger("action", 0);
			}
		}
	}

	private void PlayerTracking()
	{		
		if (anim.GetInteger("action") != 2)
		{
			anim.SetInteger("facing", (int)currentFacing);
			if ((InputCapture.hThrow != 0 || InputCapture.vThrow != 0) && !roLo.AreYouStopped())
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

	public void EndHitStall()
	{
		roLo.stopped = false;
		anim.SetInteger("action", 0);
	}
	public void StartHitStall()
	{
		roLo.stopped = true;
		anim.SetInteger("action", 2);
	}

	public void RemoveSprite(int locationNum)
	{
		SpriteLoadOut[locationNum].enabled = false;
		switch (locationNum)
		{
			case 0:
				SpriteLoadOut[0].enabled = false;
				break;
			case 1:
				SpriteLoadOut[1].enabled = false;				
				break;
			case 2:
				SpriteLoadOut[2].enabled = false;
				SpriteLoadOut[3].enabled = false;
				SpriteLoadOut[4].enabled = false;
				break;
			case 3:
				SpriteLoadOut[5].enabled = false;
				SpriteLoadOut[6].enabled = false;
				SpriteLoadOut[7].enabled = false;				
				break;
			case 4:
				SpriteLoadOut[8].enabled = false;
				SpriteLoadOut[9].enabled = false;
				SpriteLoadOut[10].enabled = false;
				SpriteLoadOut[11].enabled = false;
				break;
			default:
				break;
		}
	}
}
