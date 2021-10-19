using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject activator;
    [SerializeField] Vector3 target;
    [SerializeField] GameObject result;
    private void FixedUpdate()
    {
        if(activator.transform.rotation.eulerAngles.ToString() == target.ToString())
        {
            result.SetActive(true);
        }
    }
    
}
