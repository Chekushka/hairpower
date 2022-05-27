using Character;
using UnityEngine;

namespace GrowItems
{
    public class GrowItemTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem onGrowParticles;
        [SerializeField] private float chargedAttackPoints;
        private HairGrowing _hairGrowing;
        private ChargedAttackBarFill _attackBar;
        private Collider _collider;

        private const int GirlLayer = 3;

        private void Start()
        {
            _hairGrowing = FindObjectOfType<HairGrowing>();
            _attackBar = FindObjectOfType<ChargedAttackBarFill>();
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != GirlLayer) return;
            
            _collider.enabled = false;
            _hairGrowing.GrowHair();
            Instantiate(onGrowParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _attackBar.AddBarPoints(chargedAttackPoints);
        }
    }
}
