using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

	Text text;
	
	public void DamageSetup(int damage, Color newColor, Vector3 newPos)
	{
		text = GetComponentInChildren<Text>();
		text.text = damage.ToString();
		text.color = newColor;
		transform.position = newPos;
	}

	public void DamageDestroy()
	{
		Destroy(gameObject);
	}
}
