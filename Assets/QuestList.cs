﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour {

    [SerializeField] GameObject QuestBtn;
    GameObject newButton;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnButton()
    {
        newButton = Instantiate(QuestBtn);
        newButton.transform.parent = transform;
        newButton.transform.localPosition = new Vector3(0, 0, 0);
    }
}
