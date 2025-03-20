using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public Animator animator;

    public float waitTimer;
    
    void Start()
    {   
        waitTimer = PlayerPrefs.GetFloat("countdown") + 1.5f;
        InvokeRepeating("Attack", waitTimer, 6.0f);
    }


    void Attack()
    {
        
        if(FindFirstObjectByType<GameManager>()?.gameHasEnded == false)
        {
            animator.SetBool("attack", true);
            StartCoroutine(WaitCoroutine(1.0f));
        }
    }

    void ShootProjectile()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); 
        FindFirstObjectByType<AudioManager>()?.Play("Shoot");

        animator.SetBool("attack", false);
    }

    private IEnumerator WaitCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootProjectile();
    } 

}
