using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {

	Animator anim;
	Text text;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		text = GetComponentInChildren<Text>();
	}

	public void ItemPanelSetEnable(string newText)
	{
		FetchAnim();
		anim.SetBool("active", true);
		text.text = newText;
	}
	public void ItemPanelDisabled()
	{
		FetchAnim();
		anim.SetBool("active", false);
	}

	void FetchAnim()
	{
		if (!anim)
		{
			anim = GetComponent<Animator>();
		}
	}
}
