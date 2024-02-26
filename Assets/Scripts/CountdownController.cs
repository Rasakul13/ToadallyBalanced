using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountdownController : MonoBehaviour
{   

    public int countdownTime;
    public Text countdownText;

    public void StartCountdown(int i)
    {
        countdownTime = i;
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        
        yield return new WaitForSeconds(0f);

        while(countdownTime > 0) 
        {
            
            if(countdownTime == 3) 
            {
                FindObjectOfType<AudioManager>().Play("CountDown");
            }
            
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
