using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(Memo))]
public class MemoEditorWindow : Editor
{
    public SerializedObject so;
    public SerializedProperty propMemo;
    public SerializedProperty propLock;

    private void OnEnable()
    {
        so = serializedObject;
        propMemo = so.FindProperty("_memo");
        propLock = so.FindProperty("_lock");
    }
    
    public override void OnInspectorGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(propLock);
        so.ApplyModifiedProperties();
        
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new EditorGUI.DisabledScope(propLock.boolValue))
            {
                EditorGUILayout.PropertyField(propMemo,GUILayout.Height(200));
            }
        }

        so.ApplyModifiedProperties();
    }
}