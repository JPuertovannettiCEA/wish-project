using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueText : MonoBehaviour
{
    public bool active;
    public GameObject go;
    public TMP_Text dialogueText; 
    public TMP_Text nameText;
    public GameObject characterImage; 
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void ShowDialogue()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void HideDialogue()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateDialogueText()
    {
        if(!active)
        {
            return;
        }

        //10-7>2 Example
        if(Time.time - lastShown > duration)
        {
            HideDialogue();
        }

        go.transform.position += motion * Time.deltaTime;
    }
}
