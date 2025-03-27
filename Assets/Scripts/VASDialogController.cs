using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VASDialogController : MonoBehaviour
{

    private SpawnManager spawnManager;

    public GameObject vasDialog;
    public Button submitButton;

    private void Start()
    {   
        spawnManager = FindFirstObjectByType<SpawnManager>();

        // if (submitButton != null)
        // {
        //     submitButton.onClick.AddListener(OnSubmit);
        // }
    }

    public void ShowDialog()
    {
        if (vasDialog != null)
        {
            vasDialog.SetActive(true);
        }
    }


    private void OnSubmit()
    {
        // You can process input data here if needed before closing
        Debug.Log("VAS Dialog submitted!");


        //TODO: create a  file or append input to extisting one


        SceneManager.LoadScene(0);
    }
}
