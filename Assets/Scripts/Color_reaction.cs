using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_reaction : MonoBehaviour
{
    [SerializeField] private Color ToChangeColor;
    [SerializeField] [Range(0f, 1f)] float ChangeTime;
    private Renderer myObj;
    private void Start()
    {
        myObj = transform.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        myObj.material.color = Color.Lerp(myObj.material.color,ToChangeColor, ChangeTime*Time.deltaTime);

    }
}
