using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private Coroutine co_HideCursor;
    
    void Awake()
    {   
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {   
        
        if(Input.GetKeyDown(KeyCode.Tab))
        {
             gameManager?.Restart();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
             gameManager?.Quit();
        }


        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            if (co_HideCursor == null)
            {
                co_HideCursor = StartCoroutine(HideCursor());
            }
        }
        else
        {
            if (co_HideCursor != null)
            {
                StopCoroutine(co_HideCursor);
                co_HideCursor = null;
                Cursor.visible = true;
            }
        }
    }

    IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(2.5f);
        Cursor.visible = false;
    }
}