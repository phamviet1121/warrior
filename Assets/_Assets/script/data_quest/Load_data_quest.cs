using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO;                           // D�ng ?? ??c v� ghi file
using UnityEngine;                        // Th? vi?n ch�nh c?a Unity ?? x? l� game object, MonoBehaviour,...

//// L?p trung gian ?? ch?a d? li?u khi l?u file JSON
//[System.Serializable]
//public class SaveDataQuest
//{
//    public Data_quest savedQuest;         // Bi?n n�y s? ch?a m?t ??i t??ng nhi?m v? (Data_quest) ?? l?u v�o file
//}
public class Load_data_quest : MonoBehaviour
{



    //// H? th?ng qu?n l� nhi?m v? v� l?u/??c d? li?u

    //public List_quest_player list_quest_player;  // ScriptableObject ch?a danh s�ch c�c nhi?m v? (DataQuest)

    //// ???ng d?n t?i file l?u d? li?u trong th? m?c ri�ng c?a game (Application.persistentDataPath)
    //private string savePath => Path.Combine(Application.persistentDataPath, "quest_save.json");

    //// H�m g?i khi game b?t ??u
    //void Start()
    //{
    //    LoadDataQuest();                 // T?i d? li?u nhi?m v? t? file ho?c fallback sang m?c ??nh
    //}

    //// H�m load d? li?u nhi?m v?
    //public void LoadDataQuest()
    //{
    //    // Ki?m tra ScriptableObject c� d? li?u kh�ng
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count == 0)
    //    {
    //        Debug.LogWarning("List_quest_player ho?c danh s�ch DataQuest ch?a ???c g�n ho?c r?ng.");
    //        return;
    //    }

    //    // N?u file JSON t?n t?i
    //    if (File.Exists(savePath))
    //    {
    //        string json = File.ReadAllText(savePath);                       // ??c n?i dung file JSON
    //        SaveDataQuest loaded = JsonUtility.FromJson<SaveDataQuest>(json); // Gi?i m� JSON th�nh ??i t??ng SaveDataQuest

    //        if (loaded != null && loaded.savedQuest != null)
    //        {
    //            EnsureQuestSlotExists(1);                                  // ??m b?o DataQuest c� �t nh?t 2 ph?n t?
    //            list_quest_player.DataQuest[1] = loaded.savedQuest;       // G�n d? li?u t? file v�o DataQuest[1]

    //            Debug.Log("?� load t? file JSON -> D�ng DataQuest[1]");
    //        }
    //        else
    //        {
    //            UseDataQuest0AsFallback();                                 // N?u JSON null th� d�ng DataQuest[0]
    //        }
    //    }
    //    else
    //    {
    //        UseDataQuest0AsFallback();                                     // N?u kh�ng c� file th� d�ng DataQuest[0]
    //    }

    //    ApplyToGame(list_quest_player.DataQuest[1]);                       // G�n d? li?u v�o h? th?ng ch?i game
    //}

    //// D�ng DataQuest[0] l�m d? li?u m?c ??nh v� sao ch�p sang DataQuest[1]
    //private void UseDataQuest0AsFallback()
    //{
    //    Debug.LogWarning("Kh�ng c� file ho?c d? li?u JSON null. D�ng DataQuest[0] v� sao ch�p sang DataQuest[1].");

    //    EnsureQuestSlotExists(1);                                          // T?o slot th? 2 n?u ch?a c�
    //    list_quest_player.DataQuest[1] = CopyQuest(list_quest_player.DataQuest[0]); // Sao ch�p d? li?u t? slot 0 sang 1
    //}

    //// ??m b?o danh s�ch DataQuest c� �t nh?t (index + 1) ph?n t?
    //private void EnsureQuestSlotExists(int index)
    //{
    //    while (list_quest_player.DataQuest.Count <= index)
    //    {
    //        list_quest_player.DataQuest.Add(new Data_quest());            // Th�m ph?n t? m?i r?ng n?u c?n
    //    }
    //}

    //// Sao ch�p m?t Data_quest (copy th? c�ng v� kh�ng c� clone m?c ??nh)
    //private Data_quest CopyQuest(Data_quest original)
    //{
    //    return new Data_quest
    //    {
    //        level = original.level,
    //        exp = original.exp,
    //        expMax = original.expMax,
    //        heathMax = original.heathMax,
    //        energyMax = original.energyMax,
    //        damge1 = original.damge1,
    //        damge2 = original.damge2,
    //        damgeU = original.damgeU
    //    };
    //}

    //// G�n d? li?u nhi?m v? v�o h? th?ng hi?n t?i c?a game (health, energy, level,...)
    //private void ApplyToGame(Data_quest quest)
    //{
    //    //control_level.indexlevel = quest.level;
    //    //control_level.maxIndexLevel = quest.expMax;
    //    //control_level.currentIndexLevel = quest.exp;

    //    //healthSystem.maxHealth = quest.heathMax;
    //    //energySystem.maxEnergy = quest.energyMax;

    //    //attach.collider_attack.damageAmount = quest.damge1;
    //    //attach.collider_attack._damageAmount = quest.damge2;
    //    //attach.collider_attack.damageAmount_ = quest.damgeU;

    //    Debug.Log("?� �p d?ng d? li?u nhi?m v? t? DataQuest[1]");
    //}

    //// L?u d? li?u hi?n t?i t? h? th?ng v�o DataQuest[1] v� ghi file JSON
    //public void SaveDataQuest()
    //{
    //    // ??m b?o danh s�ch c� ?? ph?n t?
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count < 2)
    //    {
    //        Debug.LogWarning("Kh�ng th? l?u v� thi?u DataQuest[1]");
    //        return;
    //    }

    //    Data_quest quest = list_quest_player.DataQuest[1];   // D? li?u s? ???c ghi l� slot 1

    //    // C?p nh?t t? h? th?ng hi?n t?i v�o quest
    //    //quest.level = control_level.indexlevel;
    //    //quest.expMax = control_level.maxIndexLevel;
    //    //quest.exp = control_level.currentIndexLevel;
    //    //quest.heathMax = healthSystem.maxHealth;
    //    //quest.energyMax = energySystem.maxEnergy;
    //    //quest.damge1 = attach.collider_attack.damageAmount;
    //    //quest.damge2 = attach.collider_attack._damageAmount;
    //    //quest.damgeU = attach.collider_attack.damageAmount_;

    //    // Ghi ra file JSON
    //    SaveDataQuest saveData = new SaveDataQuest { savedQuest = quest };
    //    string json = JsonUtility.ToJson(saveData, true);          // Chuy?n ??i t??ng th�nh JSON
    //    File.WriteAllText(savePath, json);                         // Ghi file

    //    Debug.Log("?� l?u DataQuest[1] v�o file JSON.");
    //}
}


