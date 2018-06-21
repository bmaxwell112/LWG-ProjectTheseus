using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToTechTree : MonoBehaviour {


    [SerializeField] GameObject TechTree;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject QuestLog;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToTT()
    {
        if (PauseScreen.activeSelf || QuestLog.activeSelf)
        {
            TechTree.SetActive(true);
            PauseScreen.SetActive(false);
            QuestLog.SetActive(false);
        }

    }
}
