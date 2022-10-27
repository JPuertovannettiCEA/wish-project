using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTextManager : MonoBehaviour
{
    public GameObject textContainer;
    //public GameObject characterImage;
    public GameObject textPrefab;

    private List<DialogueText> DialogueTexts = new List<DialogueText>();

    private void Update()
    {
        foreach(DialogueText txt in DialogueTexts)
        {
            txt.UpdateDialogueText();
        }
    }

    public void ShowDialogue(string msg, string character,GameObject Image, Color color, Vector3 motion, float duration)
    {
        DialogueText DialogueText = GetDialogueText();

        DialogueText.dialogueText.text = msg;
        //DialogueText.dialogueText.fontSize = fontSize;
        DialogueText.dialogueText.color = color;
        DialogueText.nameText.text = character;
        DialogueText.characterImage = Image;
        
        //DialogueText.go.transform.position = Camera.main.WorldToScreenPoint(pos); //Transfer world space to screen space so we can use it in the UI
        DialogueText.motion = motion;
        DialogueText.duration = duration;

        DialogueText.ShowDialogue();
    }
    private DialogueText GetDialogueText()
    {
        DialogueText txt = DialogueTexts.Find(t => !t.active); //Exactly the same as a For loop

        if(txt == null)
        {
            txt = new DialogueText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.dialogueText = txt.go.GetComponent<TMP_Text>();

            DialogueTexts.Add(txt);
        }

        return txt;
    }
}
