using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public float countdown;
    [SerializeField] Slider countdownSlider;
    [SerializeField] private TextMeshProUGUI countdownSliderValue;

    public float time;
    [SerializeField] Slider timeSlider;
    [SerializeField] private TextMeshProUGUI timeSliderValue;

    public float difficulty;
    [SerializeField] TMP_Dropdown difficultyDropdown;
    
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    public void Start()
    {
        LoadCountdown();
        LoadTime();
        LoadDifficulty();

        /*
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        */
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
    

    public void ChangeDifficulty(int difficultyIndex)
    {
        difficulty = difficultyIndex;
        SaveDifficulty();
    }

    private void LoadDifficulty()
    {
        if(!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetFloat("difficulty", 1f);
        }
        
        difficultyDropdown.value = (int)PlayerPrefs.GetFloat("difficulty");
    }

    private void SaveDifficulty()
    {
        PlayerPrefs.SetFloat("difficulty", difficultyDropdown.value);
    }

    public void SetFullscreen(bool setFullscreen)
    {
        Screen.fullScreen = !setFullscreen;
    }

    /*
    public void ChangeResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    */

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey("countdown");
        LoadCountdown();

        PlayerPrefs.DeleteKey("time");
        LoadTime();
       
        PlayerPrefs.DeleteKey("soundVolume");
        FindObjectOfType<AudioManager>().LoadSoundVolume();

        PlayerPrefs.DeleteKey("musicVolume");
        FindObjectOfType<AudioManager>().LoadMusicVolume();

        PlayerPrefs.DeleteKey("difficulty");
        LoadDifficulty();
    }

    public void SelectLevel(int levelnumber)
    {
        Debug.Log("Load Level " + levelnumber.ToString());
        SceneManager.LoadScene(levelnumber);
    }

    public void QuitGame()
    {   
        Debug.Log("QUIT!");
        Application.Quit();
    }
}