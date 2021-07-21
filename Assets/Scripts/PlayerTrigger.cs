using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{   
    public ScoreController score;

    void OnTriggerEnter2D(Collider2D collider)
    {   

        if(collider.CompareTag("Fruit"))
        {
            //Debug.Log("Player collected " + collider.name);
            Destroy(collider.gameObject);
            score.Increase(100);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        
        if(collider.CompareTag("Fruit") && 
        score.scoreValue < FindObjectOfType<GameManager>().scoreGoalValue)
        {

            //Debug.Log("new fruit spawned");
            FindObjectOfType<SpawnManager>().SpawnFruit();
        }     
    }
}
