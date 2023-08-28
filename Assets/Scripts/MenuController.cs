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

    public static int scoreGoalValue;


    public void Start()
    {
        if(!PlayerPrefs.HasKey("countdown"))
        {
            PlayerPrefs.SetFloat("countdown", 3);
            Load();
        }
        else 
        {
            Load(); 
        } 
        
        /*
        countdownSlider.onValueChanged.AddListener((v) => {
            countdownSliderValue.text = v.ToString("0");
        });
        */
    }

    public void ChangeCountdown()
    {
        countdown = countdownSlider.value;
        countdownSliderValue.text = countdown.ToString();
        Save();
    }

    private void Load()
    {
        countdownSlider.value = PlayerPrefs.GetFloat("countdown");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("countdown", countdownSlider.value);
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