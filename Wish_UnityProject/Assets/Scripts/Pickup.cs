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
            if(CompareTag("RedPotion"))
            {
                GameManager.instance.ShowText("Health Potion!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
                GameManager.instance.RedPotions++;
            }
            if(CompareTag("GreenPotion"))
            {
                GameManager.instance.ShowText("Magic Potion!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
                GameManager.instance.GreenPotions++;
            }
            if(CompareTag("BluePotion"))
            {
                GameManager.instance.ShowText("Power Potion!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
                GameManager.instance.BluePotions++;
            }
            /**for(int i = 0; i < GameManager.instance.inventorySlots.Length; i++)
            {
                if(GameManager.instance.inventoryisFull[i] == false)
                {
                    //ITEM ADDED TO INVENTORY
                    GameManager.instance.inventoryisFull[i] = true;
                    Instantiate(itemButton, GameManager.instance.inventorySlots[i].transform, false);
                    break;
                }
            }**/
            Destroy(gameObject);
        }
    }
}
