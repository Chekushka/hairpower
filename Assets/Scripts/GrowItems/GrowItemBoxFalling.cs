using UnityEngine;

namespace GrowItems
{
    public class GrowItemBoxFalling : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _rigidbody;

        private const int PlatformLayer = 7;
        private void Start()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetToFall()
        {
            _collider.isTrigger = false;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(Vector3.down * 20, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.layer != PlatformLayer) return;
            _collider.isTrigger = true;
            _rigidbody.isKinematic = true;
        }
    }
}
