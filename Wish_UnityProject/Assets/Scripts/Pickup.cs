using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Collectable
{

    public GameObject itemButton;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GameManager.instance.ShowText("Health Potion!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
            for(int i = 0; i < GameManager.instance.inventorySlots.Length; i++)
            {
                if(GameManager.instance.inventoryisFull[i] == false)
                {
                    //ITEM ADDED TO INVENTORY
                    GameManager.instance.inventoryisFull[i] = true;
                    Instantiate(itemButton, GameManager.instance.inventorySlots[i].transform, false);
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
