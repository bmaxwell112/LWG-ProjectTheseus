using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CursorSetup : MonoBehaviour {

	[SerializeField] List<Cursor> cursorPositions= new List<Cursor>();

	int currentLocation = 0;
	bool moving;

	void Start()
	{
		transform.localPosition = cursorPositions[currentLocation].cursorLocation;
	}

	void Update()
	{
		CursorMovement();
		CursorAction();
	}

	void CursorAction()
	{
		bool press = InputCapture.pickup;
		if (press)
		{
			print("ran Action");
			cursorPositions[currentLocation].cursorEvent.Invoke(gameObject);
		}
	}

	void CursorMovement()
	{
		if (cursorPositions[currentLocation].cursorType == Cursor.CursorType.vertical)
		{
			VerticalMovement();
		}
		else if (cursorPositions[currentLocation].cursorType == Cursor.CursorType.horizontal)
		{
			HorizontalMovement();
		}
		else
		{
			VerticalMovement();
			HorizontalMovement();
		}
		if (InputCapture.vThrow == 0 && InputCapture.hThrow == 0)
		{
			moving = false;
		}
	}


	private void HorizontalMovement()
	{
		if (currentLocation > 0 && InputCapture.hThrow < -0.5f && !moving)
		{
			currentLocation--;
			transform.localPosition = cursorPositions[currentLocation].cursorLocation;
			moving = true;
		}
		if (currentLocation < cursorPositions.Count - 1 && InputCapture.hThrow > 0.5f && !moving)
		{
			currentLocation++;
			transform.localPosition = cursorPositions[currentLocation].cursorLocation;
			moving = true;
		}
		if (InputCapture.vThrow > 0.5f && !moving)
		{
			for (int i = currentLocation; i > 0; i--)
			{
				if (cursorPositions[i].cursorType == Cursor.CursorType.vertical)
				{
					currentLocation = cursorPositions[i].cursorOrder;
					transform.localPosition = cursorPositions[currentLocation].cursorLocation;
					moving = true;
					break;
				}
			}
		}
		if (InputCapture.vThrow < -0.5f && !moving)
		{
			for (int i = currentLocation; i < cursorPositions.Count; i++)
			{
				if (cursorPositions[i].cursorType == Cursor.CursorType.vertical)
				{
					currentLocation = cursorPositions[i].cursorOrder;
					transform.localPosition = cursorPositions[currentLocation].cursorLocation;
					moving = true;
					break;
				}
			}
		}
	}

	private void VerticalMovement()
	{
		if (currentLocation > 0 && InputCapture.vThrow > 0.5f && !moving)
		{
			currentLocation--;
			transform.localPosition = cursorPositions[currentLocation].cursorLocation;
			moving = true;
		}
		if (currentLocation < cursorPositions.Count - 1 && InputCapture.vThrow < -0.5f && !moving)
		{
			currentLocation++;
			transform.localPosition = cursorPositions[currentLocation].cursorLocation;
			moving = true;
		}		
	}
}
