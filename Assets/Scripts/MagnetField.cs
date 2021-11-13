using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField : MonoBehaviour
{
    private List<GameObject> now = new List<GameObject>();
    private int magnetinField = 0;
    private int summTesla = 0;
    private int tesla = 0;
    private void OnTriggerEnter(Collider other)
    {
        now.Add(other.gameObject);
        magnetinField++;
        SummCount();
    }
    private void OnTriggerExit(Collider other)
    {
        now.Remove(other.gameObject);
        magnetinField--;
        SummCount();
    }
    private void SummCount()
    {
        summTesla = 0;
        foreach (var i in now)
        {
            tesla = i.gameObject.GetComponent<TeslaValue>().Telsa;
            summTesla += tesla;
        }
    }
}
