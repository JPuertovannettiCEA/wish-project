using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Collectable
{


    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GameManager.instance.ShowText("Health Potion!",35,Color.black,transform.position,Vector3.up * 50, 3.0f);
            Destroy(gameObject);
        }
    }
}
