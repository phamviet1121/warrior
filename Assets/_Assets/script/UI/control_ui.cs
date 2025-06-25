using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class control_ui : MonoBehaviour
{
    public Slider healthSlider;
    public Slider energySlider;
    public Slider levelSlider;

    public Control_attribute control_attribute;

    public TextMeshProUGUI icoin_txt;

    public TextMeshProUGUI revive_txt;
    public TextMeshProUGUI un_revive_txt;

    public TextMeshProUGUI health_txt;
    public TextMeshProUGUI energy_txt;
    public TextMeshProUGUI level_txt;
    public TextMeshProUGUI indexlevel_txt;

    // Biến để lưu trạng thái trước đó
    private float prevHealth = -1;
    private float prevMaxHealth = -1;

    private float prevEnergy = -1;
    private float prevMaxEnergy = -1;

    private float maxPrevLevel = -1;
    private float currentPrevLevel = -1;

    private float prevIcoin = -1;
    private float prevRevive = -1;
    private float prev_un_Revive = -1;

    public GameObject canvas_gameover;
    public GameObject canvas_setting;
    public GameObject canvas_Level_Up;
    public GameObject canvar_book_information;

    public TextMeshProUGUI levelbook_txt;
    public TextMeshProUGUI healthMax_txt;
    public TextMeshProUGUI energyMax_txt;
    public TextMeshProUGUI exp_txt;

    public TextMeshProUGUI damge_1_txt;
    public TextMeshProUGUI damge_2_txt;
    public TextMeshProUGUI damge_U_txt;

    public TextMeshProUGUI icon_txt;
    public TextMeshProUGUI revive_book_txt;

    public TextMeshProUGUI numEnemiesDefeated_slimer_txt;
    public TextMeshProUGUI numEnemiesDefeated_vulture_txt;
    public TextMeshProUGUI numEnemiesDefeated_bunny_txt;
    public TextMeshProUGUI numEnemiesDefeated_eagle_txt;
    public TextMeshProUGUI numEnemiesDefeated_sketeton_txt;
    public TextMeshProUGUI numEnemiesDefeated_goblin_txt;
    public TextMeshProUGUI numEnemiesDefeated_flying_txt;
    public TextMeshProUGUI numEnemiesDefeated_dragon_txt;





    private void Start()
    {
        canvas_gameover.SetActive(false);
        canvas_setting.SetActive(false);
        canvas_Level_Up.SetActive(false);
        canvar_book_information.SetActive(false);
    }

    void Update()
    {
        var healthSystem = control_attribute.healthSystem;
        var energySystem = control_attribute.energySystem;
        var icoinSystem = control_attribute.control_icoin;
        var control_levelSystem = control_attribute.control_level;

        // Cập nhật thanh máu nếu thay đổi
        if (prevMaxHealth != healthSystem.maxHealth || prevHealth != healthSystem.currentHealth)
        {
            healthSlider.maxValue = healthSystem.maxHealth;
            healthSlider.value = healthSystem.currentHealth;
            health_txt.text = healthSystem.currentHealth.ToString() + " / " + healthSystem.maxHealth.ToString();
            prevHealth = healthSystem.currentHealth;
            prevMaxHealth = healthSystem.maxHealth;

        }

        // Cập nhật thanh năng lượng nếu thay đổi
        if (prevMaxEnergy != energySystem.maxEnergy || prevEnergy != energySystem.currentEnergy)
        {
            energySlider.maxValue = energySystem.maxEnergy;
            energySlider.value = energySystem.currentEnergy;
            energy_txt.text = energySystem.currentEnergy.ToString() + " / " + energySystem.maxEnergy.ToString();
            prevEnergy = energySystem.currentEnergy;
            prevMaxEnergy = energySystem.maxEnergy;
        }


        // Cập nhật thanh năng lượng nếu thay đổi
        if (maxPrevLevel != control_levelSystem.maxIndexLevel || currentPrevLevel != control_levelSystem.currentIndexLevel)
        {
            levelSlider.maxValue = control_levelSystem.maxIndexLevel;
            levelSlider.value = control_levelSystem.currentIndexLevel;
            level_txt.text = control_levelSystem.currentIndexLevel.ToString() + " / " + control_levelSystem.maxIndexLevel.ToString();
            indexlevel_txt.text = control_levelSystem.indexlevel.ToString();
            maxPrevLevel = control_levelSystem.maxIndexLevel;
            currentPrevLevel = control_levelSystem.currentIndexLevel;
        }


        // Cập nhật số iCoin nếu thay đổi
        if (prevIcoin != icoinSystem.icoin)
        {
            icoin_txt.text = icoinSystem.icoin.ToString();
            prevIcoin = icoinSystem.icoin;
        }

        // Cập nhật số prevRevive nếu thay đổi
        if (prevRevive != icoinSystem.revive)
        {
            revive_txt.text = icoinSystem.revive.ToString();
            prevRevive = icoinSystem.revive;
        }
        if (prev_un_Revive != icoinSystem.un_revive)
        {
            un_revive_txt.text = icoinSystem.un_revive.ToString();
            prev_un_Revive = icoinSystem.un_revive;
        }
    }


    public void On_canvas_game_over()
    {
        StartCoroutine(DelayGameOver());
    }
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(2f);
        canvas_gameover.SetActive(true);
        Time.timeScale = 0;
    }

    public void Off_canvas_game_over()
    {
        canvas_gameover.SetActive(false);
        Time.timeScale = 1f;
    }



    public void On_canvas_Setting()
    {
        canvas_setting.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Off_canvas_Setting()
    {
        Time.timeScale = 1f;
        canvas_setting.SetActive(false);
    }

    public void on_canvas_level_up()
    {
        canvas_Level_Up.SetActive(true);
        Time.timeScale = 0f;
    }
    public void off_canvas_level_up()
    {
        canvas_Level_Up.SetActive(false);
        Time.timeScale = 1f;

    }


    public void on_canvar_book_information()
    {
        informattion_book();
        canvar_book_information.SetActive(true);
        Time.timeScale = 0f;
    }
    public void off_canvar_book_information()
    {
        canvar_book_information.SetActive(false);
        Time.timeScale = 1f;

    }

    public void informattion_book()
    {
        levelbook_txt.text = "Level " + control_attribute.control_level.indexlevel.ToString();
        healthMax_txt.text = "HealthMax : " + control_attribute.healthSystem.maxHealth.ToString();

        energyMax_txt.text = "EnergyMax : " + control_attribute.energySystem.maxEnergy.ToString();

        exp_txt.text = "Exp : " + control_attribute.control_level.currentIndexLevel.ToString();

        damge_1_txt.text = "Damge attck 1 : " + control_attribute.attach.collider_attack.damageAmount.ToString("F2");
        damge_2_txt.text = "Damge attck 2 : " + control_attribute.attach.collider_attack._damageAmount.ToString("F2");
        damge_U_txt.text = "Damage attack U : " + control_attribute.attach.collider_attack.damageAmount_.ToString("0.##");


        icon_txt.text = control_attribute.control_icoin.icoin.ToString();
        revive_book_txt.text = control_attribute.control_icoin.revive.ToString();

        numEnemiesDefeated_slimer_txt.text = "Slimer : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_slimer.ToString();
        numEnemiesDefeated_vulture_txt.text = "Vulture : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_vulture.ToString();
        numEnemiesDefeated_bunny_txt.text = "Bunny : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_bunny.ToString();
        numEnemiesDefeated_eagle_txt.text = "Eagle : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_eagle.ToString();
        numEnemiesDefeated_sketeton_txt.text = "Sketetion : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_sketeton.ToString();
        numEnemiesDefeated_goblin_txt.text = "Goblin : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_goblin.ToString();
        numEnemiesDefeated_flying_txt.text = "Flying : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_flying.ToString();
        numEnemiesDefeated_dragon_txt.text = "Dragon : " + control_attribute.enemyKillCount_Monsters.numEnemiesDefeated_dragon.ToString();



    }





}


