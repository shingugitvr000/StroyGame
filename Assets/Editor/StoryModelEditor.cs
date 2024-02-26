using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StoryModel))]
public class StoryModelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 기본 스크립터블 오브젝트 필드 표시
        base.OnInspectorGUI();

        StoryModel storyModel = (StoryModel)target;

        //// 선택지 배열 UI 커스터마이징
        //if (GUILayout.Button("Expand Options"))
        //{
        //    storyModel.showOptions = !storyModel.showOptions;
        //}

        //if (storyModel.showOptions)
        //{
        //    EditorGUI.indentLevel++;
        //    for (int i = 0; i < storyModel.options.Length; i++)
        //    {
        //        EditorGUILayout.BeginVertical(GUI.skin.box);
        //        EditorGUILayout.LabelField($"Option {i + 1}", EditorStyles.boldLabel);
        //        storyModel.options[i].optionText = EditorGUILayout.TextField("Option Text", storyModel.options[i].optionText);
        //        storyModel.options[i].buttonText = EditorGUILayout.TextField("Button Text", storyModel.options[i].buttonText);

        //        // Effect 배열에 대한 UI 추가
        //        if (storyModel.options[i].effects != null && storyModel.options[i].effects.Length > 0)
        //        {
        //            EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);
        //            EditorGUI.indentLevel++;
        //            for (int j = 0; j < storyModel.options[i].effects.Length; j++)
        //            {
        //                EditorGUILayout.BeginVertical(GUI.skin.box);
        //                EditorGUILayout.EnumPopup("Effect Type", storyModel.options[i].effects[j].effectType);
        //                storyModel.options[i].effects[j].value = EditorGUILayout.IntField("Value", storyModel.options[i].effects[j].value);
        //                EditorGUILayout.EndVertical();
        //            }
        //            EditorGUI.indentLevel--;
        //        }

        //        EditorGUILayout.EndVertical();
        //    }
        //    EditorGUI.indentLevel--;
        //}

        // 변경 사항이 있을 때 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
