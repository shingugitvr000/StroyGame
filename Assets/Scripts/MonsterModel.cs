using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "ScriptableObjects/MonsterModel")]
public class MonsterModel : ScriptableObject
{
    public int MonsterNumber;

    //기본 스텟 설정
    public int strength;            //STR
    public int dexterity;           //DEX
    public int consitiution;        //CON
    public int Intelligence;        //INT
    public int wisdom;              //WIS
    public int charisma;            //CHA

    //체력와 정신력
    public int hpPoint;
    public int spPoint;

    public int currentHpPoint;
    public int currentSpPoint;


}
