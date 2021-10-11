using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject activator;
    [SerializeField] Vector3 tarhet;
    [SerializeField] GameObject result;
    private void FixedUpdate()
    {
        if(activator.transform.position == tarhet)
        {
            result.SetActive(true);
        }
    }
    
}
