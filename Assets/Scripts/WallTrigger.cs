using RayFire;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem wallPunchPrefab;
    [SerializeField] private ParticleSystem hitSmokePrefab;
    private RayfireRigid _rayfireRigid;
    private RayfireBomb _rayfireBomb;
    private const int GirlHairLayer = 8;

    private void Start()
    {
        _rayfireRigid = GetComponentInChildren<RayfireRigid>();
        _rayfireBomb = GetComponentInChildren<RayfireBomb>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlHairLayer) return;
        {
            Instantiate(wallPunchPrefab, other.transform.position, Quaternion.identity);
            Instantiate(hitSmokePrefab, other.transform.position, Quaternion.identity);
            _rayfireRigid.Demolish();
            _rayfireBomb.Explode(0f);
        }
    }
}
