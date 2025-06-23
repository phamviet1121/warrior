using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO;                           // Dùng ?? ??c và ghi file
using UnityEngine;                        // Th? vi?n chính c?a Unity ?? x? lý game object, MonoBehaviour,...

//// L?p trung gian ?? ch?a d? li?u khi l?u file JSON
//[System.Serializable]
//public class SaveDataQuest
//{
//    public Data_quest savedQuest;         // Bi?n này s? ch?a m?t ??i t??ng nhi?m v? (Data_quest) ?? l?u vào file
//}
public class Load_data_quest : MonoBehaviour
{



    //// H? th?ng qu?n lý nhi?m v? và l?u/??c d? li?u

    //public List_quest_player list_quest_player;  // ScriptableObject ch?a danh sách các nhi?m v? (DataQuest)

    //// ???ng d?n t?i file l?u d? li?u trong th? m?c riêng c?a game (Application.persistentDataPath)
    //private string savePath => Path.Combine(Application.persistentDataPath, "quest_save.json");

    //// Hàm g?i khi game b?t ??u
    //void Start()
    //{
    //    LoadDataQuest();                 // T?i d? li?u nhi?m v? t? file ho?c fallback sang m?c ??nh
    //}

    //// Hàm load d? li?u nhi?m v?
    //public void LoadDataQuest()
    //{
    //    // Ki?m tra ScriptableObject có d? li?u không
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count == 0)
    //    {
    //        Debug.LogWarning("List_quest_player ho?c danh sách DataQuest ch?a ???c gán ho?c r?ng.");
    //        return;
    //    }

    //    // N?u file JSON t?n t?i
    //    if (File.Exists(savePath))
    //    {
    //        string json = File.ReadAllText(savePath);                       // ??c n?i dung file JSON
    //        SaveDataQuest loaded = JsonUtility.FromJson<SaveDataQuest>(json); // Gi?i mã JSON thành ??i t??ng SaveDataQuest

    //        if (loaded != null && loaded.savedQuest != null)
    //        {
    //            EnsureQuestSlotExists(1);                                  // ??m b?o DataQuest có ít nh?t 2 ph?n t?
    //            list_quest_player.DataQuest[1] = loaded.savedQuest;       // Gán d? li?u t? file vào DataQuest[1]

    //            Debug.Log("?ã load t? file JSON -> Dùng DataQuest[1]");
    //        }
    //        else
    //        {
    //            UseDataQuest0AsFallback();                                 // N?u JSON null thì dùng DataQuest[0]
    //        }
    //    }
    //    else
    //    {
    //        UseDataQuest0AsFallback();                                     // N?u không có file thì dùng DataQuest[0]
    //    }

    //    ApplyToGame(list_quest_player.DataQuest[1]);                       // Gán d? li?u vào h? th?ng ch?i game
    //}

    //// Dùng DataQuest[0] làm d? li?u m?c ??nh và sao chép sang DataQuest[1]
    //private void UseDataQuest0AsFallback()
    //{
    //    Debug.LogWarning("Không có file ho?c d? li?u JSON null. Dùng DataQuest[0] và sao chép sang DataQuest[1].");

    //    EnsureQuestSlotExists(1);                                          // T?o slot th? 2 n?u ch?a có
    //    list_quest_player.DataQuest[1] = CopyQuest(list_quest_player.DataQuest[0]); // Sao chép d? li?u t? slot 0 sang 1
    //}

    //// ??m b?o danh sách DataQuest có ít nh?t (index + 1) ph?n t?
    //private void EnsureQuestSlotExists(int index)
    //{
    //    while (list_quest_player.DataQuest.Count <= index)
    //    {
    //        list_quest_player.DataQuest.Add(new Data_quest());            // Thêm ph?n t? m?i r?ng n?u c?n
    //    }
    //}

    //// Sao chép m?t Data_quest (copy th? công vì không có clone m?c ??nh)
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

    //// Gán d? li?u nhi?m v? vào h? th?ng hi?n t?i c?a game (health, energy, level,...)
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

    //    Debug.Log("?ã áp d?ng d? li?u nhi?m v? t? DataQuest[1]");
    //}

    //// L?u d? li?u hi?n t?i t? h? th?ng vào DataQuest[1] và ghi file JSON
    //public void SaveDataQuest()
    //{
    //    // ??m b?o danh sách có ?? ph?n t?
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count < 2)
    //    {
    //        Debug.LogWarning("Không th? l?u vì thi?u DataQuest[1]");
    //        return;
    //    }

    //    Data_quest quest = list_quest_player.DataQuest[1];   // D? li?u s? ???c ghi là slot 1

    //    // C?p nh?t t? h? th?ng hi?n t?i vào quest
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
    //    string json = JsonUtility.ToJson(saveData, true);          // Chuy?n ??i t??ng thành JSON
    //    File.WriteAllText(savePath, json);                         // Ghi file

    //    Debug.Log("?ã l?u DataQuest[1] vào file JSON.");
    //}
}


