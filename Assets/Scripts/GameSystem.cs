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

    //=========================================================================================
    // ����
    //=========================================================================================


    public Stats stats;



    public GAMESTATE currentState;

    //=========================================================================================
    // ���� 
    //=========================================================================================

    // ���� ���� ���
    private const int BASE_DAMAGE = 1; // �⺻ ���ط�

    private System.Random random = new System.Random(); // ���� ���� ������

    // ���� ���� ���
    private const int D20_MAX = 20;
    private const int CRITICAL_HIT_THRESHOLD = 20;
    private const int CRITICAL_HIT_MULTIPLIER = 2;

    // ���� �α׸� ������ StringBuilder ��ü
    private StringBuilder battleLog = new StringBuilder();

    //=========================================================================================
    // ���丮
    //=========================================================================================

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

    public void ApplyChoice(StoryModel.Result result)
    {       
        switch (result.resultType)
        {
            case StoryModel.Result.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                GameUI.Instance.UpdateHpUI();
                break;

            case StoryModel.Result.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                GameUI.Instance.UpdateXpUI();
                break;

            case StoryModel.Result.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STORYSHOW);
                break;

            case StoryModel.Result.ResultType.GoToRandomStory:
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

        StoryModel tempStoryModels = FindStoryModel(number);

        StorySystem.Instance.currentStoryModel = tempStoryModels;
        StorySystem.Instance.CoShowText();
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel> StoryModelList = new List<StoryModel>();

        for (int i = 0; i < storyModels.Length; i++)
        {
            if (storyModels[i].storytype == StoryModel.STORYTYPE.MAIN)
            {
                StoryModelList.Add(storyModels[i]);              
            }
        }

        tempStoryModels = StoryModelList[Random.Range(0, StoryModelList.Count)];

        currentStoryIndex = tempStoryModels.storyNumber;

        Debug.Log("currentStoryIndex" + currentStoryIndex);

        return tempStoryModels;
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

    //=========================================================================================
    // ���� �α�
    //=========================================================================================

    

    // ���� ���� �� ȣ��Ǵ� �Լ�
    public void StartBattle()
    {
        // ���� �α� �ʱ�ȭ
        battleLog.Clear();
        AppendToBattleLog("���� ����!");

        // ���⼭ ���� ������ �����ϰ�, ���� ��Ȳ�� ���� AppendToBattleLog �Լ��� ȣ���Ͽ� �α׸� ����մϴ�.
        // ���� ���,
        // AppendToBattleLog("���� �����߽��ϴ�!");
        // AppendToBattleLog("�÷��̾ �ݰ��߽��ϴ�!");

        // ���� ���� �� ���� �α� ���
        Debug.Log(battleLog.ToString());
    }

    // ���� �α׿� ���� �߰��ϴ� �Լ�
    private void AppendToBattleLog(string log)
    {
        battleLog.AppendLine(log);
    }

    //=========================================================================================
    // ����
    //=========================================================================================

    public void Combat()
    {
        int attackRoll = RollD20(); // ���� �ֻ��� ������
        int damage = 0;

        if (attackRoll == D20_MAX) // 20�� ������ ũ��Ƽ�� ��Ʈ
        {
            damage = RollDice(1, 6) * CRITICAL_HIT_MULTIPLIER;
            AppendToBattleLog("ũ��Ƽ�� ��Ʈ!");
        }
        else
        {
            // ���ط� ��� (�Ϲ� ����)
            damage = RollDice(1, 6);
        }

        // ���ط� ���� �� �ٸ� ���� ���� ó��
        // ���� ���,
        // player.TakeDamage(damage);

        // ���� �α׿� ��� �߰�
        AppendToBattleLog($"���� �ֻ��� ������ ���: {attackRoll}");
        AppendToBattleLog($"���ط�: {damage}");
    }

    // D20 �ֻ����� ���� ����� ��ȯ�ϴ� �Լ�
    private int RollD20()
    {
        return random.Next(1, D20_MAX + 1);
    }

    // �ֻ����� numDice�� ���� ���� ���� �ջ��Ͽ� ��ȯ�ϴ� �Լ�
    private int RollDice(int numDice, int diceSides)
    {
        int total = 0;
        for (int i = 0; i < numDice; i++)
        {
            total += random.Next(1, diceSides + 1);
        }
        return total;
    }

}
