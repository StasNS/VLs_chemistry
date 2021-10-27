using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViews : MonoBehaviour
{
    private GameObject Cam;
    Quaternion currot;
    private void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }
    public void TopSwitch()
    {
        Cam.transform.position = new Vector3(-1.921f, 2.70499992f, 0.640999973f);
        currot.eulerAngles = new Vector3(56.7586517f, 180, 0);
        Cam.transform.rotation = currot;
    }
    public void FrontSwitch()
    {
        Cam.transform.position = new Vector3(-1.921f, 2.7052f, 2.139f);
        currot.eulerAngles = new Vector3(19.7017536f, 180, 0);
        Cam.transform.rotation = currot;
    }
    public void SideSwitch()
    {

    }
}
