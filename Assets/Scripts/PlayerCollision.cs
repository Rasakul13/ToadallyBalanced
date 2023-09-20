using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{   
    public PlayerMovement movement;

    public Animator animator;
    public GameObject player;
    
    bool disappear;
    bool invulnerable;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        invulnerable = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(!invulnerable) 
        {
            if(collision.collider.CompareTag("Trap")) 
            {   
                invulnerable = true;
                
                Debug.Log("Player hit " + collision.collider.name);
                
                // -1 HP 
                FindObjectOfType<GameManager>().TakeDamage();

                movement.enabled = false;
                //movement.setMovementBool(false);
                
                animator.Play("DisappearingAnimation", 0, 3f);

                if(player && FindObjectOfType<GameManager>().gameHasEnded == false)
                {   
                    StartCoroutine(ResetCoroutine(3.0f));
                }
                else
                {
                    animator.SetBool("gameHasEnded", true);
                }
            }      
        }
         
    }

    
    private IEnumerator ResetCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));

        movement.enabled = true;
        //movement.setMovementBool(true);
        
        invulnerable = false;

        FindObjectOfType<AudioManager>().Play("PlayerSpawn");

    } 
}
