using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueText
{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
