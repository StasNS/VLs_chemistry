using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private GameObject prob;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject place_Prob;
    [SerializeField] private GameObject aDrop;
    [SerializeField] private Vector3 rot_Angle;
    private Quaternion cur_Angle;
    private bool start = false;
    private bool start = false;
    private void FixedUpdate()
    {
        if (prob.transform.position == place_Prob.transform.position)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //луч триггер
                RaycastHit hit; //регистрация попадания

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.gameObject == GameObject.Find("Smesitel"))
                    {
                        Debug.Log("Got");
                        cur_Angle = prob.transform.rotation;
                        start = true;
                    }
                }
            }
        }
        if (start)
        {
            Splasing(cur_Angle);
        }
    }
    private void Splasing(Quaternion back_angle)
    {
        Debug.Log("wow");
        var _angle = Quaternion.Euler(rot_Angle);
        prob.transform.rotation = Quaternion.Lerp(prob.transform.rotation, _angle, Speed * Time.deltaTime);
        if (prob.transform.rotation.eulerAngles.ToString() == rot_Angle.ToString())
        {
            
        }
    }
}
