using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using System;

public class Buttons : MonoBehaviour, ISelectHandler
{
	AudioClip hover, select;
	Button btn;
	Slider slider;

	public void Start()
	{
		hover = Resources.Load<AudioClip>("sound/Menu Effects/Hover");
		select = Resources.Load<AudioClip>("sound/Menu Effects/Select");
		btn = GetComponent<Button>();
		slider = GetComponent<Slider>();
		if (btn)
		{
			btn.onClick.AddListener(StartClickNoise);
		}
		if (slider)
		{
			slider.onValueChanged.AddListener(StartMoveNoise);
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		
		Utilities.PlaySoundEffect(hover);
	}
	
	private void StartMoveNoise(float arg0)
	{
		Utilities.PlaySoundEffect(select);
	}
	public void StartClickNoise()
	{
		Utilities.PlaySoundEffect(select);
	}
}
