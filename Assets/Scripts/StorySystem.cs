using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;               //UI를 컨트롤 할 것이라서 추가
using TMPro;                        // TextMeshPro를 사용하기 위해 필요
using UnityEngine;

public class StorySystem : MonoBehaviour
{
    public static StorySystem Instance;                 //간단한 싱글톤 화

    public StoryModel currentStoryModel;

    public enum TEXTSYSTEM    
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                  // 각 글자가 나타나는 데 걸리는 시간
    public string fullText;                     // 전체 표시할 텍스트
    private string currentText = "";            // 현재까지 표시된 텍스트
    public TMP_Text textComponent;             // TextMeshPro 컴포넌트

    public TMP_Text way01;
    public TMP_Text way02;
    public TMP_Text way03;

    public Button buttonWay01;
    public Button buttonWay02;
    public Button buttonWay03;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buttonWay01.onClick.AddListener(OnWay01Click);
        buttonWay02.onClick.AddListener(OnWay02Click);
        buttonWay03.onClick.AddListener(OnWay03Click);
    }

    public void StoryModelInit()
    {
        fullText = currentStoryModel.storyText;
        way01.text = currentStoryModel.options[0].buttonText;
        way02.text = currentStoryModel.options[1].buttonText;
    }

    public void CoShowText()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    // 버튼 리스너들
    public void OnWay01Click()
    {
        for(int i = 0; i < currentStoryModel.options[0].effects.Length; i++)
        {
            GameSystem.Instance.ApplyEffect(currentStoryModel.options[0].effects[i]);
        }
    }

    public void OnWay02Click()
    {
       
    }

    public void OnWay03Click()
    {
      
    }

}
