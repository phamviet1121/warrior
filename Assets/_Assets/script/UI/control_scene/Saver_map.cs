using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Saver_map 
{
    private const string SceneKey = "SavedScene";
    public static string name_scene; // Biến static để dùng trong runtime

    // Lưu và đồng bộ name_scene
    public static void SaveSceneName(string sceneName)
    {
        name_scene = sceneName; // Lưu vào biến static
        PlayerPrefs.SetString(SceneKey, sceneName);
        PlayerPrefs.Save();
    }

    // Tải lại từ PlayerPrefs và gán vào biến static
    public static void LoadSceneName(string defaultScene = "level_1_0")
    {
        if (PlayerPrefs.HasKey(SceneKey))
        {
            name_scene = PlayerPrefs.GetString(SceneKey);
        }
        else
        {
            name_scene = defaultScene;
        }
    }

    // Xóa dữ liệu
    public static void ClearSavedScene()
    {
        PlayerPrefs.DeleteKey(SceneKey);
        name_scene = null;
    }
}


