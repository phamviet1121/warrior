using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Saver_data_quest
{
    public int level;
    public float exp;
    public float expMax;
    public float heathMax;
    public float energyMax;
    public float damge1;
    public float damge2;
    public float damgeU;
    //public int level;
    //public int exp;
    //public int expMax;
    //public int heathMax;
    //public int energyMax;
    //public float damge1;
    //public float damge2;
    //public float damgeU;

    public Saver_data_quest(Data_quest source)
    {
        level = source.level;
        exp = source.exp;
        expMax = source.expMax;
        heathMax = source.heathMax;
        energyMax = source.energyMax;
        damge1 = source.damge1;
        damge2 = source.damge2;
        damgeU = source.damgeU;
    }
    public Data_quest ToDataQuest()
    {
        return new Data_quest
        {
            level = this.level,
            exp = this.exp,
            expMax = this.expMax,
            heathMax = this.heathMax,
            energyMax = this.energyMax,
            damge1 = this.damge1,
            damge2 = this.damge2,
            damgeU = this.damgeU
        };
    }

}
