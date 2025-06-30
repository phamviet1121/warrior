using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Control_icoin : MonoBehaviour
{
    public float icoin;
    public float revive;
    public float un_revive;

    private const string IcoinKey = "PlayerIcoin";
    private const string ReviveKey = "PlayerRevive";

    void Start()
    {
        LoadData(); // Tải lại khi bắt đầu game
    }
    public void increase_icoin(float nuber)
    {
        icoin += nuber;
        SaveData();
    }    

    public void increase_revive(float nuber)
    {
        revive += nuber;
        SaveData();
    }   
    public void replay_revive()
    {
        if(revive>= un_revive)
        {
            revive -= un_revive;
        }
        SaveData();


    }


    public void SaveData()
    {
        PlayerPrefs.SetFloat(IcoinKey, icoin);
        PlayerPrefs.SetFloat(ReviveKey, revive);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        //if (PlayerPrefs.HasKey(IcoinKey))
        //    icoin = PlayerPrefs.GetFloat(IcoinKey);

        //if (PlayerPrefs.HasKey(ReviveKey))
        //    revive = PlayerPrefs.GetFloat(ReviveKey);
        icoin = PlayerPrefs.GetFloat(IcoinKey, 0f);
        revive = PlayerPrefs.GetFloat(ReviveKey, 0f);

    }

    public void ClearData() // Nếu muốn reset dữ liệu (ví dụ nút "Chơi mới")
    {
        PlayerPrefs.DeleteKey(IcoinKey);
        PlayerPrefs.DeleteKey(ReviveKey);
        icoin = 0;
        revive = 0;
    }
}
