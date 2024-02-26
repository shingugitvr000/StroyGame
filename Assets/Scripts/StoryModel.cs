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

    public Option[] options; // 선택지 배열


    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText; // 선택지 버튼의 이름

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

        public Result[] sucessResult; // 선택지에 대한 효과 배열
        public Result[] failResult; // 선택지에 대한 효과 배열
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

    //// 선택지를 선택할 때 영향을 주는 메서드
    //public void ApplyChoice(int optionIndex, GameSystem gameSystem)
    //{
    //    foreach (Choice choice in options[optionIndex].choices)
    //    {
    //        gameSystem.ApplyChoice(choice);
    //    }
    //}


}
