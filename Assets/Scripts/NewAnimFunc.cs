using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NewAnimFunc : MonoBehaviour
{
    [SerializeField] enum Action { None, Move, Rotate, ChangeColor }
    [SerializeField] Action _Action;

    #region changeColorParams
    [SerializeField] private Color ToChangeColor;
    [SerializeField] [Range(0f, 1f)] float ChangeTime;
    private Renderer myObj;
    private bool startreact = false;
    #endregion

    #region moveParams
    [SerializeField] List<Vector3> MoveTo = new List<Vector3>();
    [SerializeField] float Speed;
    [System.FlagsAttribute] enum Conditions { None, ComparePositionGO, ActivityIerarchy, }
    [SerializeField] Conditions _Conditions;
    [SerializeField] List<GameObject> Left = new List<GameObject>(); //позиция объетка справа
    [SerializeField] List<GameObject> Right = new List<GameObject>(); //позиция объетка слева
    [SerializeField] List<GameObject> GOActive = new List<GameObject>();
    private int next = 0;
    private bool notdone = true;
    private bool startMove = false;
    #endregion

    private void Start()
    {
        myObj = transform.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        if (_Action == Action.Move)
        {
            if (startMove)
            {
                Moving();
            }
            if (notdone && CheckCondition(Left, Right))
            {
                startMove = true;
            }
            else if (notdone == false)
            {
                startMove = false;
                notdone = true;
                next = 0;
            }
        }
        if (_Action == Action.ChangeColor)
        {
            if (startreact)
            {
                Debug.Log("Enter");
                Reaction();
            }
            if (CheckCondition(Left, Right))
            {
                startreact = true;
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
        if (_Conditions == Conditions.ActivityIerarchy)
        {
            //активность объекта в сцене
        }
        if (_Conditions == Conditions.ActivityIerarchy && _Conditions == Conditions.ComparePositionGO)
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
            }
            else
            {
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
    private void Reaction()
    {
        myObj.material.color = Color.Lerp(myObj.material.color, ToChangeColor, ChangeTime * Time.deltaTime);
        if (myObj.material.color.ToString() == ToChangeColor.ToString())
        {
            startreact = false;
        }
    }
    #region CustomEditor
    [CustomEditor(typeof(NewAnimFunc))]
    public class CustomScript : Editor
    {
        #region MoveParams

        SerializedProperty MoveTo;
        SerializedProperty Speed;
        SerializedProperty _Action;
        SerializedProperty _Conditions;
        SerializedProperty Left;
        SerializedProperty Right;
        SerializedProperty GOActive;
        #endregion
        SerializedProperty ToChangeColor;
        SerializedProperty ChangeTime;
        void OnEnable()
        {
            MoveTo = serializedObject.FindProperty("MoveTo");
            _Action = serializedObject.FindProperty("_Action");
            Speed = serializedObject.FindProperty("Speed");
            _Conditions = serializedObject.FindProperty("_Conditions");
            Left = serializedObject.FindProperty("Left");
            Right = serializedObject.FindProperty("Right");
            GOActive = serializedObject.FindProperty("GOActive");
            ToChangeColor = serializedObject.FindProperty("ToChangeColor");
            ChangeTime = serializedObject.FindProperty("ChangeTime");
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
            if (_Action.enumValueIndex == 3) //Добавление в Move поле
            {
                EditorGUILayout.PropertyField(ToChangeColor);
                EditorGUILayout.PropertyField(ChangeTime);
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
