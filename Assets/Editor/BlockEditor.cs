using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{

    public override void  OnInspectorGUI()
    {
        serializedObject.Update();

        Block myScript = (Block)target;

        EditorGUILayout.LabelField("Fields only-read");
        EditorGUILayout.Toggle("Added", myScript.Added);
        EditorGUILayout.Toggle("Stop Left", myScript.StopLeft);
        EditorGUILayout.Toggle("Stop Rigth", myScript.StopRight);
        EditorGUILayout.EnumFlagsField("Color", myScript.MyColor);
        EditorGUILayout.EnumFlagsField("Type", myScript.MyType);
        EditorGUILayout.Separator();

        if (GUILayout.Button("Get Components & Prefabs"))
        {
            var rigidBody2DField = serializedObject.FindProperty("rigidbody2D");
            var rigidBody2D = myScript.GetComponent<Rigidbody2D>();
            rigidBody2DField.objectReferenceValue = rigidBody2D;

        }
        EditorGUILayout.ObjectField(myScript.Rigidbody, typeof(Rigidbody2D), false);

        serializedObject.ApplyModifiedProperties();
    }
}
