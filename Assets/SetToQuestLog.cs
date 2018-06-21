using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToQuestLog: MonoBehaviour {


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

    public void MoveToQL()
    {
        if (PauseScreen.activeSelf || TechTree.activeSelf)
        {
            QuestLog.SetActive(true);
            TechTree.SetActive(false);
            PauseScreen.SetActive(false);
        }

    }
}
