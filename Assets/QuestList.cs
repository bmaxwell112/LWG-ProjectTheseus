using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{

    [SerializeField] GameObject QuestBtn;
    GameObject newButton;
    public QuestEvent lastEvent;
    bool alreadySpawned;


    // Use this for initialization
    void Start()
    {
        alreadySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (QuestController.currentQuest.eventID != lastEvent.eventID)
        {
            print("Refreshed button load, already spawned is " + alreadySpawned);
            alreadySpawned = false;
        }

    }

    public void SpawnButton()
    {
        if (alreadySpawned == false)
        {
            newButton = Instantiate(QuestBtn);
            newButton.transform.parent = transform;
            alreadySpawned = true;
        }

    }


}
