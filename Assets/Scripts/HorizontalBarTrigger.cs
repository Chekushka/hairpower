using Character;
using UnityEngine;

public class HorizontalBarTrigger : MonoBehaviour
{
    private CharacterJumping _characterJumping;
    private Collider _collider;
    private const int GirlLayer = 3;

    private void Start()
    { 
        _characterJumping = FindObjectOfType<CharacterJumping>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlLayer) return;
        _characterJumping.StartHorizontalBarAction();
        _collider.enabled = false;
    }
}
