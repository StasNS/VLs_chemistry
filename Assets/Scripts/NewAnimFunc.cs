using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class NewAnimFunc : MonoBehaviour
{
    public enum Action {None, Move, Rotate }
    public Action _Action;
    public List<Vector3> Points = new List<Vector3>();
}

#region CustomEditor
[CustomEditor(typeof(NewAnimFunc))]
public class CustomScript : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as NewAnimFunc;

        myScript._Action = (NewAnimFunc.Action)EditorGUILayout.EnumPopup(myScript._Action);

        if (myScript._Action == NewAnimFunc.Action.Move)
        {

        }
    }
}
#endregion
