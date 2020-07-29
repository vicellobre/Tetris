using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ConfigurationMenu))]
public class ConfigurationMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();
        EditorGUILayout.Separator(); EditorGUILayout.Separator(); EditorGUILayout.Separator();

        ConfigurationMenu myScript = (ConfigurationMenu)target;

        EditorGUILayout.LabelField("Fields assigned by button");
        EditorGUILayout.ObjectField("Button Music", myScript.ButtonMusic, typeof(Image), false);
        
        if (GUILayout.Button("Get Components & Prefabs"))
        {
            var buttonMusicField = serializedObject.FindProperty("buttonMusic");
            var buttonMusic = GameObject.FindGameObjectWithTag("ButtonMusic").GetComponent<Image>();
            buttonMusicField.objectReferenceValue = buttonMusic;
        }

        serializedObject.ApplyModifiedProperties();
    }
}