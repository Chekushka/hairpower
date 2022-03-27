using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _characterAnimator;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int SpinAttack1 = Animator.StringToHash("SpinAttack");

        private void Start()
        {
            _characterAnimator = GetComponent<Animator>();
        }

        public void SetMoving() => _characterAnimator.SetBool(IsMoving, true);
        public void SetIdle() => _characterAnimator.SetBool(IsMoving, false);
        public void Attack() => _characterAnimator.SetTrigger(Attack1);
        public void SpinAttack() => _characterAnimator.SetTrigger(SpinAttack1);
    }
}
