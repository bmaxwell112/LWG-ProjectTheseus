/* Dialogue System
 * 
 * This system contains all parts needed
 * to have a system that runs dialogue.
 * Adding the script Dialogue Trigger to
 * a GameObject lets you add dialogue to that trigger. 
 * That trigger is then called by another action to
 * start that conversation.
 * In the example scene there is an array of triggers
 * that activate when the previous trigger's dialoue
 * has ended. 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager instance = null;
	public static bool dialogueRunning;
	Queue<string> sentences = new Queue<string>();
	float characterSpeed;
	Animator anim;
	[SerializeField] Text nameTxt, sentenceTxt;
	[SerializeField] Image[] characters;
	[SerializeField] Button btn;
	[SerializeField] bool autoScroll;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		anim = GetComponent<Animator>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		btn.Select();
		dialogueRunning = true;
		CharacterImageCheck(dialogue);
		anim.SetBool("open", true);
		sentences.Clear();
		nameTxt.text = dialogue.name;
		characterSpeed = dialogue.characterSpeed;
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{				
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		sentenceTxt.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			sentenceTxt.text += letter;
			yield return new WaitForSeconds(characterSpeed);
		}
		if (autoScroll)
		{
			yield return new WaitForSeconds(1.5f);
			DisplayNextSentence();
		}
	}

	private void EndDialogue()
	{
		anim.SetBool("open", false);
		dialogueRunning = false;
	}

	private void CharacterImageCheck(Dialogue dialogue)
	{
		if (dialogue.characterImage != null && characters.Length > 0)
		{
			characters[dialogue.characterNumber].sprite = dialogue.characterImage;
		}
	}
}
