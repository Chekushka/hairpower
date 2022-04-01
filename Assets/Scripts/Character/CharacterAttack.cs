using System.Collections;
using FIMSpace.FTail;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private float timeToPerformSpinAttack = 1f;
        [SerializeField] private LayerMask attackObjectsMask;
        
        private CharacterMovement _movement;
        private CharacterAnimation _animation;
        private TailAnimator2 _hairTailAnimator;
        private Camera _camera;

        #region Consts

        private const float HairSlitheryDefault = 1f;
        private const float HairSlitheryOnSpin = 0.1f;
        private const float HairCurlingDefault = 0.8f;
        private const float HairAngleLimitOnSpin = 20;
        private const float HairAngleLimitDefault = 181;
        private const float HairCurlingOnSpin = 0f;
        private const float HairCurlingOnAttack = 0.15f;
        private const int WallLayer = 6;

        #endregion

        private void Start()
        {
            _movement = GetComponent<CharacterMovement>();
            _animation = GetComponent<CharacterAnimation>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
            _camera = Camera.main;
            InputControls.OnTap += Tap;
        }
        
        public void StartDelayedSpinAttack() => StartCoroutine(DelayedSpinAttack());
        
        private void Tap(Vector2 touchPos)
        {
            var ray = _camera.ScreenPointToRay(touchPos);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, attackObjectsMask)) return;
            if (hit.collider == null) return;

            if (hit.transform.gameObject.layer == WallLayer)
            {
                _hairTailAnimator.Gravity = Vector3.zero;
                _hairTailAnimator.Curling = HairCurlingOnAttack;
                _animation.HairSetAttack();
                _animation.SetAttack();
            }

            StartCoroutine(MoveHairDirectionPointBack(1.3f));
        }

        private void SpinAttack()
        {
            _movement.DisableAllMovement();
            _movement.isSideAttack = true;
            _hairTailAnimator.Curling = HairCurlingOnSpin;
            _hairTailAnimator.Slithery = HairSlitheryOnSpin;
            _hairTailAnimator.AngleLimit = HairAngleLimitOnSpin;
            _animation.SetSpinAttack();
            StartCoroutine(DelayedHairSpin());

            StartCoroutine(MoveHairDirectionPointBack(2.2f));
        }
        
        private IEnumerator MoveHairDirectionPointBack(float delay)
        {
            yield return new WaitForSeconds(delay);
            _hairTailAnimator.Curling = HairCurlingDefault;
            _hairTailAnimator.Slithery = HairSlitheryDefault;
            _hairTailAnimator.AngleLimit = HairAngleLimitDefault;
            _movement.isSideAttack = false;
        }
        
        private IEnumerator DelayedSpinAttack()
        {
            yield return new WaitForSeconds(timeToPerformSpinAttack);
            SpinAttack();
        }

        private IEnumerator DelayedHairSpin()
        {
            yield return new WaitForSeconds(1f);
            _animation.HairSetSpinAttack();
        }
    }
}
