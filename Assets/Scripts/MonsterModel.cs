using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "ScriptableObjects/MonsterModel")]
public class MonsterModel : ScriptableObject
{
    public int MonsterNumber;

    //�⺻ ���� ����
    public int strength;            //STR
    public int dexterity;           //DEX
    public int consitiution;        //CON
    public int Intelligence;        //INT
    public int wisdom;              //WIS
    public int charisma;            //CHA

    //ü�¿� ���ŷ�
    public int hpPoint;
    public int spPoint;

    public int currentHpPoint;
    public int currentSpPoint;


}
