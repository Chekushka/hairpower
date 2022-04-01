using UnityEngine;

public class ShampooTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem bubbles;
    private HairGrowing _hairGrowing;

    private void Start() => _hairGrowing = FindObjectOfType<HairGrowing>();

    private void OnTriggerEnter(Collider other)
    {
        _hairGrowing.GrowHair();
        Instantiate(bubbles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
