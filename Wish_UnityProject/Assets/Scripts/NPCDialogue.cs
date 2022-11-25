using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : Collidable
{
    public DialogueText dialogue;

    /**
    protected override void Start()
    {
        base.Start();
        if(this.name == "Boss_Battle(Clone)")
        {
        }
    }**/

    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Fighter" && GameManager.instance.dialogue.isColliding == false && this.name == "NPCAllegra")
        {
            GameManager.instance.dialogueBegins = true;
            GameManager.instance.dialogue.StartDialogue(dialogue, GameManager.instance.TextFonts[2], GameManager.instance.NPCAvatarsForDialogue[0]);
        } 
    }

    protected override void Update()
    {
        base.Update();
        if(GameManager.instance.isBossBattle == true && this.name == "Boss_Battle(Clone)")
        {
            GameManager.instance.dialogueBegins = true;
            GameManager.instance.dialogue.StartDialogue(dialogue, GameManager.instance.TextFonts[1], GameManager.instance.NPCAvatarsForDialogue[1]);
            GameManager.instance.isBossBattle = false;
        }
        if(GameManager.instance.bossBattleEnds == true && this.name == "Boss_PostBattle")
        {
            //GameManager.instance.BossPostdialogueBegins = true;
            GameManager.instance.dialogue.StartDialogue(dialogue, GameManager.instance.TextFonts[1], GameManager.instance.NPCAvatarsForDialogue[1]);
            GameManager.instance.bossBattleEnds = false;
        }
    }
}
