using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime = 1f;
    public Animator transition;

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void FadeAnExit()
    {
        StartCoroutine(ExitGame());
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        Debug.Log("Crossfading");
        yield return new WaitForSeconds(transitionTime);

        Debug.Log("Load level!" + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator ExitGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }

}
