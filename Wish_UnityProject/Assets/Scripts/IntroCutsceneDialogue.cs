using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCutsceneDialogue : MonoBehaviour
{
    public TMP_Text text;

    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "One day, the children went to the woods...";
        StartCoroutine(NextDialogue());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator NextDialogue()
    {
        yield return new WaitForSeconds(6f);
        text.text = "They overcame many challenges but most of all, they discovered a new land";
        yield return new WaitForSeconds(6f);
        text.text = "...One night, they found a sacred place";
        yield return new WaitForSeconds(6f);
        text.text = "Where they found Allegra!";
        yield return new WaitForSeconds(5f);
        transition.SetTrigger("Start"); 
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);

        
    }
}
