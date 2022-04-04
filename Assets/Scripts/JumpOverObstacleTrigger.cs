using Character;
using UnityEngine;

public class JumpOverObstacleTrigger : MonoBehaviour
{
    private CharacterJumping _characterJumping;
    private const int GirlLayer = 3;
    private void Start() => _characterJumping = FindObjectOfType<CharacterJumping>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlLayer) return;
        _characterJumping.StartParkourJump();
        Destroy(gameObject);
    }
}
