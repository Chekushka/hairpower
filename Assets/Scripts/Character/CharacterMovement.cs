using FIMSpace.FTail;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimating))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 1.5f;
        [SerializeField] private float runSpeed = 2;
        [SerializeField] private float sideMovementSpeed = 3f;
        [SerializeField] private float timeWhenRunEnables = 3f;
        [SerializeField] private GameObject hairEnd;
        [SerializeField] private GameObject failWindow;

        [Header("Ragdoll")]
        [SerializeField] private Transform hips;

        [Header("Debug")] 
        [SerializeField] private bool isAbleToSpin;

        public bool isSideAttack;
        public bool isMoving;

        private InputControls _inputControls;
        private CharacterAnimating _animating;
        private CharacterAttack _attack;
        private CharacterJumping _jumping;
        private Collider _mainCollider;
        private Rigidbody _mainRigidbody;
        private TailAnimator2 _hairTailAnimator;
        private FinishHairShooting _finishHairShooting;
        private Camera _camera;

        private bool _isWaitingForRun = true;
        private bool _isRunning;
        private bool _isFinish;

        private const float PlatformBorderDistance = 1.9f;
        
        private void Start()
        {
            _animating = GetComponent<CharacterAnimating>();
            _attack = GetComponent<CharacterAttack>();
            _jumping = GetComponent<CharacterJumping>();
            _mainRigidbody = GetComponent<Rigidbody>();
            _mainCollider = GetComponent<Collider>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
            _inputControls = FindObjectOfType<InputControls>();
            _finishHairShooting = GetComponent<FinishHairShooting>();
            _camera = Camera.main;

            EnableRagDoll(false, false);

            InputControls.OnHoldForwardStarted += SetMoving;
            InputControls.OnHoldForwardEnded += EndMoving;
        }

        private void FixedUpdate()
        {
            if (isMoving && !_jumping.IsJumping())
            {
                MoveForward();
                SideMove(_inputControls.GetFingerPos());
            }
        }

        public void SetFinish()
        {
            DisableAllMovement();
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            // _animating.SetFinishDance();
            _isFinish = true;
        }

        public void DisableAllMovement()
        {
            _animating.SetIdle();
            isMoving = false;
            _isRunning = false;
        }

        public void DisableRunning() => _isRunning = false;

        public void EnableRagDoll(bool value, bool addForce)
        {
            if (value)
            {
                failWindow.SetActive(true);
                _animating.DisableCharacterAnimator();
                _hairTailAnimator.Gravity = Vector3.down * 9;
            }

            _mainRigidbody.isKinematic = !value;
            _mainCollider.enabled = !value;
            GetComponent<HairGrowing>().SetAbleValueForHairColliders(!value);
            hairEnd.SetActive(!value);
            var rigidbodies = hips.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = !value;
                if(addForce)
                    rb.AddForce(Vector3.back, ForceMode.Impulse);
            }
        }

        private void EndMoving()
        {
            if (!_jumping.isReadyToJump && !_isFinish && !_jumping.IsJumping())
            {
                if (_isRunning)
                {
                    if(isAbleToSpin) 
                        _attack.StartDelayedSpinAttack();
                    else
                        DisableAllMovement();
                }
                else
                    _attack.StartDelayedAttack();
            }
            else
            {
                if(_isFinish)
                    _finishHairShooting.StartAction();
                else
                    DisableAllMovement();
            }


            CancelInvoke(nameof(SetRun));
            _isWaitingForRun = true;
        }

        private void MoveForward()
        {
            if (_isFinish) return;
            
            if (_isWaitingForRun)
            {
                Invoke(nameof(SetRun), timeWhenRunEnables);
                _isWaitingForRun = false;
            }

            if (_isRunning)
                transform.position += Vector3.forward * runSpeed * Time.deltaTime;
            else
                transform.position += Vector3.forward * forwardMovementSpeed * Time.deltaTime;
        }

        private void SideMove(Vector2 touchPosition)
        {
            if (transform.position.x < PlatformBorderDistance && transform.position.x > -PlatformBorderDistance)
            {
                var convertedPos = new Vector3(touchPosition.x, touchPosition.y, 10f);
                var fingerPos = _camera.ScreenToWorldPoint(convertedPos) * sideMovementSpeed;
                transform.position = new Vector3(fingerPos.x, transform.position.y, transform.position.z);
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
            transform.rotation = Quaternion.identity;
            isMoving = true;
            _animating.SetMoving();
        }

        private void SetRun()
        {
            _isRunning = true;
            _animating.SetRunning();
        }
    }
}