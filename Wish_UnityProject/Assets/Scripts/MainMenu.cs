using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OnBack()
    {
        SceneManager.LoadScene("IntroductionScene");
    }
}
