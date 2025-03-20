using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public UdpSocket client;

    public Rigidbody2D rb;
    public Animator animator;

    public float moveSpeed = 5f;

    Vector2 movement;

    string input;

    float x_Axis = 0;
    float y_Axis = 0;

    bool isEnabled = true;

    void Update()
    {
        if(client.socketOpen)
        {
            input = client.ReceiveData();

            if(!String.IsNullOrEmpty(input)) {
                print(input);
            }


            if(!String.IsNullOrEmpty(input) && input.Contains(" 83,")) 
            {   
                // use gravitysensor data (id 83)
                int start = input.IndexOf(" 83,");
                input = input.Substring(start);
                
                print(input);

                var inputValues = input.Split(","[0]);

                // [ID, X, Y, Z]
                if(inputValues.Length == 4) {
                    x_Axis = float.Parse(inputValues[1], CultureInfo.InvariantCulture);
                    y_Axis = float.Parse(inputValues[2], CultureInfo.InvariantCulture);
                }

                if(Mathf.Abs(x_Axis) > 0.4f) // evt 0.3?
                {   
                    var x = -x_Axis/2f;
                    movement.x = x;
                }
                else 
                {
                    movement.x = 0;
                }

                
                if(Mathf.Abs(y_Axis) > 0.25f) // evt 0.2?
                {   
                    var y = -y_Axis/2f;
                    
                    if(y < 0) 
                    {
                        y = y/2;
                    }
                    movement.y = y;
                }
                else 
                {
                    movement.y = 0;
                }
                
                
            }
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    
    }

    void FixedUpdate() 
    {   

        //while(enabled==true) {
            // Movement
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //}
    }

    public void setMovementBool(bool boolean) {
        isEnabled = boolean;
    }
}