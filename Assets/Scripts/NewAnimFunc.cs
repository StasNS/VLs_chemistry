using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NewAnimFunc : MonoBehaviour
{
    [SerializeField] enum Action { None, Move, Rotate }
    [SerializeField] Action _Action;
    [SerializeField] List<Vector3> MoveTo = new List<Vector3>();
    [SerializeField] float Speed;
    [SerializeField] float[] time;
    private int current = 0;
    private float timer = 0f;
    private int next = 1;

    private void FixedUpdate()
    {
        if (_Action == Action.Move)
        {
            Moving();
        }
    }
    private void Moving()
    {
        foreach (var point in MoveTo)
        {
            // transform.position = Vector3.MoveTowards(transform.position,point,Speed*Time.deltaTime);
            transform.position = Vector3.Lerp(point[current], point[next], timer / time[current]);
        }
        timer += Time.deltaTime;
        if (timer >= time[current])
        {
            timer = 0f;
            current = next;
            next++;
            if (next == points.Length) next = 0;
        }
    }

    #region CustomEditor
    [CustomEditor(typeof(NewAnimFunc))]
    public class CustomScript : Editor
    {
        SerializedProperty MoveTo;
        SerializedProperty Speed;
        SerializedProperty _Action;
        void OnEnable()
        {
            MoveTo = serializedObject.FindProperty("MoveTo");
            _Action = serializedObject.FindProperty("_Action");
            Speed = serializedObject.FindProperty("Speed");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_Action);

            if (_Action.enumValueIndex == 0)
            {
                //Скрыть все
            }
            if (_Action.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(MoveTo); //Добавление списка точек перемещения
                EditorGUILayout.PropertyField(Speed);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endregion
