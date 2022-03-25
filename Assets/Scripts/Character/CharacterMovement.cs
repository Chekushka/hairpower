using System;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 5f;
        [SerializeField] private float sideMovementSpeed = 3f;

        private CharacterAnimation _animation;

        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:  
                    Move(touch);
                    _animation.SetMoving();
                    break;
                case TouchPhase.Moved:
                    Move(touch);
                    break;
                case TouchPhase.Stationary:
                    Move(touch);
                    break;
                case TouchPhase.Ended:
                    _animation.SetIdle();
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
}
