using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChangeBasedOnPlayer : MonoBehaviour {

	[SerializeField] SpriteRenderer[] SpritesToTrack;	
	bool layerAbovePlayer;
	PlayerController player;

	void Start()
	{
		player = FindObjectOfType<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		if (layerAbovePlayer)
		{
			foreach (SpriteRenderer sr in SpritesToTrack)
			{
				if (transform.position.y < player.transform.position.y)
				{
					sr.sortingLayerName = "BelowPlayer";
					layerAbovePlayer = false;
				}
			}
		}
		else if (!layerAbovePlayer)
		{
			foreach (SpriteRenderer sr in SpritesToTrack)
			{
				if (transform.position.y > player.transform.position.y)
				{
					sr.sortingLayerName = "AbovePlayer";
					layerAbovePlayer = true;
				}
			}
		}
	}
}
