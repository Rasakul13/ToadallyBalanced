using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Globalization;
using static System.Environment; 

public class VASDialogController : MonoBehaviour
{

    private SpawnManager spawnManager;

    public GameObject vasDialog;
    public Button submitButton;

    [SerializeField] Slider painSlider;
    [SerializeField] private TextMeshProUGUI painSliderValue;

    [SerializeField] private TMP_InputField noteInputField;

    private string filePath;
    

    private void Start()
{
    string documentsPath = GetFolderPath(SpecialFolder.MyDocuments);
    string gameFolder = Path.Combine(documentsPath, "ToadallyBalanced"); // Name the games folder
    string dataFolder = Path.Combine(gameFolder, "Data");

    Directory.CreateDirectory(dataFolder); // Ensure directory exists
    filePath = Path.Combine(dataFolder, "VAS_data.csv"); // File path inside 'Data' folder
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

        SaveData(painValue, userText); // Save to CSV


        SceneManager.LoadScene(0);
    }

    private void SaveData(float value, string text)
    {

        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture); // Use invariant format
        string newEntry = $"{value.ToString(CultureInfo.InvariantCulture)};{text};{currentDate}"; // Proper CSV formatting

        try
        {
            bool fileExists = File.Exists(filePath);

            using (StreamWriter sw = new StreamWriter(filePath, true))

            {
                if (!fileExists)
                {
                    sw.WriteLine("Pain value;Notes;Date");
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
}
