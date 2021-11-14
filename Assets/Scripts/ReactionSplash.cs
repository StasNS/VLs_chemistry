using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSplash : MonoBehaviour
{
    [SerializeField] GameObject blob;
    [SerializeField] int timeofReact;
    [SerializeField] Vector3 blobTarget;
    [SerializeField] GameObject Kolba;
    [SerializeField] int CountofBlob;
    [SerializeField] float step;
    private bool react = false;
    private bool flag = false;
    private float iterCount = 0;
    private float timer = 0;

    private void FixedUpdate()
    {
        if (blob.transform.position.ToString() == blobTarget.ToString())
        {
            iterCount++;
            if ((iterCount - 55) == CountofBlob)
            {
                react = true;
            }
        }
        if (react)
        {
            timer += Time.deltaTime;
            if (timeofReact == Convert.ToInt32(timer))
            {
                flag = true;
            }
            if (flag)
            {
                var getfiller = Kolba.GetComponent<Renderer>().material.GetFloat("noise_vect");
                var setfiller = getfiller - step * Time.deltaTime;
                Kolba.GetComponent<Renderer>().material.SetFloat("noise_vect", setfiller);
                if(Convert.ToInt32(setfiller) == 0)
                {
                    flag = false;
                }
            }
        }
    }
}

