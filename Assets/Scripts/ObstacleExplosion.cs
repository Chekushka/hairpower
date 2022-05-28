using System.Collections.Generic;
using Character;
using GrowItems;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleExplosion : MonoBehaviour
{
    [SerializeField] private ObstacleType type;
    [SerializeField] private GameObject obstacleObject;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private GameObject explosionWave;
    [SerializeField] private List<Rigidbody> obstaclesParts;
    [SerializeField] private GrowItemBoxFalling growItem;
    [SerializeField] private ParticleSystem punchPrefab;
    [SerializeField] private ParticleSystem hitSmokePrefab;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource obstacleHitSound;
    [SerializeField] private AudioSource wallDestroySound;
    [SerializeField] private AudioSource obstacleDestroySound;
    
    private const int PlatformLayer = 7;
    private const int GirlHairLayer = 8;
    private const int EnemyLayer = 10;
    private const float ExplodeImpulsePower = 8f;
    private const float FinishWallExplodeImpulsePower = 5f;
    private const float FallingDelta = 0.01f;

    private float _lastYPos;
    private bool _isFalling;
    private bool _isExploded;

    private void Start() => _lastYPos = transform.position.y;

    private void Update()
    {
        _isFalling = _lastYPos - transform.position.y > FallingDelta;
        _lastYPos = transform.position.y;
    }

    private void OnCollisionEnter(Collision other)
    {
        var hitCondition = other.gameObject.layer == GirlHairLayer || other.gameObject.layer == EnemyLayer ||
                            other.gameObject.CompareTag("Girl");
        var fallCondition = other.gameObject.layer == PlatformLayer && _isFalling 
                                                                    && !other.gameObject.CompareTag("Wall");
        if ((hitCondition || fallCondition) && !_isExploded)
        {
            obstacleObject.SetActive(false);
            triggerCollider.enabled = false;
            
            if (other.gameObject.CompareTag("Girl"))
            {
                var girlMovement = other.gameObject.GetComponent<CharacterMovement>();
                girlMovement.DisableAllMovement(false);
                girlMovement.EnableRagDoll(true, true);
            }
            
            obstacleHitSound.Play();
            Instantiate(punchPrefab, other.contacts[0].point, Quaternion.Euler(0,180,0));
            Instantiate(hitSmokePrefab, other.contacts[0].point, Quaternion.identity);
            
            switch (type)
            {
                case ObstacleType.Wall:
                    wallDestroySound.Play();
                    break;
                case ObstacleType.FinishWall:
                    wallDestroySound.Play();
                    break;
                case ObstacleType.Box:
                    if(growItem != null) 
                        growItem.SetToFall();
                    obstacleDestroySound.Play();
                    break;
                default:
                    obstacleDestroySound.Play();
                    break;
            }

            Explode(other.gameObject);
            _isExploded = true;
        }
    }

    private void Explode(GameObject other)
    {
        bool isSideAttack;
        if (other.GetComponentInParent<CharacterMovement>() != null)
            isSideAttack =
                other.gameObject.layer == GirlHairLayer && other.GetComponentInParent<CharacterMovement>().isSideAttack;
        else
            isSideAttack = false;

        foreach (var part in obstaclesParts)
        {
            part.gameObject.SetActive(true);
            if (type == ObstacleType.FinishWall) continue;
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

            var impulsePower = ExplodeImpulsePower;
            part.AddForce(direction * impulsePower, ForceMode.Impulse);
        }
        
        if (type == ObstacleType.Barrel)
        {
            Instantiate(explosionWave, transform.position, Quaternion.identity);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}

public enum ObstacleType
{
    Wall,
    Barrel,
    Box,
    FinishWall
}