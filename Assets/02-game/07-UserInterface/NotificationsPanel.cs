using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsPanel : MonoBehaviour {

	Animator anim;
	Text text;

	// Use this for initialization
	void Start()
	{
		FetchAnim();
		text = GetComponentInChildren<Text>();
	}

	public void NotificationsPanelSetEnable(string newText)
	{
		CancelInvoke();
		FetchAnim();
		anim.SetBool("active", true);
		text.text = newText;
		Invoke("NotificationsPanelDisabled", 3);
	}
	public void NotificationsPanelDisabled()
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
