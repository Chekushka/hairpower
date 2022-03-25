using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float forwardMovementSpeed = 5f;
    [SerializeField] private float sideMovementSpeed = 3f;

    private void Update()
    {
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:  
                Move(touch);
                break;
            case TouchPhase.Moved:
                Move(touch);
                break;
            case TouchPhase.Stationary:
                Move(touch);
                break;
            case TouchPhase.Ended:
                break;
        }
    }

    private void Move(Touch touch)
    {
        transform.position += Vector3.forward * forwardMovementSpeed * Time.deltaTime;
        
        if(touch.deltaPosition.x > 0)
            transform.position += Vector3.right * sideMovementSpeed * Time.deltaTime;
        else if(touch.deltaPosition.x < 0)
            transform.position += Vector3.left * sideMovementSpeed * Time.deltaTime;
    }
}
