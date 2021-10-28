using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadZone : MonoBehaviour
{
    private List<GameObject> deadObj = new List<GameObject>();
    private List<Vector3> posDeadObj = new List<Vector3>();
    private float timer = 0.0f;
    private float waitTime = 2.0f;
    [SerializeField] private Text message;
    void Start()
    {
        deadObj.AddRange(GameObject.FindGameObjectsWithTag("Dragable"));
        for (int i = 0; i < deadObj.Count; i++)
        {
            posDeadObj.Add(deadObj[i].transform.position);
        }
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            message.text = "";
            timer = 0;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < deadObj.Count; i++)
        {
            if (other.gameObject.name == deadObj[i].name)
            {
                other.transform.position = posDeadObj[i];
                message.text = "Обращайтесь с приборами аккуратно";
            }
        }
    }
}
