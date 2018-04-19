using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	public void MoveCamera(Vector3 roomLocation)
	{
		Vector3 endLocation = new Vector3(roomLocation.x, roomLocation.y, transform.position.z);
		Vector3 startLocation = transform.position;
		float startTime = Time.time;
		float journeyLength = Vector3.Distance(startLocation, endLocation);
		StartCoroutine(Moving(startLocation, endLocation, startTime, journeyLength));

	}

	IEnumerator Moving(Vector3 start, Vector3 end, float time, float journeyLength)
	{
		while (transform.position != end)
		{
			float distCovered = (Time.time - time) * 15f;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(start, end, fracJourney);
			yield return null;
		}
		
	}
}
