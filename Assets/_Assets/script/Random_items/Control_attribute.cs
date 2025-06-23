using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;

//[System.Serializable]
//public class SaveDataQuest
//{
//    public Data_quest savedQuest; 
//}


public class Control_attribute : MonoBehaviour
{
    public float maxHealth_player;
    public float maxMana_player;
    public float maxDamage;
    // public int index_icoin;
    public Transform child;
    public HealthSystem healthSystem;
    public EnergySystem energySystem;
    public attach attach;
    public Control_icoin control_icoin;
    public Transform a;
    public Control_level control_level;
    public Control_start_player control_start_player;
    public control_ui control_ui;
    public EnemyKillCount_monsters enemyKillCount_Monsters;


    public List_quest_player list_quest_player;  
   // private string savePath => Path.Combine(Application.persistentDataPath, "quest_save.json");

    void Start()
    {
        input_start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //hồi máu 
    public void ItemHeath_PickedUp(float nuber)
    {
        healthSystem.Take_healing(nuber);
    }
    //tăng máu 
    public void increasedBlood_PickedUp(float nuber)
    {
        healthSystem.IncreaseMaxHealth(nuber);
    }


    // hồi năng lượng 
    public void ItemEnergy_PickedUp(float nuber)
    {
        energySystem.RestoreMana(nuber);
    }
    // tăng mana
    public void increasedMana_PickedUp(float nuber)
    {
        energySystem.IncreaseMaxMana(nuber);
    }



    // tăng sát thương

    public void increaseDamage_PickedUp(float nuber)
    {
        attach.collider_attack.Increase_Damage(nuber);
    }


    public void Increase_Icoin_PickedUp(float nuber)
    {
        control_icoin.increase_icoin(nuber);
    }
    public void Increase_RevivePickedUp(float nuber)
    {
        control_icoin.increase_revive(nuber);
    }

    public void level_up(float nuber)
    {
        control_level.currentIndexLevel_Up(nuber);
    }

    public void replay_game()
    {
        if (control_icoin.revive >= control_icoin.un_revive)
        {
            control_icoin.replay_revive();
            control_ui.Off_canvas_game_over();
            control_start_player.On_replay();


        }

    }


    public void input_start()
    {
        child = a.transform.GetChild(0);
        healthSystem = child.GetComponent<HealthSystem>();
        energySystem = child.GetComponent<EnergySystem>();
        attach = child.GetComponent<attach>();

        //  LoadDataQuest();
        load_data_quest();
    }



    public void load_data_quest()
    {
       if( list_quest_player.DataQuest!=null && list_quest_player.DataQuest.Count>0)
        {
           if( list_quest_player.DataQuest.Count <2)
            {
                list_quest_player.DataQuest.Add(new Data_quest());
                list_quest_player.DataQuest[1] = CopyQuest(list_quest_player.DataQuest[0]);
            }

            Saver_data_quest loaded = LoadQuestFromJson();
            if (loaded != null)
            {
                list_quest_player.DataQuest[1] = loaded.ToDataQuest();
                Debug.Log("Quest[1] loaded from saved JSON.");
            }
            else
            {
                list_quest_player.DataQuest[1] = CopyQuest(list_quest_player.DataQuest[0]);
                Debug.Log("Quest[1] copied from Quest[0].");
            }

            Data_quest quest = list_quest_player.DataQuest[1];

            control_level.indexlevel = quest.level;
            control_level.maxIndexLevel = quest.expMax;
            control_level.currentIndexLevel = quest.exp;

            healthSystem.maxHealth = quest.heathMax;
            energySystem.maxEnergy = quest.energyMax;

            attach.collider_attack.damageAmount = quest.damge1;
            attach.collider_attack._damageAmount = quest.damge2;
            attach.collider_attack.damageAmount_ = quest.damgeU;
        }
    }
    private void EnsureQuestSlotExists(int index)
    {
        while (list_quest_player.DataQuest.Count <= index)
        {
            list_quest_player.DataQuest.Add(new Data_quest());
        }
    }
    private Data_quest CopyQuest(Data_quest original)
    {
        return new Data_quest
        {
            level = original.level,
            exp = original.exp,
            expMax = original.expMax,
            heathMax = original.heathMax,
            energyMax = original.energyMax,
            damge1 = original.damge1,
            damge2 = original.damge2,
            damgeU = original.damgeU
        };
    }

    private void SaveQuestToJson(Data_quest quest)
    {
        Saver_data_quest saver = new Saver_data_quest(quest);
        string json = JsonUtility.ToJson(saver, true); // pretty print
        string path = Application.persistentDataPath + "/quest_data.json";
        System.IO.File.WriteAllText(path, json);

        Debug.Log("Quest data saved to: " + path);
    }

    void OnApplicationPause()
    {

        Data_quest quest = list_quest_player.DataQuest[1];
        SaveQuestToJson(quest);
    }
    void OnApplicationQuit()
    {
        save();
    }
    private Saver_data_quest LoadQuestFromJson()
    {
        string path = Application.persistentDataPath + "/quest_data.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            return JsonUtility.FromJson<Saver_data_quest>(json);
        }
        return null;
    }
    public void save()
    {
        Data_quest quest = list_quest_player.DataQuest[1];
        SaveQuestToJson(quest);
    }    


    //void OnApplicationPause(bool pauseStatus)
    //{
    //    if (pauseStatus) // app is going to background
    //    {
    //        // Lưu dữ liệu
    //        if (list_quest_player.DataQuest.Count > 1)
    //        {
    //            SaveQuestToJson(list_quest_player.DataQuest[1]);
    //        }
    //    }
    //    else // app quay trở lại foreground
    //    {
    //        // Tải lại dữ liệu nếu có
    //        Saver_data_quest loaded = LoadQuestFromJson();
    //        if (loaded != null)
    //        {
    //            if (list_quest_player.DataQuest.Count > 1)
    //            {
    //                list_quest_player.DataQuest[1] = loaded.ToDataQuest();
    //                Debug.Log("Quest data restored from JSON.");
    //            }
    //        }
    //    }
    //}





    //public void LoadDataQuest()
    //{
    //    Debug.Log("chạy chưa");
    //    // Ki?m tra ScriptableObject có d? li?u không
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count == 0)
    //    {
    //        Debug.LogWarning("List_quest_player ho?c danh sách DataQuest ch?a ???c gán ho?c r?ng.");
    //        return;
    //    }

    //    // N?u file JSON t?n t?i
    //    if (File.Exists(savePath))
    //    {
    //        string json = File.ReadAllText(savePath);                      
    //        SaveDataQuest loaded = JsonUtility.FromJson<SaveDataQuest>(json); 

    //        if (loaded != null && loaded.savedQuest != null)
    //        {
    //            EnsureQuestSlotExists(1);                                 
    //            list_quest_player.DataQuest[1] = loaded.savedQuest;       

    //            Debug.Log("?ã load t? file JSON -> Dùng DataQuest[1]");
    //        }
    //        else
    //        {
    //            UseDataQuest0AsFallback();                                
    //        }
    //    }
    //    else
    //    {
    //        UseDataQuest0AsFallback();                                  
    //    }

    //    ApplyToGame(list_quest_player.DataQuest[1]);                      
    //}


    //private void UseDataQuest0AsFallback()
    //{
    //    Debug.LogWarning("Không có file ho?c d? li?u JSON null. Dùng DataQuest[0] và sao chép sang DataQuest[1].");

    //    EnsureQuestSlotExists(1);                                        
    //    list_quest_player.DataQuest[1] = CopyQuest(list_quest_player.DataQuest[0]);
    //}


    //private void EnsureQuestSlotExists(int index)
    //{
    //    while (list_quest_player.DataQuest.Count <= index)
    //    {
    //        list_quest_player.DataQuest.Add(new Data_quest());
    //    }
    //}


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


    //private void ApplyToGame(Data_quest quest)
    //{
    //    control_level.indexlevel = quest.level;
    //    control_level.maxIndexLevel = quest.expMax;
    //    control_level.currentIndexLevel = quest.exp;

    //    healthSystem.maxHealth = quest.heathMax;
    //    energySystem.maxEnergy = quest.energyMax;

    //    attach.collider_attack.damageAmount = quest.damge1;
    //    attach.collider_attack._damageAmount = quest.damge2;
    //    attach.collider_attack.damageAmount_ = quest.damgeU;

    //    Debug.Log("?ã áp d?ng d? li?u nhi?m v? t? DataQuest[1]");
    //}

    //void OnApplicationQuit()
    //{
    //    SaveDataQuest(); // Gọi lưu dữ liệu khi game tắt
    //}
    //public void SaveDataQuest()
    //{
    //    // ??m b?o danh sách có ?? ph?n t?
    //    if (list_quest_player == null || list_quest_player.DataQuest == null || list_quest_player.DataQuest.Count < 2)
    //    {
    //        Debug.LogWarning("Không th? l?u vì thi?u DataQuest[1]");
    //        return;
    //    }

    //    Data_quest quest = list_quest_player.DataQuest[1];   


    //    quest.level = control_level.indexlevel;
    //    quest.expMax = control_level.maxIndexLevel;
    //    quest.exp = control_level.currentIndexLevel;
    //    quest.heathMax = healthSystem.maxHealth;
    //    quest.energyMax = energySystem.maxEnergy;
    //    quest.damge1 = attach.collider_attack.damageAmount;
    //    quest.damge2 = attach.collider_attack._damageAmount;
    //    quest.damgeU = attach.collider_attack.damageAmount_;

    //    // Ghi ra file JSON
    //    SaveDataQuest saveData = new SaveDataQuest { savedQuest = quest };
    //    string json = JsonUtility.ToJson(saveData, true);        
    //    File.WriteAllText(savePath, json);                       

    //    Debug.Log("?ã l?u DataQuest[1] vào file JSON.");
    //}





}
