using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trackpoints : MonoBehaviour {

    [SerializeField] Text thisText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        thisText.text = "Points: " + PlayerPrefsManager.GetPointValue();
	}
}
