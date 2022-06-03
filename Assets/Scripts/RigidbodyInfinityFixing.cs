using UnityEngine;

public class RigidbodyInfinityFixing : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        _rigidbody.position = FixVector(_rigidbody.position);
        _rigidbody.rotation = FixQuaternion(_rigidbody.rotation);
        _rigidbody.velocity = FixVector(_rigidbody.velocity);
        _rigidbody.angularVelocity = FixVector(_rigidbody.angularVelocity);
    }

    private Vector3 FixVector(Vector3 vector3)
    {
        var result = vector3;
        if(float.IsNaN(result.x))
            result.x = 0f;
        if(float.IsNaN(result.y))
            result.y = 0f;
        if(float.IsNaN(result.z))
            result.z = 0f;
        return result.normalized;
    }
        
    private Quaternion FixQuaternion(Quaternion quaternion)
    {
        var result = quaternion;
        if(float.IsNaN(result.x))
            result.x = 0f;
        if(float.IsNaN(result.y))
            result.y = 0f;
        if(float.IsNaN(result.z))
            result.z = 0f;
        if(float.IsNaN(result.w))
            result.w = 0f;
        return result.normalized;
    }
}
