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
    [System.FlagsAttribute] enum Conditions { None, ComparePositionGO, ActivityIerarchy, CompGOandActIe, CompareVector }
    [SerializeField] Conditions _Conditions;
    [SerializeField] List<GameObject> Left = new List<GameObject>(); //позиция объетка справа
    [SerializeField] List<GameObject> Right = new List<GameObject>(); //позиция объетка слева
    [SerializeField] List<GameObject> GOActive = new List<GameObject>();
    [SerializeField] List<GameObject> AnimObjects = new List<GameObject>();
    [SerializeField] List<Vector3> ComparePos = new List<Vector3>();
    private int next = 0;
    private bool notdone = true;
    private bool startMove = false;
    #endregion
    #region  rotate
    [SerializeField] List<Vector3> RotationAngle = new List<Vector3>();
    private Quaternion originRotation;
    private float angle;
    private int nextr = 0;
    private bool notdoner = true;
    private bool startRot = false;
    #endregion
    private void Start()
    {
        myObj = transform.GetComponent<Renderer>();
        originRotation = transform.rotation;
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
                Reaction();
            }
            if (CheckCondition(Left, Right))
            {
                startreact = true;
            }
        }
        if (_Action == Action.Rotate)
        {
            if (startRot)
            {
                Rotation(RotationAngle);
            }
            if (notdoner && CheckCondition(Left, Right))
            {
                startRot = true;
            }
            else if (notdoner == false)
            {
                Debug.Log(notdoner);
                startRot = false;
                notdoner = true;
                nextr = 0;
            }
        }
    }
    private void Rotation(List<Vector3> Angle)
    {
        var _angle = Quaternion.Euler(Angle[nextr]);
        transform.rotation = Quaternion.Lerp(transform.rotation,_angle,Speed * Time.deltaTime);
        if (transform.rotation.eulerAngles.ToString() == Angle[nextr].ToString())
        {
            nextr++;
        }
        if (nextr == Angle.Count) notdoner = false;
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
           result = isActive(AnimObjects);
        }
        if (_Conditions == Conditions.CompareVector)
        {
            result = VectorToObj(AnimObjects, ComparePos);
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
    private bool VectorToObj(List<GameObject> gameObjects, List<Vector3> vectors)
    {
        var res = false;
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].transform.position == vectors[i])
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
    private bool isActive(List<GameObject> animobj)
    {
        var res = false;
        for (int i = 0; i < animobj.Count; i++)
        {
            if (animobj[i].activeInHierarchy)
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
        SerializedProperty AnimObjects;
        SerializedProperty ComparePos;
        #endregion
        SerializedProperty ToChangeColor;
        SerializedProperty ChangeTime;
        SerializedProperty RotationAngle;
        void OnEnable()
        {
            #region MoveParEnable
            MoveTo = serializedObject.FindProperty("MoveTo");
            _Action = serializedObject.FindProperty("_Action");
            Speed = serializedObject.FindProperty("Speed");
            _Conditions = serializedObject.FindProperty("_Conditions");
            Left = serializedObject.FindProperty("Left");
            Right = serializedObject.FindProperty("Right");
            AnimObjects = serializedObject.FindProperty("AnimObjects");
            ComparePos = serializedObject.FindProperty("ComparePos");
            #endregion

            ToChangeColor = serializedObject.FindProperty("ToChangeColor");
            ChangeTime = serializedObject.FindProperty("ChangeTime");
            RotationAngle = serializedObject.FindProperty("RotationAngle");

        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            #region _ActionEnum
            EditorGUILayout.PropertyField(_Action);
            if (_Action.enumValueIndex == 1) //Добавление в Move поле
            {
                EditorGUILayout.PropertyField(MoveTo);
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(_Conditions);
            }
            if (_Action.enumValueIndex == 2)
            {
                EditorGUILayout.PropertyField(RotationAngle);
                EditorGUILayout.PropertyField(AnimObjects);
                EditorGUILayout.PropertyField(Speed);
                EditorGUILayout.PropertyField(_Conditions);
            }
            if (_Action.enumValueIndex == 3)
            {
                EditorGUILayout.PropertyField(ToChangeColor);
                EditorGUILayout.PropertyField(ChangeTime);
                EditorGUILayout.PropertyField(_Conditions);
            }
            #endregion
            #region _ConditionEnum
            if (_Conditions.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(Left);
                EditorGUILayout.PropertyField(Right);
            }
            if (_Conditions.enumValueIndex == 2)
            {
                EditorGUILayout.PropertyField(AnimObjects);
            }
            if (_Conditions.enumValueIndex == 4)
            {
                EditorGUILayout.PropertyField(AnimObjects);
                EditorGUILayout.PropertyField(ComparePos);
            }
            #endregion
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endregion
