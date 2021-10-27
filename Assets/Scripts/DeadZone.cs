using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private List<GameObject> deadObj = new List<GameObject>();
    private List<Vector3> posDeadObj = new List<Vector3>();
    void Start()
    {
        deadObj.AddRange(GameObject.FindGameObjectsWithTag("Dragable"));
        for (int i = 0; i < deadObj.Count; i++)
        {
            posDeadObj.Add(deadObj[i].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < deadObj.Count; i++)
        {
            if (other.gameObject.name == deadObj[i].name)
            {
                other.transform.position = posDeadObj[i];
            }
        }
    }
}
