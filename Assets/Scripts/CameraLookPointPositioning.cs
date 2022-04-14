using Character;
using UnityEngine;

public class CameraLookPointPositioning : MonoBehaviour
{
    [SerializeField] private Transform hips;

    private CharacterMovement _characterMovement;
    private Transform _followTransform;

    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _followTransform = _characterMovement.transform;
    }

    public void EnableCameraFollowHips() => _followTransform = hips;
    public void DisableCameraFollowHips() => _followTransform = _characterMovement.transform;

    private void Update() => transform.position = 
        new Vector3(transform.position.x, transform.position.y, _followTransform.position.z);
}
