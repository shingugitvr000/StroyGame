using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;
    
    public enum STORYTYPE
    {
        MAIN,
        SUB,
        SERIAL
    }

    public STORYTYPE storytype;

    public bool storyDone;

    public string storyText;

    public Option[] options; // ������ �迭


    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText; // ������ ��ư�� �̸�

        public Check check;
    }

    [System.Serializable]
    public class Check
    {
        public int checkvalue;
        public enum CheckType : int
        {
            NONE,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA

        }

        public CheckType checkType;

        public Result[] sucessResult; // �������� ���� ȿ�� �迭
        public Result[] failResult; // �������� ���� ȿ�� �迭
    }

    [System.Serializable]
    public class Result
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToBattle,
            GoToShop,
            GoToNextStory,
            GoToRandomStory,
            GoToEnding 
        }

        public ResultType resultType;
        public int value;
        public Stats stats;
    }

    //// �������� ������ �� ������ �ִ� �޼���
    //public void ApplyChoice(int optionIndex, GameSystem gameSystem)
    //{
    //    foreach (Choice choice in options[optionIndex].choices)
    //    {
    //        gameSystem.ApplyChoice(choice);
    //    }
    //}


}
