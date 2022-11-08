using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : Collidable
{
    public DialogueText dialogue;

    protected override void OnCollide(Collider2D col)
    {
        GameManager.instance.dialogue.StartDialogue(dialogue);
    }
}
