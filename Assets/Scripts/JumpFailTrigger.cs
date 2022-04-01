using Character;
using UnityEngine;

public class JumpFailTrigger : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterJumping _characterJumping;
    private const int GirlLayer = 3;

    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _characterJumping = FindObjectOfType<CharacterJumping>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlLayer) return;
        if (!_characterJumping.IsJumping())
        {
            _characterMovement.DisableAllMovement();
            _characterMovement.EnableRagDoll(true);
        }
    }
}
