using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlace : MonoBehaviour
{
    public GameObject Standed;
    public string RenamedObj;
    private void FixedUpdate()
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;

        if (Used != null && Used == Standed && Used.name == RenamedObj)//появление ключевой позиции при претаскивании предмета
        {
            if (Used.name == Standed.name)
            {
                transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        else transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Standed)
        {
            Standed.transform.position = transform.position; //установка при триггере объекта в позицию стойки
            Standed.transform.rotation = transform.rotation; //установка положения вращения объетку 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Standed.GetComponent<Rigidbody>().isKinematic = true;
    }
}
