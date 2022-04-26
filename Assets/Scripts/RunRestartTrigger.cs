using Character;
using UnityEngine;

public class RunRestartTrigger : MonoBehaviour
{
    [SerializeField] private float timeToRunStart;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Girl")) return;
        var movement = other.GetComponent<CharacterMovement>();
        movement.SetTimeWhenRunEnables(timeToRunStart);
        movement.RestartMovement();
    }
}
