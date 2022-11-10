using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : Collidable
{
    public string[] sceneNames;
    protected override void OnCollide(Collider2D col)
    {
        if(col.name == "Player")
        {
            //Teleport the player
            GameManager.instance.SaveState();
            //string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            if(this.name == "Portal_Main")
            {
                SceneManager.LoadScene(sceneNames[1]);

            }
            if(this.name == "Portal_R1")
            {
                SceneManager.LoadScene(sceneNames[2]);

            }
            if(this.name == "Portal_R2")
            {
                SceneManager.LoadScene(sceneNames[3]);

            }
            if(this.name == "Portal_R2_Dungeon")
            {
                SceneManager.LoadScene(sceneNames[4]);

            }
            if(this.name == "Portal_R3")
            {
                SceneManager.LoadScene(sceneNames[5]);

            }
        }
    }
}
