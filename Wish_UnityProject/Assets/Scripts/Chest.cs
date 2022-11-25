using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int Amount = 10;
    public GameObject characterImage;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + Amount + "Pesos!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
            //SAVE
            //GameManager.instance.pesos += pesosAmount;
        }
    }
}
