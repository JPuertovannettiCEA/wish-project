using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTextManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public TMPro.TMP_FontAsset fontAsset;

    public Image avatar;

    public Image NPC;
    private Queue<string> sentences;

    public bool isColliding = false;

    public Animator animator;
    private void Start()
    {
        sentences = new Queue<string>();
        //avatar = NPC;
    }

    public void StartDialogue(DialogueText dialogue, TMPro.TMP_FontAsset font)
    {
        animator.SetBool("isOpen",true);
        //Debug.Log("Starting conversation with" + dialogue.name);
        isColliding = true;
        avatar.sprite = NPC.sprite;
        GameManager.instance.player.GetComponent<PlayerController>().enabled = false;
        dialogueText.font = font;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen",false);
        GameManager.instance.player.GetComponent<PlayerController>().enabled = true;
        //Debug.Log("End conversation");
    }
}
