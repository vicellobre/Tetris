using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Piece))]
public class PieceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Piece myScript = (Piece)target;

        EditorGUILayout.Vector2Field("Direction", myScript.Direction);

        EditorGUILayout.Toggle("Fix Position", myScript.FixPosition);
        EditorGUILayout.Toggle("Move to Left", myScript.ItCanMoveToLeft);
        EditorGUILayout.Toggle("Move to Rigth", myScript.ItCanMoveToRight);
        EditorGUILayout.Toggle("Rotate", myScript.ItCanRotate);
        EditorGUILayout.Separator();

        EditorGUILayout.FloatField("Tolerance", myScript.Tolerance);
        EditorGUILayout.IntField("Children", myScript.Children);
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