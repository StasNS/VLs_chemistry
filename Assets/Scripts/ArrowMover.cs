using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    [SerializeField] GameObject KolbaCyl1;
    [SerializeField] GameObject KolbaCyl2;
    [SerializeField] float LiqLevel;
    [SerializeField] float speedArrow;
    private Quaternion startedrotation;
    private void Start()
    {
        startedrotation = transform.rotation;
    }
    private void FixedUpdate()
    {
        var _liquid1 = KolbaCyl1.transform.GetChild(0);
        var _liquid2 = KolbaCyl2.transform.GetChild(0);
        var getfiller1 = _liquid1.GetComponent<Renderer>().material.GetFloat("LiqFill");
        var getfiller2 = _liquid2.GetComponent<Renderer>().material.GetFloat("LiqFill");
        if (GameObject.Find("KolbaCylinder").transform.position == GameObject.Find("PlaceKolbaCylinder").transform.position &&
            GameObject.Find("KolbaCylinder1").transform.position == GameObject.Find("PlaceKolbaCylinder1").transform.position &&
            getfiller1 >= LiqLevel && getfiller2 >= LiqLevel &&
            GameObject.Find("Electrod1").transform.position == new Vector3(-1.30250013f,1.47099996f,0.143700004f) &&
            GameObject.Find("Electrod2").transform.position == new Vector3(-1.51900005f,1.47099996f,0.143700004f) &&
            GameObject.Find("WetPaper").transform.position == new Vector3(-1.42570019f,1.45000005f,0.164799988f))
        {
            Quaternion targetArrow = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 112);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetArrow, speedArrow * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startedrotation, speedArrow * Time.deltaTime);
        }
    }
}

