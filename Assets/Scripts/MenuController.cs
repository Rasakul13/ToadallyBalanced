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

    public int port;
    [SerializeField] private TMP_InputField portInputField;


    public void Start()
    {   
        portInputField.onValueChanged.RemoveAllListeners();
        portInputField.onValueChanged.AddListener(ChangePort);

        LoadCountdown();
        LoadTime();
        LoadDifficulty();
        LoadPort();

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

    public void ChangePort(string portString)
    {
        if (int.TryParse(portString, out int port) && port > 0 && port < 65536)
        {
            PlayerPrefs.SetInt("port", port);
            Debug.Log($"Port set to: {port}");
        }
        else
        {
            Debug.LogWarning("Invalid port number. Please enter a value between 1 and 65535.");
        }
    }

 
    private void LoadPort()
    {
        if (!PlayerPrefs.HasKey("port"))
        {
            PlayerPrefs.SetInt("port", 5555);
        }

        portInputField.text = PlayerPrefs.GetInt("port").ToString();
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

        PlayerPrefs.DeleteKey("port");
        LoadPort();
       
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