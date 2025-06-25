using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_scene : MonoBehaviour
{
    public string name_scene;
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

    public void next_scene()
    {
        StartCoroutine(LoadSceneAfterDelay(1f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(name_scene);
    }



}
