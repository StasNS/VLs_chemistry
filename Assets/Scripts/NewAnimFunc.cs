using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NewAnimFunc : MonoBehaviour
{
    [SerializeField] enum Action { None, Move, Rotate }
    [SerializeField] Action _Action;
    [SerializeField] List<Vector3> MoveTo = new List<Vector3>();
    [SerializeField] float Speed;
    [System.FlagsAttribute] enum Conditions { None, ComparePositionGO, ActivityIerarchy, }
    [SerializeField] Conditions _Conditions;
    [SerializeField] List<GameObject> Left = new List<GameObject>(); //позиция объетка справа
    [SerializeField] List<GameObject> Right = new List<GameObject>(); //позиция объетка слева
    [SerializeField] List<GameObject> GOActive = new List<GameObject>();
    private int next = 0;
    private int condIter = 0;
    private bool notdone = true;
    private void FixedUpdate()
    {
        if (_Action == Action.Move)
        {
            if (notdone && CheckCondition(Left, Right))
            {
                Moving();
            }
        }
    }

    private bool CheckCondition(List<GameObject> _left, List<GameObject> _right)
    {
        bool result = false;

        if (_Conditions == Conditions.None)
        {
            result = true;
        }
        if (_Conditions == Conditions.ComparePositionGO)
        {
            result = ComparePose(_left, _right);
        }
        if(_Conditions == Conditions.ActivityIerarchy)
        {
            //активность объекта в сцене
        }
        if(_Conditions == Conditions.ActivityIerarchy && _Conditions == Conditions.ComparePositionGO)
        {
            
        }
        return result;
    }
    private bool ComparePose(List<GameObject> _leftPos, List<GameObject> _rightPos)
    {
        var res = false;
        for (int i = 0; i < _leftPos.Count; i++)
        {
            if (_leftPos[i].transform.position == _rightPos[i].transform.position)
            {
                res = true;
                Debug.Log(i);
            }
            else
            {
                Debug.Log("Enter");
                res = false;
                break;
            }
        }
        return res;
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
        SerializedProperty Left;
        SerializedProperty Right;
        SerializedProperty GOActive;
        void OnEnable()
        {
            MoveTo = serializedObject.FindProperty("MoveTo");
            _Action = serializedObject.FindProperty("_Action");
            Speed = serializedObject.FindProperty("Speed");
            _Conditions = serializedObject.FindProperty("_Conditions");
            Left = serializedObject.FindProperty("Left");
            Right = serializedObject.FindProperty("Right");
            GOActive = serializedObject.FindProperty("GOActive");
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
                EditorGUILayout.PropertyField(Left);
                EditorGUILayout.PropertyField(Right);
            }
            if (_Conditions.enumValueIndex == 2)
            {
                EditorGUILayout.PropertyField(GOActive);
            }
            if (_Conditions.enumValueIndex == -1)
            {
                EditorGUILayout.PropertyField(Left);
                EditorGUILayout.PropertyField(Right);
                EditorGUILayout.PropertyField(GOActive);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endregion
