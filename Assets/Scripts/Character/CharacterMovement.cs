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
        [SerializeField] private Transform hairDirectionPoint;
        [SerializeField] private Transform hairSpinDirectionPoint;
        [SerializeField] private LayerMask attackObjectsMask;

        private CharacterAnimation _animation;
        private TailAnimator2 _hairTailAnimator;
        private Camera _camera;
        private bool _wasHold;
        private bool _isMoving;
        private bool _isRunning;
        private bool _isSideAttack;

        private const float TimeWhenHoldEnables = 0.2f;
        private const float TimeWhenRunEnables = 3f;
        private const float HairCurlingDefault = 0.3f;
        private const float HairCurlingOnSpin = 0f;
        private const int GirlLayer = 3;
        private const int WallLayer = 6;


        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
            _camera = Camera.main;
            SetHairDirectionPointToDefault();
        }

        private void Update()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Invoke(nameof(SetHold), TimeWhenHoldEnables);
                    Invoke(nameof(SetRun), TimeWhenRunEnables);
                    break;
                case TouchPhase.Moved:
                    if (_wasHold)
                        Move(touch);
                    break;
                case TouchPhase.Stationary:
                    if (_wasHold)
                        Move(touch);
                    break;
                case TouchPhase.Ended:
                    if (!_wasHold)
                    {
                        CancelInvoke(nameof(SetHold));
                        Attack(touch);
                    }
                    else
                    {
                        if(_isRunning)
                            SpinAttack();
                    }

                    _animation.SetIdle();
                    _wasHold = false;
                    _isMoving = false;
                    
                    CancelInvoke(nameof(SetRun));
                    _isRunning = false;
                    break;
            }
        }
        
        public bool IsSideAttack() => _isSideAttack;

        private void Move(Touch touch)
        {
            if (!_isMoving)
            {
                _animation.SetMoving();
                _isMoving = true;
            }

            if (_isRunning)
                transform.position += Vector3.forward * runSpeed * Time.deltaTime;
            else
                transform.position += Vector3.forward * forwardMovementSpeed * Time.deltaTime;

            if (touch.deltaPosition.x > 0)
                transform.position += Vector3.right * sideMovementSpeed * Time.deltaTime;
            else if (touch.deltaPosition.x < 0)
                transform.position += Vector3.left * sideMovementSpeed * Time.deltaTime;
        }

        private void Attack(Touch touch)
        {
            var ray = _camera.ScreenPointToRay(touch.position);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, attackObjectsMask)) return;
            if (hit.collider == null) return;

            if (hit.transform.gameObject.layer == WallLayer)
            {
                _hairTailAnimator.IKTarget = hairDirectionPoint;
                _animation.Attack();
                hairDirectionPoint.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.5f);
            }

            StartCoroutine(MoveHairDirectionPointBack(1.1f));
        }

        private void SpinAttack()
        {
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
            _isSideAttack = false;
        }
        private void SetHold() => _wasHold = true;

        private void SetRun()
        {
            _isRunning = true;
            _animation.SetRunning();
        }

        private void SetHairDirectionPointToDefault() =>
            hairDirectionPoint.position = transform.position + Vector3.back * hairDirectionPointDistance;
    }
}