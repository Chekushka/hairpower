using Character;
using UnityEngine;

public class CameraLookPointPositioning : MonoBehaviour
{
    private CharacterMovement _characterMovement;

    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        transform.position = _characterMovement.transform.position;
    }

    private void Update() => transform.position = 
        new Vector3(transform.position.x, transform.position.y,_characterMovement.transform.position.z);
}
