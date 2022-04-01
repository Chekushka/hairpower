using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShampooRotating : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;

    private void Start()
    {
        var randomRotationValue = Random.Range(0, 360);
        transform.Rotate(Vector3.forward * randomRotationValue, Space.Self);
    }

    private void Update() => transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
}
