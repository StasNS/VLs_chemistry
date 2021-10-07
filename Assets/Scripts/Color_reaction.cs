using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_reaction : MonoBehaviour
{
    [SerializeField] private Color ToChangeColor;
    [SerializeField] [Range(0f, 1f)] float ChangeTime;
    private Renderer myObj;
    bool ischanged;
    private Color colorChanged = new Color(0.078f, 0, 0.153f, 0);
    private void Start()
    {
        myObj = transform.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        if (GameObject.Find("Electrod1").transform.position == new Vector3(-1.772f, 1.8f, 0.233855f) &&
        GameObject.Find("Electrod2").transform.position == new Vector3(-2.022f, 1.8f, 0.233855f))
        {
            if (ischanged)
            {
                myObj.material.color = Color.Lerp(myObj.material.color, ToChangeColor, ChangeTime * Time.deltaTime);
                if(myObj.material.color.ToString() == ToChangeColor.ToString())
                {
                    ischanged = false;
                }
            }
            if (myObj.material.color.ToString() == colorChanged.ToString())
            {
                ischanged = true;
                GameObject.Find("Bubbles_particle").GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
