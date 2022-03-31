using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator hairAnimator;
        private Animator _characterAnimator;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int SpinAttack1 = Animator.StringToHash("SpinAttack");
        private static readonly int Run = Animator.StringToHash("Run");

        private void Start()
        {
            _characterAnimator = GetComponent<Animator>();
        }

        public void SetMoving() => _characterAnimator.SetBool(IsMoving, true);
        public void SetIdle() => _characterAnimator.SetBool(IsMoving, false);
        public void SetAttack() => _characterAnimator.SetTrigger(Attack);
        public void SpinAttack() => _characterAnimator.SetTrigger(SpinAttack1);
        public void SetRunning() => _characterAnimator.SetTrigger(Run);
        public void HairSetAttack() => hairAnimator.SetTrigger(Attack);
    }
}
