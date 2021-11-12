using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSplash : MonoBehaviour
{
    [SerializeField] GameObject blob;
    [SerializeField] float step;
    private bool react = false;

    private void FixedUpdate()
    {
        if (blob.transform.position.ToString() == new Vector3(-0.728f, 1.4f, 0.3089f).ToString())
        {
            react = true;
        }
        if (react)
        {
            var getfiller = GameObject.Find("Kolba_Sulfur").GetComponent<Renderer>().material.GetFloat("noise_vect");
            var setfiller = getfiller - step * Time.deltaTime;
            GameObject.Find("Kolba_Sulfur").GetComponent<Renderer>().material.SetFloat("noise_vect", setfiller);
            var getfillerdel = GameObject.Find("Sphere").GetComponent<Renderer>().material.GetFloat("noise_vect");
            var setfillerdel = getfillerdel - step * Time.deltaTime;
            GameObject.Find("Sphere").GetComponent<Renderer>().material.SetFloat("noise_vect", setfillerdel);
        }
    }
}

