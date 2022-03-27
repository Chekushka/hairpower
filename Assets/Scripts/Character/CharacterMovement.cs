using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float forwardMovementSpeed = 5f;
        [SerializeField] private float sideMovementSpeed = 3f;
        [SerializeField] private Transform hairDirectionPoint;
        [SerializeField] private LayerMask attackObjectsMask;
        
        private CharacterAnimation _animation;
        private Camera _camera;
        private Vector3 _originHairDirectionPointPos;
        private bool _wasHold;
        private bool _isMoving;

        private const float TimeWhenHoldEnables = 0.2f;
        

        private void Start()
        {
            _animation = GetComponent<CharacterAnimation>();
            _camera = Camera.main;
            _originHairDirectionPointPos = hairDirectionPoint.position;
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

            _animation.Attack();
            hairDirectionPoint.position = new Vector3(hit.point.x, 2.5f,hit.point.z + 0.5f);
            StartCoroutine(MoveHairDirectionPointBack());
        }

        private IEnumerator MoveHairDirectionPointBack()
        {
            yield return new WaitForSeconds(1f);
            hairDirectionPoint.position = _originHairDirectionPointPos;
        }

        private void SetHold() => _wasHold = true;

    }
}
