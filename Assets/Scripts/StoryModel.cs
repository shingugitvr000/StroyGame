using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;
    public bool storyDone;

    public string storyText;

    public Option[] options; // ������ �迭

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText; // ������ ��ư�� �̸�
        public Effect[] effects; // �������� ���� ȿ�� �迭
    }

    [System.Serializable]
    public class Effect
    {
        public enum EffectType
        {
            AddHealth,
            AddExperience,
            GoToNextStory
        }

        public EffectType effectType;
        public int value;
    }

    // �������� ������ �� ������ �ִ� �޼���
    public void ApplyEffects(int optionIndex, GameSystem gameSystem)
    {
        foreach (Effect effect in options[optionIndex].effects)
        {
            gameSystem.ApplyEffect(effect);
        }
    }


}
