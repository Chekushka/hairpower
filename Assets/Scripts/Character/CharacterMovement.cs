using System;
using System.Collections;
using FIMSpace.FTail;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 1.5f;
        [SerializeField] private float runSpeed = 2;
        [SerializeField] private float sideMovementSpeed = 3f;
        [SerializeField] private float hairDirectionPointDistance = 3f;
        [SerializeField] private float timeToPerformSpinAttack = 1f;
        [SerializeField] private Transform hairDirectionPoint;
        [SerializeField] private Transform hairSpinDirectionPoint;
        [SerializeField] private LayerMask attackObjectsMask;
        
        private CharacterAnimation _animation;
        private TailAnimator2 _hairTailAnimator;
        private Camera _camera;
        private bool _isWaitingForRun = true;
        private bool _isMoving;
        private bool _isRunning;
        private bool _isSideAttack;

        private const float TimeWhenRunEnables = 3f;
        private const float HairCurlingDefault = 0.8f;
        private const float HairCurlingOnSpin = 0f;
        private const float HairCurlingOnAttack = 0.15f;
        private const float GravityMod = -9;
        private const int WallLayer = 6;
        private const float PlatformBorderDistance = 1.9f;
        
        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
            _camera = Camera.main;
            SetHairDirectionPointToDefault();

            InputControls.OnHoldForwardStarted += SetMoving;
            InputControls.OnHoldForwardEnded += RunEndMovingAction;
            InputControls.OnHoldSide += SideMove;
            InputControls.OnAttack += Attack;
        }

        private void Update()
        {
            if(_isMoving)
                MoveForward();
        }

        public bool IsSideAttack() => _isSideAttack;

        private void RunEndMovingAction()
        {
            if (_isRunning)
                StartCoroutine(DelayedSpinAttack());
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

        private void Attack(Vector2 touchPos)
        {
            var ray = _camera.ScreenPointToRay(touchPos);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, attackObjectsMask)) return;
            if (hit.collider == null) return;

            if (hit.transform.gameObject.layer == WallLayer)
            {
                _hairTailAnimator.Gravity = Vector3.zero;
                _hairTailAnimator.IKTarget = hairDirectionPoint;
                _hairTailAnimator.Curling = HairCurlingOnAttack;
                _animation.HairSetAttack();
                _animation.SetAttack();
                hairDirectionPoint.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.5f);
            }

            StartCoroutine(MoveHairDirectionPointBack(1.3f));
        }

        private void SpinAttack()
        {
            _animation.SetIdle();
            _isMoving = false;
            _hairTailAnimator.Gravity = Vector3.zero;
            _isSideAttack = true;
            _hairTailAnimator.IKTarget = hairSpinDirectionPoint;
            _hairTailAnimator.Curling = HairCurlingOnSpin;
            _animation.SpinAttack();

            StartCoroutine(MoveHairDirectionPointBack(2.2f));
        }
        
        private IEnumerator MoveHairDirectionPointBack(float delay)
        {
            yield return new WaitForSeconds(delay);
            SetHairDirectionPointToDefault();
            _hairTailAnimator.IKTarget = hairDirectionPoint;
            _hairTailAnimator.Curling = HairCurlingDefault;
            _hairTailAnimator.Gravity = new Vector3(0, GravityMod, 0);
            _isSideAttack = false;
        }

        private IEnumerator DelayedSpinAttack()
        {
            yield return new WaitForSeconds(timeToPerformSpinAttack);
            SpinAttack();
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

        private void SetHairDirectionPointToDefault() =>
            hairDirectionPoint.position = transform.position + Vector3.back * hairDirectionPointDistance;
    }
}