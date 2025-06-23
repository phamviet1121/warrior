using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;

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
    }




}
