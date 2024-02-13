using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{

    public enum GAMESTATE
    {
        STORYSHOW,
        WAITSELECT,
        STORYEND,
        BATTLEMODE,
        BATTLEDONE,
        SHOPMODE,
        ENDMODE

    }


    //ü�¿� ���ŷ�
    public int hpPoint;
    public int mpPoint;

    public int currentHpPoint;
    public int currentMpPoint;

    //�⺻ ���� ����
    public int strength;            //STR
    public int dexterity;           //DEX
    public int consitiution;        //CON
    public int Intelligence;        //INT
    public int wisdom;              //WIS
    public int charisma;            //CHA


    

   
}
