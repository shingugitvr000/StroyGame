using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameSystem gameSystem = (GameSystem)target;

        // Reset Story Models 버튼 생성
        if (GUILayout.Button("Reset Story Models"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;                 //간단한 싱글톤 화

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

    public StatsTableObject statsTalbeOject;

    public GAMESTATE currentState;
    public StoryTableObject[] storyModels;
    public int currentStoryIndex = 1;

#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        storyModels = Resources.LoadAll<StoryTableObject>(""); // Resources 폴더 아래 모든 StoryModel 불러오기
    }
#endif

    public void Start()
    {
        ChangeState(GAMESTATE.STORYSHOW);
    }

    public void ApplyChoice(StoryTableObject.Result result)
    {       
        switch (result.resultType)
        {
            case StoryTableObject.Result.ResultType.ChangeHp:
                statsTalbeOject.statsData.currentHpPoint += result.value;
                GameUI.Instance.UpdateHpUI();
                break;

            case StoryTableObject.Result.ResultType.AddExperience:
                statsTalbeOject.statsData.currentXpPoint += result.value;
                GameUI.Instance.UpdateXpUI();
                break;

            case StoryTableObject.Result.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STORYSHOW);
                break;

            case StoryTableObject.Result.ResultType.GoToRandomStory:
                RandomStory();
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

        StoryTableObject tempStoryModels = FindStoryModel(number);

        StorySystem.Instance.currentStoryModel = tempStoryModels;
        StorySystem.Instance.CoShowText();
    }

    StoryTableObject RandomStory()
    {
        StoryTableObject tempStoryModels = null;

        List<StoryTableObject> StoryModelList = new List<StoryTableObject>();

        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storytype == StoryTableObject.STORYTYPE.MAIN)
            {
                StoryModelList.Add(storyModels[i]);              
            }
        }

        tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];

        currentStoryIndex = tempStoryModels.storyNumber;

        Debug.Log("currentStoryIndex" + currentStoryIndex);

        return tempStoryModels;
    }

    StoryTableObject FindStoryModel(int number)
    {
        StoryTableObject tempStoryModels = null;
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
