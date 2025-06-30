using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCount_monsters : MonoBehaviour
{
     public long numEnemiesDefeated_slimer;
    public long numEnemiesDefeated_vulture;
    public long numEnemiesDefeated_bunny;
    public long numEnemiesDefeated_eagle;
    public long numEnemiesDefeated_sketeton; 
    public long numEnemiesDefeated_goblin;
    public long numEnemiesDefeated_flying;
    public long numEnemiesDefeated_dragon;

    private void Start()
    {
        LoadData();
    }
    public void manage_numEnemiesDefeated_slimer()
    {
        numEnemiesDefeated_slimer += 1;
        SaveValue("slimer", numEnemiesDefeated_slimer);
    }
    public void manage_numEnemiesDefeated_vulture()
    {
        numEnemiesDefeated_vulture += 1;
        SaveValue("vulture", numEnemiesDefeated_vulture);
    }
    public void manage_numEnemiesDefeated_bunny()
    {
        numEnemiesDefeated_bunny+= 1;
        SaveValue("bunny", numEnemiesDefeated_bunny);
    }
    
    public void manage_numEnemiesDefeated_eagle()
    {
        numEnemiesDefeated_eagle += 1;
        SaveValue("eagle", numEnemiesDefeated_eagle);
    }
    public void manage_numEnemiesDefeated_sketeton()
    {
        numEnemiesDefeated_sketeton += 1;
        SaveValue("sketeton", numEnemiesDefeated_sketeton);
    }
    public void manage_numEnemiesDefeated_goblin()
    {
        numEnemiesDefeated_goblin += 1;
        SaveValue("goblin", numEnemiesDefeated_goblin);
    }
    public void manage_numEnemiesDefeated_flying()
    {
        numEnemiesDefeated_flying += 1;
        SaveValue("flying", numEnemiesDefeated_flying);

    }
    public void manage_numEnemiesDefeated_dragon()
    {
        numEnemiesDefeated_dragon += 1;
        SaveValue("dragon", numEnemiesDefeated_dragon);
    }


    private void SaveValue(string key, long value)
    {
        PlayerPrefs.SetString("enemy_" + key, value.ToString());
        PlayerPrefs.Save();
    }

    private long LoadValue(string key)
    {
        string fullKey = "enemy_" + key;
        if (PlayerPrefs.HasKey(fullKey))
        {
            long.TryParse(PlayerPrefs.GetString(fullKey), out long result);
            return result;
        }
        return 0;
    }

    public void LoadData()
    {
        numEnemiesDefeated_slimer = LoadValue("slimer");
        numEnemiesDefeated_vulture = LoadValue("vulture");
        numEnemiesDefeated_bunny = LoadValue("bunny");
        numEnemiesDefeated_eagle = LoadValue("eagle");
        numEnemiesDefeated_sketeton = LoadValue("sketeton");
        numEnemiesDefeated_goblin = LoadValue("goblin");
        numEnemiesDefeated_flying = LoadValue("flying");
        numEnemiesDefeated_dragon = LoadValue("dragon");
    }

    public void ClearData()
    {
        string[] keys = { "slimer", "vulture", "bunny", "eagle", "sketeton", "goblin", "flying", "dragon" };
        foreach (string key in keys)
        {
            PlayerPrefs.DeleteKey("enemy_" + key);
        }
    }
}
