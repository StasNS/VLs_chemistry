using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private GameObject prob;
    [SerializeField] private float speed;
    [SerializeField] private float blobspeed;
    [SerializeField] private GameObject place_Prob;
    [SerializeField] private GameObject blob;
    [SerializeField] private Vector3 rot_Angle;
    private Quaternion startedAngle = new Quaternion(-0.5f,-0.5f,-0.5f,0.5f);
    private bool there = false;
    private bool back = false;
    private void FixedUpdate()
    {
        if (prob.transform.position == place_Prob.transform.position)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.gameObject == GameObject.Find("Smesitel"))
                    {
                        there = true;
                    }
                }
            }
        }
        if (there)
        {
            Splasing();
        }
        if (back)
        {
            Reverse(startedAngle);
        }
        if(blob.activeInHierarchy)
        {
            blob.transform.position = Vector3.MoveTowards(blob.transform.position, new Vector3(-1.65310001f,1.40339994f,0.268000007f), blobspeed * Time.deltaTime);
            if(blob.transform.position == new Vector3(-1.65310001f,1.40339994f,0.268000007f))
            {
                blob.SetActive(false);
                blob.transform.position = new Vector3(-1.65310001f,1.71780002f,0.268000007f);
            }
        }
    }
    private void Splasing()
    {
        var _angle = Quaternion.Euler(rot_Angle);
        prob.transform.rotation = Quaternion.Lerp(prob.transform.rotation, _angle, speed * Time.deltaTime);
        if (prob.transform.rotation.eulerAngles.ToString() == rot_Angle.ToString())
        {
            blob.SetActive(true);
            back = true;
            there = false;
        }
    }
    private void Reverse(Quaternion back_Angle)
    {
        prob.transform.rotation = Quaternion.Lerp(prob.transform.rotation, back_Angle, speed * Time.deltaTime);
        if (prob.transform.rotation.eulerAngles.ToString() == back_Angle.eulerAngles.ToString())
        {
            back = false;
        }
    }
}
