using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Globalization;
using static System.Environment; 

public class VASDialogController : MonoBehaviour
{

    private GameManager gameManager;

    public GameObject vasDialog;
    public Button submitButton;

    [SerializeField] Slider painSlider;

    [SerializeField] private TMP_InputField noteInputField;

    private string filePath;

    private string timePlayed;
    

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        string documentsPath = GetFolderPath(SpecialFolder.MyDocuments);
        string gameFolder = Path.Combine(documentsPath, "ToadallyTilted"); // Name the games folder
        string dataFolder = Path.Combine(gameFolder, "Data");

        Directory.CreateDirectory(dataFolder); // Ensure directory exists
        filePath = Path.Combine(dataFolder, "vas_log.csv"); // File path inside 'Data' folder
    }


    public void ShowDialog()
    {
        if (vasDialog != null)
        {
            vasDialog.SetActive(true);
        }
    }

    public void OnSubmit()
    {
        float painValue = painSlider.value;
        string userText = noteInputField != null ? noteInputField.text : "";
    
        Debug.Log($"VAS Dialog submitted! Pain Value: {painValue}, User Input: {userText}");
        
        timePlayed = CalcTimePlayed();

        bool levelCompleted = gameManager.levelCompleted;
        int level = gameManager.currentLevel;

        int difficultyValue = (int)PlayerPrefs.GetFloat("difficulty");
        string difficulty = "";

        switch(difficultyValue)
        {
            case 0:
                difficulty = "Easy";
                break;
            case 1:
                difficulty = "Normal";
                break;
            case 2:
                difficulty = "Hard";
                break;
        }

        SaveData(painValue, userText, level, difficulty, levelCompleted); // Save to CSV


        SceneManager.LoadScene(0);
    }

    private void SaveData(float value, string text, int level, string difficulty, bool levelCompleted)
    {

        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // Use invariant format
        string newEntry = $"{currentDate};{value.ToString(CultureInfo.InvariantCulture)};{text};{level};{difficulty};{levelCompleted};{timePlayed}"; // Proper CSV formatting

        try
        {
            bool fileExists = File.Exists(filePath);

            using (StreamWriter sw = new StreamWriter(filePath, true))

            {
                if (!fileExists)
                {
                    sw.WriteLine("Date;Pain value;Notes;Level;Difficulty;Completed;Time played");
                }
                sw.WriteLine(newEntry);
            }

            Debug.Log($"Data saved to {filePath}: {newEntry}");
        }
        catch (IOException e)
        {
            Debug.LogError($"File write error: {e.Message}");
        }
    }

    private string CalcTimePlayed() {
        float totalTime = PlayerPrefs.GetFloat("time") * 30;
        float remainingTime = gameManager.timer.GetRemainingTime();

        float time = totalTime - remainingTime;

        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time%60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
