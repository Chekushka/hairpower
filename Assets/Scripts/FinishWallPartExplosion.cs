using UnityEngine;

public class FinishWallPartExplosion : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("HairBall")) return;
        _rigidbody.AddForce(Vector3.forward * 15, ForceMode.Impulse);
    }
}
