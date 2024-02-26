using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;
    public bool storyDone;

    public string storyText;

    public Option[] options; // 선택지 배열
    public bool showOptions = false; // 기본적으로는 false로 설정


    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText; // 선택지 버튼의 이름
        public Effect[] effects; // 선택지에 대한 효과 배열
    }

    [System.Serializable]
    public class Effect
    {
        public enum EffectType
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToBattle,
            GoToShop,
            GoToNextStory,
            GoToEnding
        }

        public EffectType effectType;
        public int value;
    }

    // 선택지를 선택할 때 영향을 주는 메서드
    public void ApplyEffects(int optionIndex, GameSystem gameSystem)
    {
        foreach (Effect effect in options[optionIndex].effects)
        {
            gameSystem.ApplyEffect(effect);
        }
    }


}
