using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPaperMove : MonoBehaviour
{
    [SerializeField] GameObject KolbaCyl1;
    [SerializeField] GameObject KolbaCyl2;
    [SerializeField] float LiqLevel;
    [SerializeField] float speedAnim;

    private bool ismoving = false;
    private void FixedUpdate()
    {
        if (ismoving)
        {
            Moving();
        }
        if (transform.gameObject.name == "WetPaper")
        {
            var _liquid1 = KolbaCyl1.transform.GetChild(0);
            var _liquid2 = KolbaCyl2.transform.GetChild(0);
            var getfiller1 = _liquid1.GetComponent<Renderer>().material.GetFloat("LiqFill");
            var getfiller2 = _liquid2.GetComponent<Renderer>().material.GetFloat("LiqFill");
            if (transform.position == GameObject.Find("PlaceWetPaper").transform.position
            && getfiller1 >= LiqLevel && getfiller2 >= LiqLevel
            && GameObject.Find("KolbaCylinder").transform.position == GameObject.Find("PlaceKolbaCylinder").transform.position
            && GameObject.Find("KolbaCylinder1").transform.position == GameObject.Find("PlaceKolbaCylinder1").transform.position)
            {
                ismoving = true;
            }
        }
    }
    private void Moving()
    {
        var target = new Vector3(transform.position.x, 1.45f, transform.position.z);
        var children = transform.GetComponentInChildren<Transform>();
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speedAnim);
        foreach (Transform child in children)
        {
            if (child.name == "Left")
            {
                child.localEulerAngles = new Vector3(60, 0, 0);
            }
            if (child.name == "Right")
            {
                child.localEulerAngles = new Vector3(-60, 0, 0);
            }
        }
    }
}
