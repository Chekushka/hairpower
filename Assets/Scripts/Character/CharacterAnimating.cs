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
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int ParkourJump = Animator.StringToHash("ParkourJump");
        private static readonly int Finish = Animator.StringToHash("Finish");
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        private static readonly int FinalHit = Animator.StringToHash("FinalHit");
        private static readonly int FinishIdle = Animator.StringToHash("FinishIdle");

        private void Start() => _characterAnimator = GetComponent<Animator>();

        public void SetMoving()
        {
            _characterAnimator.SetBool(IsMoving, true);
            _characterAnimator.SetBool(IsIdle, false);
        }
        public void SetIdle() 
        {
            _characterAnimator.SetBool(IsMoving, false);
            _characterAnimator.SetBool(IsIdle, true);
        }
        public void SetAttack() => _characterAnimator.SetTrigger(Attack);
        public void SetSpinAttack() => _characterAnimator.SetTrigger(SpinAttack);
        public void SetRunning(bool value) => _characterAnimator.SetBool(IsRunning, value);
        public void SetJumping() => _characterAnimator.SetTrigger(Jump);
        public void SetParkourJump() => _characterAnimator.SetTrigger(ParkourJump);
        public void SetFinishDance() => _characterAnimator.SetTrigger(Finish);
        public void HairSetAttack() => hairAnimator.SetTrigger(Attack);
        public void HairSetSpinAttack() => hairAnimator.SetTrigger(SpinAttack);
        public void SetFinalHit() => _characterAnimator.SetTrigger(FinalHit);
        public void SetFinishIdle() => _characterAnimator.SetTrigger(FinishIdle);

        public void DisableCharacterAnimator() => _characterAnimator.enabled = false;
    }
}
