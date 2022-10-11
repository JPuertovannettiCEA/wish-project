using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public TMP_Text text; 
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void ShowText()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void HideText()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if(!active)
        {
            return;
        }

        //10-7>2 Example
        if(Time.time - lastShown > duration)
        {
            HideText();
        }

        go.transform.position += motion * Time.deltaTime;
    }
}
