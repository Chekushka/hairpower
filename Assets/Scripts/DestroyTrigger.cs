using Enemy;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private const int EnemyLayer = 10;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != EnemyLayer)
           Destroy(other.gameObject);
        else
        {
            var parent = other.transform.parent;
            while (!parent.CompareTag("Enemy"))
                parent = parent.transform.parent;
            Debug.Log(parent.gameObject.name);
            Destroy(parent.gameObject);
        }
    }
}
