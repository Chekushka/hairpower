using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimating))]
    public class CharacterJumping : MonoBehaviour
    {
        [SerializeField] private float jumpTime;
        [SerializeField] private float parkourJumpTime;
        [SerializeField] private float jumpingSpeed;
        [SerializeField] private AudioSource jumpStartSound;
        [SerializeField] private AudioSource jumpEndSound;

        public bool isReadyToJump;
        private bool _isJumping;

        private CharacterMovement _movement;
        private CharacterAnimating _animating;
        private CharacterFootstepsSoundPlaying _footstepsSoundPlaying;

        private void Start()
        {
            _animating = GetComponent<CharacterAnimating>();
            _movement = GetComponent<CharacterMovement>();
            _footstepsSoundPlaying = GetComponent<CharacterFootstepsSoundPlaying>();
            InputControls.OnTap += StartJump;
        }

        private void FixedUpdate()
        {
            if(_isJumping)
                transform.position += Vector3.forward * jumpingSpeed * Time.deltaTime;
        }

        public bool IsJumping() => _isJumping;

        public void StartParkourJump()
        {
            _footstepsSoundPlaying.isWalking = false;
            _animating.SetParkourJump();
            jumpStartSound.Play();
            _movement.DisableRunning();
            StartCoroutine(SetJumping(0.3f));
            StartCoroutine(EndJump(parkourJumpTime));
        }

        private void StartJump()
        {
            if (isReadyToJump)
            {
                _footstepsSoundPlaying.isWalking = false;
                _animating.SetJumping();
                jumpStartSound.Play();
                StartCoroutine(SetJumping(0.2f));
                StartCoroutine(EndJump(jumpTime));
            }
        }

        private IEnumerator SetJumping(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isJumping = true;
        }

        private IEnumerator EndJump(float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            isReadyToJump = false;
            _isJumping = false;
            jumpEndSound.Play();
            _footstepsSoundPlaying.isWalking = true;
            _movement.RestartMovement();
        }
    }
}
