using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextProperties : MonoBehaviour {

	[Tooltip("Character per Second")][SerializeField] float characterSpeed;
	Text text;
	[SerializeField] bool TextAtStart;
	[SerializeField] float delayInSeconds;
	string txtString;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		txtString = text.text;
		text.text = "";
		if (TextAtStart)
		{
			StartSentence();
		}
		if (delayInSeconds > 0)
		{
			Invoke("StartSentence", delayInSeconds);
		}
	}

	public void StartSentence()
	{
		StartCoroutine(TypeSentence(txtString));
	}

	IEnumerator TypeSentence(string sentence)
	{
		text.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			text.text += letter;
			yield return new WaitForSeconds(characterSpeed);
		}
	}
}
