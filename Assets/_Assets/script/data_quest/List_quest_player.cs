using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataList", menuName = "ScriptableObjects/Quests", order = 1)]
public class List_quest_player : ScriptableObject
{
    [SerializeField]
    public List<Data_quest> DataQuest;
}
