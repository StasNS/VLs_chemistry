using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private GameObject prob;
    [SerializeField] private List<GameObject> kolbaForTube = new List<GameObject>();
    [SerializeField] private float speed;
    [SerializeField] private float blobspeed;
    [SerializeField] private List<GameObject> place_Prob = new List<GameObject>();
    [SerializeField] private List<GameObject> blob = new List<GameObject>();
    [SerializeField] private List<Vector3> blobTarget = new List<Vector3>();
    [SerializeField] private Vector3 rot_Angle;
    private Quaternion startedAngle = new Quaternion(-0.5f, -0.5f, -0.5f, 0.5f);
    private bool there = false;
    private bool back = false;
    int i = 0;
    private List<Vector3> blobPosStart;
    private void Start()
    {
        blobPosStart = new List<Vector3>() {blob[0].transform.position,blob[1].transform.position,blob[2].transform.position};
    }
    private void FixedUpdate()
    {
        if (prob.transform.position == place_Prob[0].transform.position ||
        prob.transform.position == place_Prob[1].transform.position ||
        prob.transform.position == place_Prob[2].transform.position)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.gameObject == kolbaForTube[0] ||
                    hit.transform.gameObject == kolbaForTube[1] ||
                    hit.transform.gameObject == kolbaForTube[2])
                    {
                        if (hit.transform.gameObject == kolbaForTube[0])
                        {
                            i = 0;
                        }
                        if (hit.transform.gameObject == kolbaForTube[1])
                        {
                            i = 1;
                        }
                        if (hit.transform.gameObject == kolbaForTube[2])
                        {
                            i = 2;
                        }
                        there = true;
                    }
                }
            }
        }
        if (there)
        {
            Splasing(i);
        }
        if (back)
        {
            Reverse(startedAngle);
        }
        if (blob[0].activeInHierarchy)
        {
            blob[0].transform.position = Vector3.MoveTowards(blob[0].transform.position, blobTarget[0], blobspeed * Time.deltaTime);
            if (blob[0].transform.position == blobTarget[0])
            {
                blob[0].SetActive(false);
                blob[0].transform.position = blobPosStart[0];
            }
        }
        if (blob[1].activeInHierarchy)
        {
            blob[1].transform.position = Vector3.MoveTowards(blob[1].transform.position, blobTarget[1], blobspeed * Time.deltaTime);
            if (blob[1].transform.position == blobTarget[1])
            {
                blob[1].SetActive(false);
                blob[1].transform.position = blobPosStart[1];
            }
        }
        if (blob[2].activeInHierarchy)
        {
            blob[2].transform.position = Vector3.MoveTowards(blob[2].transform.position, blobTarget[2], blobspeed * Time.deltaTime);
            if (blob[1].transform.position == blobTarget[2])
            {
                blob[2].SetActive(false);
                blob[2].transform.position = blobPosStart[2];
            }
        }
    }
    private void Splasing(int i)
    {
        var _angle = Quaternion.Euler(rot_Angle);
        prob.transform.rotation = Quaternion.Lerp(prob.transform.rotation, _angle, speed * Time.deltaTime);
        if (prob.transform.rotation.eulerAngles.ToString() == rot_Angle.ToString())
        {
            blob[i].SetActive(true);
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
