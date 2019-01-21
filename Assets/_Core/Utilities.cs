using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
	public class Utilities : MonoBehaviour {

		public static float CalculateAngle (Vector3 from, Vector3 to) {
			return Quaternion.FromToRotation (Vector3.up, to - from).eulerAngles.z;
		}

		public static float ReturnEulerAngle (float rotateValue) {
			if (rotateValue > 360) {
				return rotateValue - 360;
			} else if (rotateValue < 0) {
				return rotateValue + 360;
			} else {
				return rotateValue;
			}
		}

		public static void PlaySoundEffect (AudioClip audio) {
			Debug.Log (audio);
			if (audio != null) {
				AudioSource.PlayClipAtPoint (audio, CameraController.CAMPOS, GameManager.SoundFXVolume);
			}
		}
	}
}