using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : Collidable
{

    public Animator transition;

    public float transitionTime = 1f;

    public string[] sceneNames;
    protected override void OnCollide(Collider2D col)
    {
        if(col.name == "Player")
        {
            //Teleport the player
            GameManager.instance.SaveState();
            
            if(this.name == "Portal_Main")
            {
                StartCoroutine(LoadLevel(1));
                //SceneManager.LoadScene(sceneNames[1]);

            }
            if(this.name == "Portal_R1")
            {
                StartCoroutine(LoadLevel(2));
                
                //SceneManager.LoadScene(sceneNames[2]);

            }
            if(this.name == "Portal_R2")
            {
                StartCoroutine(LoadLevel(3));
                //SceneManager.LoadScene(sceneNames[3]);

            }
            if(this.name == "Portal_R2_Dungeon")
            {
                StartCoroutine(LoadLevel(4));
                //SceneManager.LoadScene(sceneNames[4]);

            }
            if(this.name == "Portal_R3")
            {
                StartCoroutine(LoadLevel(5));
                //SceneManager.LoadScene(sceneNames[5]);

            }
            if(this.name == "Portal_End")
            {
                StartCoroutine(LoadLevel(6));
                //SceneManager.LoadScene(sceneNames[5]);

            }
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait to stop playing
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(sceneNames[levelIndex]);

    }
}
