using System.Collections;
using FIMSpace.FTail;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimating))]
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private float timeToPerformSpinAttack = 1f;
        [SerializeField] private ParticleSystem spinTrail;
        [SerializeField] private LayerMask attackObjectsMask;
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private AudioSource spinAttackSound;

        private CharacterMovement _movement;
        private CharacterAnimating _animating;
        private TailAnimator2 _hairTailAnimator;
        private Camera _camera;

        #region Consts

        private const float HairSlitheryDefault = 1f;
        private const float HairSlitheryOnSpin = 0.4f;
        private const float HairCurlingDefault = 0.8f;
        private const float HairAngleLimitOnSpin = 40;
        private const float HairAngleLimitDefault = 181;
        private const float HairCurlingOnSpin = 0.3f;
        private const float HairCurlingOnAttack = 0.3f;
        private const int WallLayer = 6;

        #endregion

        private void Start()
        {
            _movement = GetComponent<CharacterMovement>();
            _animating = GetComponent<CharacterAnimating>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
            _camera = Camera.main;
            InputControls.OnTap += Attack;
        }
        
        public void StartDelayedSpinAttack() => StartCoroutine(DelayedSpinAttack());
        
        private void Attack(Vector2 touchPos)
        {
            var ray = _camera.ScreenPointToRay(touchPos);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, attackObjectsMask)) return;
            if (hit.collider == null) return;

            if (hit.transform.gameObject.layer == WallLayer)
            {
                _movement.isSideAttack = false;
                _hairTailAnimator.Gravity = Vector3.zero;
                _hairTailAnimator.Curling = HairCurlingOnAttack;
                attackSound.Play();
                _animating.HairSetAttack();
                _animating.SetAttack();
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
            _animating.SetSpinAttack();
            spinAttackSound.Play();
            spinTrail.Play();
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
            _animating.HairSetSpinAttack();
        }
    }
}
