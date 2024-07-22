using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerContr : MonoBehaviour
{
    public Image timer_linear_image;
    float time_remaining;
    public float maxTime=30;
    // Start is called before the first frame update
    void Start()
    {
        time_remaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            timer_linear_image.fillAmount = time_remaining / maxTime;
        }
        else
        {
            Debug.Log("kaboom");
        }
    }
}
