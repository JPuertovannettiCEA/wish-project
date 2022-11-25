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
        text.text = "Under a beautiful starry sky, a group of friends reunites nearby a campfire, telling stories about how their ancestors became heroes...";
        StartCoroutine(NextDialogue());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator NextDialogue()
    {
        yield return new WaitForSeconds(3f);
        text.text = "Until, a shooting star appears! Lighting the night with a big shiny trail!";
        yield return new WaitForSeconds(4.5f);
        text.text = "One day, the children went to the woods...";
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
