using System.Collections;
using FIMSpace.FTail;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimating))]
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private float spinAttackDelay;
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private MMF_Player spinFeedback;

        private CharacterMovement _movement;
        private CharacterAnimating _animating;
        private HairGrowing _growing;
        private TailAnimator2 _hairTailAnimator;

        #region Consts

        private const float HairSlitheryDefault = 1f;
        private const float HairSlitheryOnSpin = 0.4f;
        private const float HairCurlingDefault = 0.8f;
        private const float HairAngleLimitOnSpin = 40;
        private const float HairAngleLimitDefault = 181;
        private const float HairCurlingOnSpin = 0.3f;
        private const float HairCurlingOnAttack = 0.3f;

        #endregion

        private void Start()
        {
            _movement = GetComponent<CharacterMovement>();
            _animating = GetComponent<CharacterAnimating>();
            _growing = GetComponent<HairGrowing>();
            _hairTailAnimator = GetComponentInChildren<TailAnimator2>();
        }

        public void StartDelayedAttack() => StartCoroutine(DelayedAttack());
        public void StartDelayedSpinAttack() => StartCoroutine(DelayedSpinAttack());

        private void Attack()
        {
            _movement.isSideAttack = false;
            _growing.SetAbleValueForHairColliders(false);
            _hairTailAnimator.Gravity = Vector3.zero;
            _hairTailAnimator.Curling = HairCurlingOnAttack;
            attackSound.Play();
            _animating.SetAttack();

            StartCoroutine(DelayedHairAttack());
            StartCoroutine(EndAttack(1.5f));
        }

        private void SpinAttack()
        {
            _movement.isSideAttack = true;
            _hairTailAnimator.Curling = HairCurlingOnSpin;
            _hairTailAnimator.Slithery = HairSlitheryOnSpin;
            _hairTailAnimator.AngleLimit = HairAngleLimitOnSpin;
            StartCoroutine(DelayedHairSpin());

            StartCoroutine(EndAttack(1.5f));
        }

        private IEnumerator EndAttack(float delay)
        {
            yield return new WaitForSeconds(delay);
            _movement.DisableAllMovement();
            _hairTailAnimator.Curling = HairCurlingDefault;
            _hairTailAnimator.Slithery = HairSlitheryDefault;
            _hairTailAnimator.AngleLimit = HairAngleLimitDefault;
            _movement.isSideAttack = false;
        }

        private IEnumerator DelayedAttack()
        {
            yield return new WaitForSeconds(attackDelay);
            Attack();
        }

        private IEnumerator DelayedSpinAttack()
        {
            yield return new WaitForSeconds(spinAttackDelay);
            SpinAttack();
        }

        private IEnumerator DelayedHairSpin()
        {
            yield return new WaitForSeconds(0.2f);
            _animating.SetSpinAttack();
            spinFeedback.PlayFeedbacks();
            _animating.HairSetSpinAttack();
        }

        private IEnumerator DelayedHairAttack()
        {
            yield return new WaitForSeconds(0.4f);
            _growing.SetAbleValueForHairColliders(true);
            _animating.HairSetAttack();
        }
    }
}