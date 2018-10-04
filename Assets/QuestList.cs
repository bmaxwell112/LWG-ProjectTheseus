using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{

    [SerializeField] GameObject QuestBtn;
    GameObject newButton;
    public static QuestEvent lastEvent;
    public ButtonSetup[] buttonsAlive;
    public InfoDummy currentInfo;
    bool alreadySpawned;


    // Use this for initialization
    void Start()
    {
        alreadySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        buttonsAlive = FindObjectsOfType<ButtonSetup>();

        if (QuestController.currentQuest.eventID != lastEvent.eventID)
        {
            print("Refreshed button load, already spawned is " + alreadySpawned);
            alreadySpawned = false;
        }

        RemoveCompletedQuests();

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

    public void RemoveCompletedQuests()
    {
        for (int i = 0; i < buttonsAlive.Length; i++)
        {
            if (buttonsAlive[i].refEvent.completed == true)
            {
                Destroy(buttonsAlive[i].gameObject);
                currentInfo = FindObjectOfType<InfoDummy>();
                currentInfo.GetComponent<Text>().text = "";
            }
        }
    }
}
