using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(GameOverMenu))]
public class GameOverMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GameOverMenu myScript = (GameOverMenu)target;

        EditorGUILayout.ObjectField("Score Child", myScript.Score, typeof(Text), false);

        if (GUILayout.Button("Get Components & Prefabs"))
        {
            var scoreChieldField = serializedObject.FindProperty("scoreChild");
            var scoreChild = myScript.gameObject.GetComponentInChildren<Text>();
            scoreChieldField.objectReferenceValue = scoreChild;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
