using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	[SerializeField] Image loadingProgress, panel;
	Color currentColor = Color.white;
	float fadeInTime = 1;
	float den;
	bool loaded;

	// Use this for initialization
	void Start () {
		den = RoomManager.instance.roomCap;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float num = RoomGeneration.roomsInExistence;
		float xScale;
		if (den > 0)
		{
			xScale = num / den;
			loadingProgress.transform.localScale = new Vector3(xScale, 1, 1);
		}
		else
		{
			xScale = 1;
		}
		if (xScale == 1)
		{
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a -= alphaChange;
			panel.color = currentColor;
			if (!loaded)
			{
				Invoke("Destroy", fadeInTime);
				loaded = true;
			}
		}
		
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
