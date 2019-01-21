using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListRE : MonoBehaviour {

	[SerializeField] ButtonSetup questButton;
	RectTransform rt;
    QuestDisplay displayObj;

    public void LoadActiveQuests()
	{
        displayObj = FindObjectOfType<QuestDisplay>();
        // Find old buttons
        rt = GetComponent<RectTransform>();
        ButtonSetup[] oldBtn = GetComponentsInChildren<ButtonSetup>();
        
        foreach (ButtonSetup btn in oldBtn)
        {
            Destroy(btn.gameObject);
        }
			
		// Go through active events
		bool display = false;
		int i = 0;
        foreach (QuestEvent quest in QuestController.activeEvents)
		    {
                if (quest.active == true)
                {

                    // Spawn button and pass the quest data
                    ButtonSetup newBtn = Instantiate(questButton, transform) as ButtonSetup;
                    newBtn.SetupQuestButton(quest);
                    i++;
                    // Make first item spawned display on the panel
                    if (!display)
                    {
                    	newBtn.ActivateButton();
                    	display = true;
                    }
                }
            }

        if(i == 0)
        {
            displayObj.DefaultDisplay();
        }

		rt.sizeDelta = new Vector2(0, 150 * i);
    }
}
