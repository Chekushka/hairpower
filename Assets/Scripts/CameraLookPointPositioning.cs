using Character;
using UnityEngine;

public class CameraLookPointPositioning : MonoBehaviour
{
    [SerializeField] private Transform hips;
    [SerializeField] private bool followHips;
    
    private CharacterMovement _characterMovement;
    private Transform _followTransform;

    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _followTransform = followHips ? hips : _characterMovement.transform;
        transform.position = _followTransform.position;
    }

    public void EnableCameraFollowHips() => followHips = true;
    public void DisableCameraFollowHips() => followHips = false;

    private void Update() => transform.position = 
        new Vector3(transform.position.x, transform.position.y, _followTransform.position.z);
}
