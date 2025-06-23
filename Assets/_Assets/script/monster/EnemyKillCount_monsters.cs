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

    public void manage_numEnemiesDefeated_slimer()
    {
        numEnemiesDefeated_slimer += 1;
    }
    public void manage_numEnemiesDefeated_vulture()
    {
        numEnemiesDefeated_vulture += 1;
    }
    public void manage_numEnemiesDefeated_bunny()
    {
        numEnemiesDefeated_bunny+= 1;
    }
    
    public void manage_numEnemiesDefeated_eagle()
    {
        numEnemiesDefeated_eagle += 1;
    }
    public void manage_numEnemiesDefeated_sketeton()
    {
        numEnemiesDefeated_sketeton += 1;
    }
    public void manage_numEnemiesDefeated_goblin()
    {
        numEnemiesDefeated_goblin += 1;
    }
    public void manage_numEnemiesDefeated_flying()
    {
        numEnemiesDefeated_flying += 1;
    }
    public void manage_numEnemiesDefeated_dragon()
    {
        numEnemiesDefeated_dragon += 1;
    }


}
