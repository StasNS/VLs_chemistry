using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NewAnimFunc : MonoBehaviour
{
    [SerializeField] enum Action { None, Move, Rotate }
    [SerializeField] Action _Action;
    [SerializeField] List<Vector3> MoveTo = new List<Vector3>();
    [SerializeField] float Speed;
    [SerializeField] enum Conditions { None, ComparePosition }
    [SerializeField] Conditions _Conditions;
    private int next = 0;
    private bool notdone = true;
    private void FixedUpdate()
    {
        if (_Action == Action.Move)
        {
            if (notdone)
            {
                Moving();
            }
        }
    }
    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveTo[next], Speed * Time.deltaTime);
        if (transform.position == MoveTo[next])
        {
            next++;
        }
        if (next == MoveTo.Count) notdone = false;
    }

    #region CustomEditor
    [CustomEditor(typeof(NewAnimFunc))]
    public class CustomScript : Editor
    {
        SerializedProperty MoveTo;
        SerializedProperty Speed;
        SerializedProperty _Action;
        SerializedProperty _Conditions;
        void OnEnable()
        {
            MoveTo = serializedObject.FindProperty("MoveTo");
            _Action = serializedObject.FindProperty("_Action");
            Speed = serializedObject.FindProperty("Speed");
            _Conditions = serializedObject.FindProperty("_Conditions");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_Action);

            if (_Action.enumValueIndex == 1) //Добавление в Move поле
            {
                EditorGUILayout.PropertyField(MoveTo);
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(_Conditions);
            }
            if (_Conditions.enumValueIndex == 1)
            {

            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endregion
