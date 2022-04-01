using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 1.5f;
        [SerializeField] private float runSpeed = 2;
        [SerializeField] private float sideMovementSpeed = 3f;

        [Header("Ragdoll")]
        [SerializeField] private Transform hips;
        
        public bool isSideAttack;

        private CharacterAnimation _animation;
        private CharacterAttack _attack;
        private CharacterJumping _jumping;
        private Collider _mainCollider;
        private Rigidbody _mainRigidbody;

        private bool _isWaitingForRun = true;
        private bool _isMoving;
        private bool _isRunning;

        private const float TimeWhenRunEnables = 3f;
        private const float PlatformBorderDistance = 1.9f;
        
        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
            _attack = GetComponent<CharacterAttack>();
            _jumping = GetComponent<CharacterJumping>();
            _mainRigidbody = GetComponent<Rigidbody>();
            _mainCollider = GetComponent<Collider>();
            
            EnableRagDoll(false);

            InputControls.OnHoldForwardStarted += SetMoving;
            InputControls.OnHoldForwardEnded += RunEndMovingAction;
            InputControls.OnHoldSide += SideMove;
        }

        private void FixedUpdate()
        {
            if(_isMoving)
                MoveForward();
        }

        public void DisableAllMovement()
        {
            _animation.SetIdle();
            _isMoving = false;
            _isRunning = false;
        }

        public void EnableRagDoll(bool value)
        {
            if(value)
                _animation.DisableCharacterAnimator();
            
            _mainRigidbody.isKinematic = !value;
            _mainCollider.enabled = !value;
            
            var rigidbodies = hips.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
                rb.isKinematic = !value;
        }

        private void RunEndMovingAction()
        {
            if (_isRunning && !_jumping.isReadyToJump)
                _attack.StartDelayedSpinAttack();
            else
            {
                _animation.SetIdle();
                _isMoving = false;
            }
            CancelInvoke(nameof(SetRun));
            _isWaitingForRun = true;
        }

        private void MoveForward()
        {
            if (_isWaitingForRun)
            {
                Invoke(nameof(SetRun), TimeWhenRunEnables);
                _isWaitingForRun = false;
            }

            if (_isRunning)
                transform.position += Vector3.forward * runSpeed * Time.deltaTime;
            else
                transform.position += Vector3.forward * forwardMovementSpeed * Time.deltaTime;
        }

        private void SideMove(Vector2 touchPositionDelta)
        {
            if (transform.position.x < PlatformBorderDistance && transform.position.x > -PlatformBorderDistance)
            {
                if (touchPositionDelta.x > 0)
                    transform.position += Vector3.right * sideMovementSpeed * Time.deltaTime;
                else if (touchPositionDelta.x < 0)
                    transform.position += Vector3.left * sideMovementSpeed * Time.deltaTime;
            }
            else
            {
                float lastAvailablePosX = 0;
                if (transform.position.x >= PlatformBorderDistance)
                    lastAvailablePosX = PlatformBorderDistance - 0.01f;
                if(transform.position.x <= -PlatformBorderDistance)
                    lastAvailablePosX = -PlatformBorderDistance + 0.01f;
                
                transform.position = new Vector3(lastAvailablePosX, transform.position.y, transform.position.z);
            }
        }
        
        private void SetMoving()
        {
            _isMoving = true;
            _animation.SetMoving();
        }

        private void SetRun()
        {
            _isRunning = true;
            _animation.SetRunning();
        }
    }
}