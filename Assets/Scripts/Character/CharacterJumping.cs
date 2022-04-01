using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterJumping : MonoBehaviour
    {
        [SerializeField] private float jumpTime;
        [SerializeField] private float jumpingSpeed;

        public bool isReadyToJump;
        private bool _isJumping;
        
        private CharacterAnimation _animation;

        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
            InputControls.OnTap += StartJump;
        }

        private void FixedUpdate()
        {
            if(_isJumping)
                transform.position += Vector3.forward * jumpingSpeed * Time.deltaTime;
        }

        public bool IsJumping() => _isJumping;

        private void StartJump(Vector2 position)
        {
            if (isReadyToJump)
            {
                _animation.SetJumping();
                _isJumping = true;
                StartCoroutine(EndJump(jumpTime));
            }
        }

        private IEnumerator EndJump(float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            isReadyToJump = false;
            _isJumping = false;
        }
    }
}
