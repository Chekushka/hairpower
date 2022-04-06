using UnityEngine;

namespace Character
{
    public class CharacterAnimating : MonoBehaviour
    {
        [SerializeField] private Animator hairAnimator;
        private Animator _characterAnimator;
        
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int SpinAttack = Animator.StringToHash("SpinAttack");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int ParkourJump = Animator.StringToHash("ParkourJump");
        private static readonly int Finish = Animator.StringToHash("Finish");

        private const string UpperBodyLayerName = "UpperBody";

        private void Start()
        {
            _characterAnimator = GetComponent<Animator>();
        }

        public void SetMoving() => _characterAnimator.SetBool(IsMoving, true);
        public void SetIdle() => _characterAnimator.SetBool(IsMoving, false);
        public void SetAttack() => _characterAnimator.SetTrigger(Attack);
        public void SetSpinAttack() => _characterAnimator.SetTrigger(SpinAttack);
        public void SetRunning() => _characterAnimator.SetTrigger(Run);
        public void SetJumping() => _characterAnimator.SetTrigger(Jump);
        public void SetParkourJump() => _characterAnimator.SetTrigger(ParkourJump);
        public void SetFinishDance() => _characterAnimator.SetTrigger(Finish);
        public void HairSetAttack() => hairAnimator.SetTrigger(Attack);
        public void HairSetSpinAttack() => hairAnimator.SetTrigger(SpinAttack);

        public void EnableAttackLayer() => 
            hairAnimator.SetLayerWeight(1, 0);
        public void DisableAttackLayer() => 
            hairAnimator.SetLayerWeight(1, 90);

        public void DisableCharacterAnimator() => _characterAnimator.enabled = false;
    }
}
