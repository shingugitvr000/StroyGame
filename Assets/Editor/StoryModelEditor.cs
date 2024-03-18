using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoryTableObject))]
public class StoryModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // �⺻ ��ũ���ͺ� ������Ʈ �ʵ� ǥ��
        base.OnInspectorGUI();

        StoryTableObject storyModel = (StoryTableObject)target;

        // ���� ������ ���� �� ����
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
