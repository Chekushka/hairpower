using Character;
using UnityEngine;

public class ShampooTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem bubbles;
    private HairGrowing _hairGrowing;
    private Collider _collider;

    private void Start()
    {
        _hairGrowing = FindObjectOfType<HairGrowing>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        _hairGrowing.GrowHair();
        Instantiate(bubbles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
