using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class ObstacleExplosion : MonoBehaviour
{
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private List<Rigidbody> obstaclesParts;
    [SerializeField] private ParticleSystem punchPrefab;
    [SerializeField] private ParticleSystem hitSmokePrefab;
    
    private const int GirlHairLayer = 8;
    private const float ExplodeImpulsePower = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlHairLayer) return;
        {
            obstacleObject.SetActive(false);
            triggerCollider.enabled = false;
            Instantiate(punchPrefab, other.transform.position, Quaternion.identity);
            Instantiate(hitSmokePrefab, other.transform.position, Quaternion.identity);
            Explode(other);
        }
    }

    private void Explode(Component other)
    {
        var isSideAttack = other.GetComponentInParent<CharacterMovement>().isSideAttack;
        foreach (var part in obstaclesParts)
        {
            part.gameObject.SetActive(true);
            Vector3 direction;
            if (isSideAttack)
                direction = Vector3.left;
            else
            {
                var randomValueForForward = Random.value;
                var randomValueForDirection = Random.value;
                var explosionSideDirection = randomValueForDirection >= 0.5f ? Vector3.right : Vector3.left;
                direction = 
                    randomValueForForward >= 0.5f ? Vector3.forward : Vector3.forward + explosionSideDirection;
            }
            
            part.AddForce(direction * ExplodeImpulsePower, ForceMode.Impulse);
        }
    }
}
