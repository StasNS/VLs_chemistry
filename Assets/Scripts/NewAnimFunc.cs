using UnityEngine;
using System.Collections.Generic;

public class NewAnimFunc : MonoBehaviour
{
    [System.FlagsAttribute]
    enum Func
    {
        Moving,
        Rotating,
    }
    [SerializeField] List<int> Doing = new List<int>();

    
}
