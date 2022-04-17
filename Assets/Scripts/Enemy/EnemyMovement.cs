using System.Collections;
using Character;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyAnimating))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float distanceWhenPlayerBecomeVisible;
        [SerializeField] private float girlHitPower = 15;
        [SerializeField] private float delayToGirlFall;
        [SerializeField] private bool isNeededForSideDirection;
        [SerializeField] private Vector3 sideDirection;
        [SerializeField] private Transform hips;
        [SerializeField] private ParticleSystem onHitParticles;
        [SerializeField] private AudioSource hitSound;
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private AudioSource deathSound;

        private CharacterMovement _girlMovement;
        private Transform _girlTransform;
        private EnemyAnimating _animating;
        private Collider _mainCollider;
        private bool _isPlayerVisible;
        private bool _isMoving;

        private const int GirlHairLayer = 8;
        private const int GirlLayer = 3;
        private const int ObstacleLayer = 6;

        private void Start()
        {
            _animating = GetComponent<EnemyAnimating>();
            _mainCollider = GetComponent<Collider>();
            _girlMovement = FindObjectOfType<CharacterMovement>();
            _girlTransform = _girlMovement.transform;
            
            EnableRagDoll(false);
        }

        private void Update()
        {
            if (_isPlayerVisible)
            {
                if(_isMoving)
                    MoveToPlayer();
            }
            else
            {
                if (Vector3.Distance(transform.position, _girlTransform.position) < distanceWhenPlayerBecomeVisible 
                && !_isPlayerVisible)
                {
                    _isPlayerVisible = true;
                    EnableMovement();
                }
                
                // MoveForward();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case GirlHairLayer:
                    hitSound.Play();
                    _isMoving = false;
                    Instantiate(onHitParticles, other.transform.position, Quaternion.identity);
                    EnableRagDoll(true);
                    deathSound.Play();
                    break;
                case ObstacleLayer:
                    if (other.gameObject.CompareTag("ExplosionWave"))
                    {
                        hitSound.Play();
                        _isMoving = false;
                        Instantiate(onHitParticles, other.transform.position, Quaternion.identity);
                        EnableRagDoll(true);
                        deathSound.Play();
                    }
                    break;
                case GirlLayer:
                    _isMoving = false;
                    _animating.SetAttack();
                    _girlMovement.DisableAllMovement();
                    StartCoroutine(DelayedGirlFall(delayToGirlFall));
                    break;
            }
        }

        private void EnableMovement()
        {
            _isMoving = true;
            _animating.SetMoving();
        }

        private void EnableRagDoll(bool value)
        {
            if(value)
                _animating.DisableAnimator();
            
            _mainCollider.enabled = !value;
            
            var rigidbodies = hips.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = !value;

                if (isNeededForSideDirection)
                    rb.AddForce((Vector3.forward + sideDirection) * girlHitPower, ForceMode.Impulse);
                else
                {
                    if(_girlMovement.isSideAttack)  
                        rb.AddForce((Vector3.forward + Vector3.left) * 10, ForceMode.Impulse);
                    else
                        rb.AddForce((Vector3.forward) * girlHitPower, ForceMode.Impulse);
                }
            }
        }
        
        private void MoveForward() => transform.position += Vector3.back * movementSpeed * Time.deltaTime;

        private void MoveToPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _girlTransform.position, movementSpeed * Time.deltaTime);
            transform.LookAt(_girlTransform);
        }

        private IEnumerator DelayedGirlFall(float delay)
        {
            yield return new WaitForSeconds(delay);
            attackSound.Play();
            _girlMovement.EnableRagDoll(true, true);
        }
    }
}
