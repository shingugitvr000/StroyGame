using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;               //UI�� ��Ʈ�� �� ���̶� �߰�
using TMPro;                        // TextMeshPro�� ����ϱ� ���� �ʿ�
using UnityEngine;
using System;

public class StorySystem : MonoBehaviour
{
    public static StorySystem Instance;                 //������ �̱��� ȭ

    public StoryModel currentStoryModel;

    public enum TEXTSYSTEM    
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                  // �� ���ڰ� ��Ÿ���� �� �ɸ��� �ð�
    public string fullText;                     // ��ü ǥ���� �ؽ�Ʈ
    private string currentText = "";            // ������� ǥ�õ� �ؽ�Ʈ
    public TMP_Text textComponent;              // TextMeshPro ������Ʈ
    public TMP_Text storyIndex;                 // storyIndex 

    public Button[] buttonWay = new Button[3];
    public TMP_Text[] buttonWayText = new TMP_Text[3];



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < buttonWay.Length; i++)
        {
            int wayIndex = i; //Ŭ����(closure) ������ �ذ�
            // Ŭ���� ������ ���ٽ� �Ǵ� �͸� �Լ��� �ܺ� ������ ĸó�� �� �߻��ϴ� ����
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));
        }
    }


    public void StoryModelInit()
    {
        fullText = currentStoryModel.storyText;

        storyIndex.text = currentStoryModel.storyNumber.ToString();

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;
        }        
    }

    public void CoShowText()
    {
        StoryModelInit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    public void ResetShow()
    {
        textComponent.text = "";

        for (int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true); 
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);
    }

    public void OnWayClick(int index)
    {
        if(currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.NONE)
        {
            for (int i = 0; i < currentStoryModel.options[index].check.sucessResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].check.sucessResult[i]);
            }
        }

        bool CheckValue = false;

        if (currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.CheckSTR)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.stats.strength) >= currentStoryModel.options[index].check.checkvalue)
            {
                CheckValue = true;
            }           
        }
        else if (currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.CheckDEX)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.stats.dexterity) >= currentStoryModel.options[index].check.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.CheckCON)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.stats.consitiution) >= currentStoryModel.options[index].check.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.CheckINT)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.stats.Intelligence) >= currentStoryModel.options[index].check.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].check.checkType == StoryModel.Check.CheckType.CheckCHA)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.stats.charisma) >= currentStoryModel.options[index].check.checkvalue)
            {
                CheckValue = true;
            }
        }


        if (CheckValue)
        {
            for (int i = 0; i < currentStoryModel.options[index].check.sucessResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].check.sucessResult[i]);
            }
        }
        else
        {
            for (int i = 0; i < currentStoryModel.options[index].check.failResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].check.failResult[i]);
            }
        }
    }

}
