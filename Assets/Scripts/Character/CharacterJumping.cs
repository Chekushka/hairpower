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
        private bool _isOnHorizontalBar;

        private CharacterMovement _movement;
        private CharacterAnimating _animating;
        private CameraLookPointPositioning _cameraLookPoint;

        private void Start()
        {
            _animating = GetComponent<CharacterAnimating>();
            _movement = GetComponent<CharacterMovement>();
            _cameraLookPoint = FindObjectOfType<CameraLookPointPositioning>();
            InputControls.OnTap += StartJump;
        }

        private void FixedUpdate()
        {
            if(_isJumping && !_isOnHorizontalBar)
                transform.position += Vector3.forward * jumpingSpeed * Time.deltaTime;
        }

        public bool IsJumping() => _isJumping;

        public void StartParkourJump()
        {
            _animating.SetParkourJump();
            _movement.DisableRunning();
            StartCoroutine(SetJumping(0.3f));
            StartCoroutine(EndJump(parkourJumpTime));
        }

        public void StartHorizontalBarAction()
        {
            _animating.SetSwingJump();
            _isJumping = true;
            _isOnHorizontalBar = true;
            _cameraLookPoint.EnableCameraFollowHips();
            StartCoroutine(EndHorizontalBarJump(2.1f));
        }

        private void StartJump()
        {
            if (isReadyToJump)
            {
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
        }

        private IEnumerator EndHorizontalBarJump(float animationTime)
        {
            yield return new WaitForSeconds(animationTime);
            _isJumping = false;
            _isOnHorizontalBar = false;
            transform.position += Vector3.forward * 2.5f;
            _animating.SetIdleTrigger();
            _cameraLookPoint.DisableCameraFollowHips();
            jumpEndSound.Play();
        }
    }
}
