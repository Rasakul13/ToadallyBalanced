using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    public float countdown;
    [SerializeField] Slider countdownSlider;
    [SerializeField] private TextMeshProUGUI countdownSliderValue;

    public float time;
    [SerializeField] Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timeSliderValue;

    Button easyButton;
    Button normalButton;
    Button hardButton;

    public void Start()
    {
        LoadCountdown();
        LoadTime();
        LoadDifficulty();

        SelectButton(0);
        //easyButton.Select();
        
        //EventSystem.current.SetSelectedGameObject(easyButton);
    }

    
    private void SelectButton(int controlIndex)
    {
        //if (controlIndex < _buttons.Count)
        {
            //Button button = _buttons[controlIndex];
            Button button = easyButton;
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(button.gameObject, new BaseEventData(EventSystem.current));
                Debug.Log("selected" + button.gameObject);
            }
            else 
            {
                Debug.Log("button not found");

            }
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
        if(!PlayerPrefs.HasKey("countdown"))
        {
            PlayerPrefs.SetFloat("countdown", 5);
        }
        
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
        if(!PlayerPrefs.HasKey("time"))
        {
            PlayerPrefs.SetFloat("time", 3);
        }
        
        timeSlider.value = PlayerPrefs.GetFloat("time");
        time = timeSlider.value*30;
        DisplayTime(time);
    }

    private void SaveTime()
    {
        PlayerPrefs.SetFloat("time", timeSlider.value);
    }

    private void DisplayTime(float t)
    {   
        float minutes = Mathf.FloorToInt(t/60);
        float seconds = Mathf.FloorToInt(t%60);
        timeSliderValue.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
   public void SetDifficulty(float difficulty)
    {
        SaveDifficulty(difficulty);
    }

    private void LoadDifficulty()
    {
        if(!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetFloat("difficulty", 1f);
        }
        
        //TODO highlight selected Button 1 - 2 - 3
    }

    private void SaveDifficulty(float difficulty)
    {
        PlayerPrefs.SetFloat("difficulty", difficulty);
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("countdown");
        LoadCountdown();

        PlayerPrefs.DeleteKey("time");
        LoadTime();
       
        PlayerPrefs.DeleteKey("volume");
        FindObjectOfType<AudioManager>().Load();

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