using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSphere : MonoBehaviour
{
    public List<GameObject> Сontainer = new List<GameObject>(); //список объектов куда наливается
    public Vector3 BackPoint; //уровень возврата объекта в нужное положение 
    public GameObject ParticleWaterObj; // вода внутри колб и откуда наливается
    public float speed; //скорость наклона колбы
    public float speedLiq; // скорость жидкости
    private bool isCorrectContainer; //правильность наполняемого сосуда
    private GameObject _triggerredObj; //правильно задетый объект

    private void OnTriggerEnter(Collider other)
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (other.gameObject == Сontainer[i] && Used == transform.gameObject)
            {
                _triggerredObj = other.gameObject;
                isCorrectContainer = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        float angleY = Mathf.Round(transform.rotation.eulerAngles.y);
        float angleX = Mathf.Round(transform.rotation.eulerAngles.x);

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (isCorrectContainer)
            {
                Vector3 direction = Сontainer[i].transform.position - transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);

                if (angleY >= 0 && angleY < 60 || angleX >= 0 && angleX < 60)
                {
                    GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Play(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var Used = GameObject.Find("Main Camera").GetComponent<NewDragNDrop>().Selected;

        for (int i = 0; i < Сontainer.Count; i++)
        {
            if (other.gameObject == Сontainer[i] && Used == transform.gameObject)
            {
                transform.rotation = Quaternion.Euler(BackPoint); //временно замена нормальному методу
                GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().Stop(true);
                isCorrectContainer = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameObject.Find(ParticleWaterObj.name).GetComponent<ParticleSystem>().isPlaying)
        {
            WaterAnim(_triggerredObj.transform);
        }
    }
    private void WaterAnim(Transform _TriggeredObj)
    {
        var _liquid = _TriggeredObj.GetChild(0);
        var getfiller = _liquid.GetComponent<Renderer>().material.GetFloat("LiqFill");
        var setfiller = getfiller + speedLiq * Time.deltaTime;

        if (_TriggeredObj.childCount > 1)
        {
            var _liquid1 = _TriggeredObj.GetChild(1);
            var _liquid2 = _TriggeredObj.GetChild(2);
            var getfiller1 = _liquid1.GetComponent<Renderer>().material.GetFloat("LiqFill");
            var getfiller2 = _liquid2.GetComponent<Renderer>().material.GetFloat("LiqFill");
            var setfiller1 = getfiller1 + (speedLiq + 0.06f) * Time.deltaTime;
            var setfiller2 = getfiller2 + (speedLiq + 0.06f) * Time.deltaTime;
            if (getfiller >= 0.4f)
            {
                _liquid1.GetComponent<Renderer>().material.SetFloat("LiqFill", setfiller1);
                _liquid2.GetComponent<Renderer>().material.SetFloat("LiqFill", setfiller2);
            }
        }
        _liquid.GetComponent<Renderer>().material.SetFloat("LiqFill", setfiller);
    }
}