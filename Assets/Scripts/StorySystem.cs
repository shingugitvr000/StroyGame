using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;               //UI를 컨트롤 할 것이라서 추가
using TMPro;                        // TextMeshPro를 사용하기 위해 필요
using UnityEngine;
using System;

public class StorySystem : MonoBehaviour
{
    public static StorySystem Instance;                 //간단한 싱글톤 화

    public StoryTableObject currentStoryModel;

    public enum TEXTSYSTEM    
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                  // 각 글자가 나타나는 데 걸리는 시간
    public string fullText;                     // 전체 표시할 텍스트
    private string currentText = "";            // 현재까지 표시된 텍스트
    public TMP_Text textComponent;              // TextMeshPro 컴포넌트
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
            int wayIndex = i; //클로저(closure) 문제를 해결
            // 클로저 문제란 람다식 또는 익명 함수가 외부 변수를 캡처할 때 발생하는 문제
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
        if(currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.NONE)
        {
            for (int i = 0; i < currentStoryModel.options[index].eventCheck.sucessResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].eventCheck.sucessResult[i]);
            }
        }

        bool CheckValue = false;

        if (currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.CheckSTR)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.statsTalbeOject.statsData.strength) >= currentStoryModel.options[index].eventCheck.checkvalue)
            {
                CheckValue = true;
            }           
        }
        else if (currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.CheckDEX)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.statsTalbeOject.statsData.dexterity) >= currentStoryModel.options[index].eventCheck.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.CheckCON)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.statsTalbeOject.statsData.consitiution) >= currentStoryModel.options[index].eventCheck.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.CheckINT)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.statsTalbeOject.statsData.Intelligence) >= currentStoryModel.options[index].eventCheck.checkvalue)
            {
                CheckValue = true;
            }
        }
        else if (currentStoryModel.options[index].eventCheck.eventType == StoryTableObject.EventCheck.EventType.CheckCHA)
        {
            if (UnityEngine.Random.Range(0, GameSystem.Instance.statsTalbeOject.statsData.charisma) >= currentStoryModel.options[index].eventCheck.checkvalue)
            {
                CheckValue = true;
            }
        }


        if (CheckValue)
        {
            for (int i = 0; i < currentStoryModel.options[index].eventCheck.sucessResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].eventCheck.sucessResult[i]);
            }
        }
        else
        {
            for (int i = 0; i < currentStoryModel.options[index].eventCheck.failResult.Length; i++)
            {
                GameSystem.Instance.ApplyChoice(currentStoryModel.options[index].eventCheck.failResult[i]);
            }
        }
    }

}
