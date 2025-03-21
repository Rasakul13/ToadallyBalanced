using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    private AudioManager audioManager;
    private GameManager gameManager;

    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public Animator animator;

    public float waitTimer;
    
    void Awake()
    {   
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {   
        waitTimer = PlayerPrefs.GetFloat("countdown") + 1.5f;
        InvokeRepeating("Attack", waitTimer, 6.0f);
    }

    void Attack()
    {
        
        if(gameManager?.gameHasEnded == false)
        {
            animator.SetBool("attack", true);
            StartCoroutine(WaitCoroutine(1.0f));
        }
    }

    void ShootProjectile()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        audioManager?.Play("Shoot");

        animator.SetBool("attack", false);
    }

    private IEnumerator WaitCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootProjectile();
    } 

}
