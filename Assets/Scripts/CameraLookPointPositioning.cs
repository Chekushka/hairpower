using Character;
using UnityEngine;

public class CameraLookPointPositioning : MonoBehaviour
{
    [SerializeField] private Transform hips;
    [SerializeField] private Vector3 offset;

    private CharacterMovement _characterMovement;
    private Transform _followTransform;
    
    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _followTransform = _characterMovement.transform;
    }

    private void Update() => transform.position = 
        new Vector3(_followTransform.position.x, transform.position.y, _followTransform.position.z) + offset;
}
