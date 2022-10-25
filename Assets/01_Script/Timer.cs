using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;
    public bool ExtraTime = false;

    public TMP_Text TimerTxt;
   
    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if(TimerOn)
        {
            StartCoroutine(CheckTimer());
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator CheckTimer()
    {
        if(TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            updateTimer(TimeLeft);
        }
        else
        {
            if (!ExtraTime)
            {
                TimeLeft = 0;
                yield return new WaitForSeconds(1f);
                TimerOn = false;
                GameController.Instance.SetGameStage(GameStage.Fail); 
            }
            else
            {
                TimeLeft = 30;
            }

        }
    }

}