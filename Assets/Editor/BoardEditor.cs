using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Board myScript = (Board)target;


        EditorGUILayout.FloatField("Position X", myScript.PositionX);
        EditorGUILayout.FloatField("Tolerance", myScript.Tolerance);
        EditorGUILayout.Space();

        EditorGUILayout.FloatField("Width", myScript.Width);
        EditorGUILayout.FloatField("Height", myScript.Height);
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Get Components & Prefabs"))
        {   // Asignando el prefab Square
            var squareField = serializedObject.FindProperty("square");
            var square = Resources.Load<GameObject>("Prefabs/square");
            squareField.objectReferenceValue = square;

            // Asignando el gameObject HUD
            var hudField = serializedObject.FindProperty("hud");
            var hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<RectTransform>();
            hudField.objectReferenceValue = hud;

            // Asignando el gameObject Container
            var containerField = serializedObject.FindProperty("container");
            var container = GameObject.FindGameObjectWithTag("Container").GetComponent<RectTransform>();
            containerField.objectReferenceValue = container;

            // Asignando los prefabs de Piece
            myScript.GetPieces();

            // Asignando la posicion del gameObject Table
            var tableField = serializedObject.FindProperty("table");
            var table = GameObject.FindGameObjectWithTag("Table").transform.position;
            tableField.vector3Value = table;
        }

        EditorGUILayout.LabelField("Square");
        EditorGUILayout.ObjectField(myScript.Square, typeof(GameObject), false);

        EditorGUILayout.LabelField("Container");
        EditorGUILayout.ObjectField(myScript.Container, typeof(RectTransform), false);

        EditorGUILayout.LabelField("HUD");
        EditorGUILayout.ObjectField(myScript.Hud, typeof(RectTransform), false);

        var piecesField = serializedObject.FindProperty("pieces");
        EditorGUILayout.PropertyField(piecesField, true);

        EditorGUILayout.Vector3Field("Table", myScript.Table);

        serializedObject.ApplyModifiedProperties();
    }

}