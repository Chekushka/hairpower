using System.Collections;
using FIMSpace.FTail;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 5f;
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

        private const float TimeWhenHoldEnables = 0.2f;
        private const float HairCurlingDefault = 0.3f;
        private const float HairCurlingOnSpin = 0.1f;
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
                        Attack(touch);
                        CancelInvoke(nameof(SetHold));
                    }
                    _animation.SetIdle();
                    _wasHold = false;
                    _isMoving = false;
                    break;
            }
        }

        private void Move(Touch touch)
        {
            if (!_isMoving)
            {
                _animation.SetMoving();
                _isMoving = true;
            }

            transform.position += Vector3.forward * forwardMovementSpeed * Time.deltaTime;
        
            if(touch.deltaPosition.x > 0)
                transform.position += Vector3.right * sideMovementSpeed * Time.deltaTime;
            else if(touch.deltaPosition.x < 0)
                transform.position += Vector3.left * sideMovementSpeed * Time.deltaTime;
        }

        private void Attack(Touch touch)
        {
            var ray = _camera.ScreenPointToRay(touch.position);
   
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, attackObjectsMask)) return;
            if (hit.collider == null) return;

            if (hit.transform.gameObject.layer == WallLayer) // simple attack
            {
                _hairTailAnimator.IKTarget = hairDirectionPoint;
                _animation.Attack();
                hairDirectionPoint.position = new Vector3(hit.point.x, 2.5f,hit.point.z + 0.5f);
            }
            else if (hit.transform.gameObject.layer == GirlLayer) // spin attack
            {
                _hairTailAnimator.IKTarget = hairSpinDirectionPoint;
                _hairTailAnimator.Curling = HairCurlingOnSpin;
                _animation.SpinAttack();
            }
            
            StartCoroutine(MoveHairDirectionPointBack());
        }

        private IEnumerator MoveHairDirectionPointBack()
        {
            yield return new WaitForSeconds(1f);
            SetHairDirectionPointToDefault();
            _hairTailAnimator.IKTarget = hairDirectionPoint;
            _hairTailAnimator.Curling = HairCurlingDefault;
        }

        private void SetHold() => _wasHold = true;
        private void SetHairDirectionPointToDefault() => 
            hairDirectionPoint.position = transform.position + Vector3.back * hairDirectionPointDistance;
    }
}
