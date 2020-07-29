using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(RecordMenu))]
public class RecordMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        RecordMenu myScript = (RecordMenu)target;

        EditorGUILayout.ObjectField("Easy Text", myScript.EasyText, typeof(Text), false);
        EditorGUILayout.ObjectField("Medium Text", myScript.MediumText, typeof(Text), false);
        EditorGUILayout.ObjectField("Hard Text", myScript.HardText, typeof(Text), false);

        if (GUILayout.Button("Get Components & Prefabs"))
        {
            var easyTextField = serializedObject.FindProperty("easyText");
            var easyText = GameObject.FindGameObjectWithTag("EasyScore").GetComponent<Text>();
            easyTextField.objectReferenceValue = easyText;

            var mediumTextField = serializedObject.FindProperty("mediumText");
            var mediumText = GameObject.FindGameObjectWithTag("MediumScore").GetComponent<Text>();
            mediumTextField.objectReferenceValue = mediumText;

            var hardTextField = serializedObject.FindProperty("hardText");
            var hardText = GameObject.FindGameObjectWithTag("HardScore").GetComponent<Text>();
            hardTextField.objectReferenceValue = hardText;
        }
        serializedObject.ApplyModifiedProperties();
    }    
}
