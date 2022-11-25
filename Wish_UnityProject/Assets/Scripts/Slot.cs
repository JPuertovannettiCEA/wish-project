using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int i;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            //GameManager.instance.inventoryisFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
