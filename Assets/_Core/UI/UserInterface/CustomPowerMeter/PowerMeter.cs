using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {
	public class PowerMeter : MonoBehaviour {

		[SerializeField] Image fill;
		public float value = 1;

		void Update () {
			fill.transform.localScale = new Vector3 (1, value, 1);
		}
	}
}