using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour {

    //Make a real reason to start the junk belts

    public bool beginSequence;

	// Use this for initialization
	void Start () {
        beginSequence = false;
        StartCoroutine(Wait());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(30);
        beginSequence = true;
    }

}
