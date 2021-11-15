using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Secundomer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    private Text timerText;
    private int delta = 0;

    private void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        StartCoroutine(TimeFlow());
    }

    IEnumerator TimeFlow()
    {
        while (true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
            }
            sec += delta;
            timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
    public void StartStop(int delta)
    {
        this.delta = delta;
    }
    public void Reset()
    {
        sec = 0;
        min = 0;
        timerText.text = "00" + " : " + "00";
    }
}
