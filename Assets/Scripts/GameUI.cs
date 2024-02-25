using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //UI를 컨트롤 할 것이라서 추가
using TMPro;                        // TextMeshPro를 사용하기 위해 필요

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    public TMP_Text HpPoint;                
    public TMP_Text SpPoint;
    public TMP_Text XpPoint;

    public TMP_Text STR;
    public TMP_Text DEX;
    public TMP_Text CON;
    public TMP_Text INT;
    public TMP_Text WIS;
    public TMP_Text CHA;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateHpUI()
    {
        HpPoint.text = "HP : " + GameSystem.Instance.currentHpPoint;
    }

    public void UpdateSpUI()
    {
        SpPoint.text = "SP : " + GameSystem.Instance.currentSpPoint;
    }

    public void UpdateXpUI()
    {
        XpPoint.text = "XP : " + GameSystem.Instance.currentXpPoint;
    }

    public void UpdateStats()
    {
        STR.text = "STR : " + GameSystem.Instance.strength;
        DEX.text = "DEX : " + GameSystem.Instance.dexterity;
        CON.text = "CON : " + GameSystem.Instance.consitiution;
        INT.text = "INT : " + GameSystem.Instance.Intelligence;
        WIS.text = "WIS : " + GameSystem.Instance.wisdom;
        CHA.text = "CHA : " + GameSystem.Instance.charisma;
    }
}
