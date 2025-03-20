using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    
    public Text timerText; 
    float timer;

    bool timerRunning;
    
    public void SetTimer(float time)
    {
        timer = time-1;
        DisplayTime(timer);
    }
    
    
    public void StartTimer()
    {
        timerRunning = true;
    }
    

    void Update() 
    {   
        if(FindFirstObjectByType<GameManager>()?.gameHasEnded == true) {
            timerRunning = false;
        }
        
        if(timerRunning) {
            DisplayTime(timer);
        }    
    }

    void FixedUpdate()
    {   
        if(FindFirstObjectByType<GameManager>()?.gameHasEnded == true) {
            timerRunning = false;
        }

        if(timerRunning) {
            if (timer > 0)
            {
                timer -= 1 * Time.deltaTime;
            }
            else {
                timer = 0f;
                FindFirstObjectByType<GameManager>()?.GameEnd(false);
            }
        } 
    }

    void DisplayTime(float time)
    {   
        time += 1;

        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time%60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
