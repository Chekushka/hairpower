using System;
using Character;
using UnityEngine;

namespace Bandit
{
    [RequireComponent(typeof(BanditAnimating))]
    public class BanditMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float distanceWhenPlayerBecomeVisible;
        [SerializeField] private Transform hips;
        [SerializeField] private ParticleSystem onHitParticles;
        [SerializeField] private AudioSource hitSound;

        private CharacterMovement _girlMovement;
        private Transform _girlTransform;
        private BanditAnimating _animating;
        private Collider _mainCollider;
        private bool _isPlayerVisible;
        private bool _isMoving;

        private const int GirlHairLayer = 8;
        private const int GirlLayer = 3;

        private void Start()
        {
            _animating = GetComponent<BanditAnimating>();
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
                    break;
                case GirlLayer:
                    hitSound.Play();
                    _isMoving = false;
                    _animating.SetAttack();
                    _girlMovement.DisableAllMovement();
                    _girlMovement.EnableRagDoll(true);
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
                if(_girlMovement.isSideAttack)  
                    rb.AddForce((Vector3.forward + Vector3.left) * 10, ForceMode.Impulse);
                else
                    rb.AddForce((Vector3.forward) * 15, ForceMode.Impulse);
            }
        }
        
        private void MoveForward() => transform.position += Vector3.back * movementSpeed * Time.deltaTime;

        private void MoveToPlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _girlTransform.position, movementSpeed * Time.deltaTime);
            transform.LookAt(_girlTransform);
        }
        
    }
}
