using Enemy;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private const int EnemyLayer = 10;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject.layer == EnemyLayer
            ? other.GetComponentInParent<EnemyMovement>().gameObject
            : other.gameObject);
    }
}
