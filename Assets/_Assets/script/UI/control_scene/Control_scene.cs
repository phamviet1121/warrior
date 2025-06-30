using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control_scene : MonoBehaviour
{
    public string name_scene;

 

    public void ContinueGame_scene()
    {
      
        SceneManager.LoadScene(Saver_map.name_scene);
    }    



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

    public void newplay_scene_1()
    {
        SceneManager.LoadScene("level_1_0");
    }

    public void SceneManager_next(string name_scene_)
    {
        SceneManager.LoadScene(name_scene_);
    }


    public void save_scene(string name_scene_)
    {
        Saver_map.SaveSceneName(name_scene_);
    }

}
