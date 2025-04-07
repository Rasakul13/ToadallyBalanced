using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{   
    private AudioManager audioManager;
    private GameManager gameManager;

    public PlayerMovement movement;

    public Animator animator;
    public GameObject player;
    
    bool disappear;
    bool invulnerable;

    void Awake()
    {   
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        invulnerable = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if(!invulnerable) 
        {
            if(collision.collider.CompareTag("Trap") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Bullet"))
            {   
                invulnerable = true;
                
                Debug.Log("Player hit " + collision.collider.name);
                
                // -1 HP 
                gameManager?.TakeDamage();

                // movement.setMovementBool(false);

                animator.Play("DisappearingAnimation", 0, 3f);

                if(player && gameManager?.gameHasEnded == false)
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

    public void EnableMovement()
    {
        movement.setMovementBool(true);
        invulnerable = false;
    }

    private IEnumerator ResetCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.transform.SetPositionAndRotation(new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));

        audioManager?.Play("PlayerSpawn");
        // EnableMovement();
    } 
}
