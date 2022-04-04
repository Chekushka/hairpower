using UnityEngine;

namespace Bandit
{
    public class BanditAnimating : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Move = Animator.StringToHash("Move");
        private void Start() => _animator = GetComponent<Animator>();

        public void SetMoving() => _animator.SetTrigger(Move);
        public void SetAttack() => _animator.SetTrigger(Attack);
        public void DisableAnimator() => _animator.enabled = false;
    }
}
