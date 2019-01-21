using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.Core;
using Theseus.DatabaseSystem;

namespace Theseus.Character {
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

		void Start () {
			attack = GetComponentInChildren<RobotAttack> ();
			animCont = GetComponentInParent<RobotAnimationController> ();
			anim = GetComponent<Animator> ();
			roLo = GetComponentInParent<RobotLoadout> ();
			isPlayer = GetComponentInParent<PlayerController> ();
			aoc = new AnimatorOverrideController (anim.runtimeAnimatorController);
			anim.runtimeAnimatorController = aoc;
		}

		void Update () {
			if (isPlayer) {
				PlayerTracking ();
			} else {
				EnemyTracking ();
			}
			if (anim.GetInteger ("action") == 1 && isPlayer) {
				if ((int) animCont.currentFacing != rotationNum) {
					DoneAttacking ();
				}
			} else {
				rotationNum = (int) animCont.currentFacing;
			}
		}

		public void DoneAttacking () {
			anim.SetInteger ("action", 0);
			attack.meleeAttacking = false;
			attack.StartMovement ();
			animCont.DetectFacingAndSetOrder ();
		}

		public void EnemyTracking () {
			if (anim.GetInteger ("action") != 2) {
				anim.SetInteger ("facing", (int) animCont.currentFacing);
				if (roLo.loadout[armLocation].itemType == ItemType.range && (roLo.power[armLocation] > 0 && roLo.hitPoints[armLocation] > 0)) {
					anim.SetInteger ("action", 3);
				}
			}
		}

		bool PlayerMelee () {
			anim.SetInteger ("facing", (int) animCont.currentFacing);
			if (armLocation == 2) {
				return InputCapture.fireLeftDown;
			} else {
				return InputCapture.fireRightDown;
			}
		}
		bool PlayerRanged () {
			if (armLocation == 2) {
				return InputCapture.triggerLeft;
			} else {
				return InputCapture.triggerRight;
			}
		}
		public void PlayerTracking () {
			bool blockDodge = Input.GetButton ("BlockDodge");

			if (!blockDodge) {
				if (anim.GetInteger ("action") == 4) {
					anim.SetInteger ("action", 0);
				}
				if (PlayerMelee ()) {
					anim.SetInteger ("action", 1);
					attack.StopMovementCheck ();
				}
				if (PlayerRanged () && roLo.loadout[armLocation].itemType == ItemType.range && (roLo.power[armLocation] > 0 && roLo.hitPoints[armLocation] > 0)) {
					anim.SetInteger ("action", 3);
				}
			} else {
				anim.SetInteger ("action", 4);
			}
		}

		public void MeleeAttack () {
			attack.MeleeAttack ();
		}

		public void SwapWeapons (AnimatorOverrideController rac) {
			//anim.runtimeAnimatorController = rac;		
			overrides = new List<KeyValuePair<AnimationClip, AnimationClip>> (rac.overridesCount);
			rac.GetOverrides (overrides);
			for (int i = 0; i < overrides.Count; ++i) {
				overrides[i] = new KeyValuePair<AnimationClip, AnimationClip> (overrides[i].Key, overrides[i].Value);
			}
			aoc.ApplyOverrides (overrides);
			anim.Update (0f);
		}

		public void EndHitStall () {
			roLo.stopped = false;
			anim.SetInteger ("action", 2);
		}
		public void StartHitStall () {
			roLo.stopped = true;
			anim.SetInteger ("action", 0);
		}
	}
}