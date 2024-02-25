using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        // Reset Story Models ��ư ����
        if (GUILayout.Button("Reset Story Models"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;                 //������ �̱��� ȭ

    private void Awake()
    {
        Instance = this;
    }

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
    public int spPoint;

    public int currentHpPoint;
    public int currentSpPoint;

    public int currentXpPoint;

    //�⺻ ���� ����
    public int strength;            //STR
    public int dexterity;           //DEX
    public int consitiution;        //CON
    public int Intelligence;        //INT
    public int wisdom;              //WIS
    public int charisma;            //CHA


    public GAMESTATE currentState;

    public StoryModel[] storyModels;
    public int currentStoryIndex = 1;

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryModel>(""); // Resources ���� �Ʒ� ��� StoryModel �ҷ�����
    }
#endif

    public void Start()
    {
        ChangeState(GAMESTATE.STORYSHOW);
    }

    public void ApplyEffect(StoryModel.Effect effect)
    {
        switch (effect.effectType)
        {
            case StoryModel.Effect.EffectType.ChangeHp:
                currentHpPoint += effect.value;
                GameUI.Instance.UpdateHpUI();
                break;
            case StoryModel.Effect.EffectType.AddExperience:
                currentXpPoint += effect.value;
                GameUI.Instance.UpdateXpUI();
                break;
            case StoryModel.Effect.EffectType.GoToNextStory:
                currentStoryIndex = effect.value;
                ChangeState(GAMESTATE.STORYSHOW);

                break;
            default:
                Debug.LogError("Unknown effect type");
                break;
        }
    }

    public void ChangeState(GAMESTATE temp)
    {
        currentState = temp;

        if(currentState == GAMESTATE.STORYSHOW)
        {
            StoryShow(currentStoryIndex);
        }
    }

    public void StoryShow(int number)
    {

        StoryModel tempStoryModels = FindStoryModel(number);

        StorySystem.Instance.currentStoryModel = tempStoryModels;
        StorySystem.Instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storyNumber == number)
            {
                tempStoryModels = storyModels[i];
                break;
                
            }
        }

        return tempStoryModels;
    }

}
