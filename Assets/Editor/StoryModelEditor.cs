using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoryModel))]
public class StoryModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ ��ũ���ͺ� ������Ʈ �ʵ� ǥ��
        base.OnInspectorGUI();

        StoryModel storyModel = (StoryModel)target;

        // ���� ������ ���� �� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
