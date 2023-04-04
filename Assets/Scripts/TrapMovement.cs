using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
    
    public Transform targetTransform;
    public float interpolationTime = 0f;
    public float delay = 2f;

    private bool isWaiting = false;
    public int flag = 0;

    private Vector2 sourcePosition;
    private Vector2 targetPosition;
    
    void Start()
    {   
        StartCoroutine(WaitCoroutine(2.5f));

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
                interpolationTime = 0f;
                var temp = targetPosition;
                targetPosition = sourcePosition;
                sourcePosition = temp;

                flag = 0;
            }
        }
        
    }

    private IEnumerator WaitCoroutine(float delay)
    {
        isWaiting = true;
        yield return new WaitForSeconds(delay);
        isWaiting = false;
    } 
}
