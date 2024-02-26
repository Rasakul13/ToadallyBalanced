using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoController : MonoBehaviour
{
    
    public Animator animator;
    
    public Transform targetTransform;
    private Vector2 sourcePosition;
    private Vector2 targetPosition;

    private bool isFlipped = false;

    public float interpolationTime = 0f;
    public float delay = 1.5f;

    public int flag = 0;
    private bool isWaiting = false;

    public float waitTimer;

    void Start()
    {   
        waitTimer = PlayerPrefs.GetFloat("countdown") + 1f;
        StartCoroutine(WaitCoroutine(waitTimer));

        sourcePosition = transform.position;
        targetPosition = targetTransform.position;
    }

    void Update()
    {   
        if(!isWaiting) 
        {
            interpolationTime += (Time.deltaTime/delay);

            if(transform.position.x == targetPosition.x)
            {   
                flag = 1;
            }

            if(flag == 0) 
            {
                var newPosition = Vector2.Lerp(sourcePosition, targetPosition, Mathf.SmoothStep(0, 1, interpolationTime / delay));
                transform.position = newPosition;
            }
            else if (flag == 1) 
            {   

                StartCoroutine(WaitCoroutine(2f));

                interpolationTime = 0f;
                targetTransform.position = CalculateTargetPosition(transform.position);

                sourcePosition = transform.position;
                targetPosition = targetTransform.position;

                if(targetPosition.x < sourcePosition.x && isFlipped) 
                {
                    gameObject.transform.Rotate(0f, 180f, 0f);
                    isFlipped = false;
                }
                else if(targetPosition.x > sourcePosition.x && !isFlipped) 
                {
                    gameObject.transform.Rotate(0f, 180f, 0f);
                    isFlipped = true;
                }

                flag = 0;
            }
        }

        animator.SetFloat("Speed", interpolationTime);
    }

    private IEnumerator WaitCoroutine(float delay)
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        isWaiting = false;
    } 

    Vector2 CalculateTargetPosition(Vector2 currentPosition)
    {   

        Vector2 newPosition = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-3.6f, 3.2f));
        float distance = Vector2.Distance(currentPosition, newPosition);

        while(distance < 7.5) 
        {
            newPosition = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-3.6f, 3.2f));
            distance = Vector2.Distance(currentPosition, newPosition);
        }

        return newPosition; 
    }

}
