using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core.DialogueSystem {
	[System.Serializable]
	public class Dialogue {

		public string name;
		public float characterSpeed;
		public Sprite characterImage;
		public int characterNumber;
		[TextArea (3, 10)] public string[] sentences;
	}
}