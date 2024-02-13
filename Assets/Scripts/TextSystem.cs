using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;               //UI�� ��Ʈ�� �� ���̶� �߰�
using TMPro;                        // TextMeshPro�� ����ϱ� ���� �ʿ�
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    public enum TEXTSYSTEM    
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                  // �� ���ڰ� ��Ÿ���� �� �ɸ��� �ð�
    public string fullText;                     // ��ü ǥ���� �ؽ�Ʈ
    private string currentText = "";            // ������� ǥ�õ� �ؽ�Ʈ
    public TMP_Text textComponent;             // TextMeshPro ������Ʈ

    // Start is called before the first frame update
    void Start()
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

}
