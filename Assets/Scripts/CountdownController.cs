using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountdownController : MonoBehaviour
{   

    public int countdownTime = 3;
    public Text countdownText;

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        while(countdownTime > 0) 
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownText.text = "GO!";

        FindObjectOfType<GameManager>().BeginGame();

        yield return new WaitForSeconds(0.8f);

        countdownText.gameObject.SetActive(false);
    }
}
