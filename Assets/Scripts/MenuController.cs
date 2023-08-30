using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    public float countdown;
    [SerializeField] Slider countdownSlider;
    [SerializeField] private TextMeshProUGUI countdownSliderValue;

    public float time;
    [SerializeField] Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timeSliderValue;

    public void Start()
    {
        if(!PlayerPrefs.HasKey("countdown"))
        {
            PlayerPrefs.SetFloat("countdown", 3);
            LoadCountdown();
        }
        else 
        {
            LoadCountdown(); 
        } 

        
        if(!PlayerPrefs.HasKey("time"))
        {
            PlayerPrefs.SetFloat("time", 3);
            LoadTime();
        }
        else 
        {
            LoadTime(); 
        }
    }

    public void ChangeCountdown()
    {
        countdown = countdownSlider.value;
        countdownSliderValue.text = countdown.ToString();
        SaveCountdown();
    }

    private void LoadCountdown()
    {
        countdownSlider.value = PlayerPrefs.GetFloat("countdown");
        countdown = countdownSlider.value;
        countdownSliderValue.text = countdown.ToString();
    }

    private void SaveCountdown()
    {
        PlayerPrefs.SetFloat("countdown", countdownSlider.value);
    }

    
    public void ChangeTime()
    {
        time = timeSlider.value*30;
        DisplayTime(time);
        SaveTime();
    }

    private void LoadTime()
    {
        timeSlider.value = PlayerPrefs.GetFloat("time");
        time = timeSlider.value*30;
        DisplayTime(time);
    }

    private void SaveTime()
    {
        PlayerPrefs.SetFloat("time", timeSlider.value);
    }

    void DisplayTime(float t)
    {   
        float minutes = Mathf.FloorToInt(t/60);
        float seconds = Mathf.FloorToInt(t%60);
        timeSliderValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    


    public void SelectLevel(int levelnumber)
    {
        Debug.Log("Load Level " + levelnumber.ToString());
        sceneLoader.LoadLevel(levelnumber);
    }

    public void QuitGame()
    {   
        Debug.Log("QUIT!");
        Application.Quit();
    }
}