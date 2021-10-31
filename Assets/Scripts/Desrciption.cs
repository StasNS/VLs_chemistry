using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Desrciption : MonoBehaviour
{
    [SerializeField] private GameObject description;
    private void OnMouseOver()
    {
        description.transform.localScale = new Vector3(-1,1,1);
        description.transform.LookAt(Camera.main.transform);
        description.SetActive(true);
    }
    private void OnMouseExit()
    {
        description.transform.localScale = new Vector3(1,1,1);
        description.SetActive(false);
    }
}
