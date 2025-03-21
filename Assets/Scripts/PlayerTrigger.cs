using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{   
    private AudioManager audioManager;
    private GameManager gameManager;
    private SpawnManager spawnManager;


    public ScoreController score;
    int value = 100;

    public Vector2 position;
    public GameObject feedback;

    void Awake()
    {   
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        spawnManager = FindFirstObjectByType<SpawnManager>();
    }

    void Start()
    {   
        feedback = GameObject.FindWithTag("Feedback");
        feedback.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {   
        
        position = GameObject.FindWithTag("Fruit").transform.position;

        if(collider.CompareTag("Fruit"))
        {
            //Debug.Log("Player collected " + collider.name);
            Destroy(collider.gameObject);
            score.Increase(value);

            //play sound for collect item
            audioManager?.Play("CollectItem"); // depricated version: FindObjectOfType<AudioManager>().Play("CollectItem");

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Fruit") && 
        score.scoreValue < gameManager?.scoreGoalValue)
        {   
            //Debug.Log("new fruit spawned");
            

            StartCoroutine(FeedbackCoroutine(position, value));

            StartCoroutine(WaitforNewSpawnCoroutine());
        }
    }
    
    IEnumerator FeedbackCoroutine(Vector2 position, int value)
    {   
        int displayTime = 1;
    	
        GameObject duplicate = Instantiate(feedback);

        duplicate.GetComponent<SpriteRenderer>().enabled = true;

        position.y += 0.5f;
        duplicate.transform.position = position;

        while(displayTime > 0) 
        {
            yield return new WaitForSeconds(1f);
            displayTime--;
        }

        Destroy(duplicate);
    }

     IEnumerator WaitforNewSpawnCoroutine()
     {
        yield return new WaitForSeconds(1f);

        if(gameManager?.gameHasEnded == false)
        {
            spawnManager?.SpawnFruit();
        }
     }
    
}
