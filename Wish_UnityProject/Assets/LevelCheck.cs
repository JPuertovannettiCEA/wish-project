using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheck : MonoBehaviour
{
     public static string PreviousLevel { get; private set; }
     private void OnDestroy()
     {
         PreviousLevel = gameObject.scene.name;
     }
     
     private void Start()
     {
         Debug.Log(LevelCheck.PreviousLevel);  // use this in any level to get the last level.
     }
}
