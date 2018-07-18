using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToLoadout : MonoBehaviour {

    [SerializeField] GameObject TechTree;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject QuestLog;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveToLO()
    {
        if (TechTree.activeSelf || QuestLog.activeSelf)
        {
            TechTree.SetActive(false);
            PauseScreen.SetActive(true);
            QuestLog.SetActive(false);
        }
    }

}
