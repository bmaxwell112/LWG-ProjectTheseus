using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[Serializable]
public class CursorEvent : UnityEvent<GameObject> { }
[Serializable]
public class Cursor {
	public int cursorOrder;
	public Vector3 cursorLocation;
	public CursorEvent cursorEvent;
	public CursorType cursorType;

	public enum CursorType { horizontal, vertical, both }

	public Cursor(int id, CursorType type, Vector3 location, CursorEvent cEvent)
	{
		cursorOrder = id;
		cursorType = type;
		cursorLocation = location;
		cursorEvent = cEvent;
	}
}
