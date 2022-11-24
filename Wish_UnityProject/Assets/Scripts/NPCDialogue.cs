using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : Collidable
{
    public DialogueText dialogue;

    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Fighter" && GameManager.instance.dialogue.isColliding == false)
        {
            GameManager.instance.dialogue.StartDialogue(dialogue, GameManager.instance.TextFonts[2]);
        } 
        else
        {
            //GameManager.instance.dialogue.isColliding = false;
        }
    }
}
