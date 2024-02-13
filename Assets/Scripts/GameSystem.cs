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


    //체력와 정신력
    public int hpPoint;
    public int mpPoint;

    public int currentHpPoint;
    public int currentMpPoint;

    //기본 스텟 설정
    public int strength;            //STR
    public int dexterity;           //DEX
    public int consitiution;        //CON
    public int Intelligence;        //INT
    public int wisdom;              //WIS
    public int charisma;            //CHA


    

   
}
