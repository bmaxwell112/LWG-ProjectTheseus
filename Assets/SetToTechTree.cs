using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToTechTree : MonoBehaviour {


    [SerializeField] GameObject TechTree;
    [SerializeField] GameObject PauseScreen;

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
        if (PauseScreen.activeSelf)
        {
            TechTree.SetActive(true);
            PauseScreen.SetActive(false);
        }

    }
}
