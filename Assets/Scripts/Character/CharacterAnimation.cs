using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _characterAnimator;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Start()
        {
            _characterAnimator = GetComponent<Animator>();
        }

        public void SetMoving() => _characterAnimator.SetBool(IsMoving, true);
        public void SetIdle() => _characterAnimator.SetBool(IsMoving, false);
    }
}
