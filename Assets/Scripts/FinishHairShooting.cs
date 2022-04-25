using System.Collections;
using Character;
using FIMSpace.FTail;
using UnityEngine;

public class FinishHairShooting : MonoBehaviour
{
    [SerializeField] private TailAnimator2 hairTailAnimator;
    [SerializeField] private GameObject levelComplete;
    [SerializeField] private float hairRotationSpeed;
    [SerializeField] private float hairFlySpeed;
    [SerializeField] private float flyTime;
    [SerializeField] private AudioSource hairSpinOnWait;
    [SerializeField] private AudioSource finalAttackWait;
    [SerializeField] private AudioSource finalAttack;
    
    private CharacterAnimating _girlAnimation;
    private CharacterMovement _characterMovement;
    private CameraChanging _cameraChanging;
    private bool _isWaitingForAction;
    private bool _isReadyForAction;
    private bool _isHairFly;
    
    #region TailAnimatorConsts

    private const float HairSlitheryDefault = 1f;
    private const float HairSlitheryOnAction = 0.1f;
    private const float HairCurlingDefault = 0.8f;
    private const float HairAngleLimitOnAction = 10;
    private const float HairAngleLimitDefault = 181;
    private const float HairCurlingOnAction = 0.1f;

    #endregion

    private void Start()
    {
        _girlAnimation = GetComponent<CharacterAnimating>();
        _characterMovement = GetComponent<CharacterMovement>();
        _cameraChanging = FindObjectOfType<CameraChanging>();
    }

    private void FixedUpdate()
    {
        if(_isHairFly)
            hairTailAnimator.transform.position += Vector3.forward * hairFlySpeed * Time.deltaTime;
    }

    public void SetWaitForFinalAction()
    {
        _characterMovement.SetFinish();
        _girlAnimation.SetFinishIdle();
        _isWaitingForAction = true;
    }

    public void StartAction()
    {
        if (_isReadyForAction)
            StartCoroutine(DelayedAction());
        else
            _isReadyForAction = true;
    }

    private IEnumerator DelayedAction()
    {
        _girlAnimation.SetFinalHit();
        _cameraChanging.ChangeCamera(CameraType.Hair);
        yield return new WaitForSeconds(0.5f);
        var hairAnimatorTransform = hairTailAnimator.transform;
        hairAnimatorTransform.position =
            new Vector3(0, hairAnimatorTransform.position.y, hairAnimatorTransform.position.z);
        hairTailAnimator.Curling = HairCurlingOnAction;
        hairTailAnimator.Slithery = HairSlitheryOnAction;
        hairTailAnimator.AngleLimit = HairAngleLimitOnAction;
        _isWaitingForAction = false;
        finalAttack.Play();
        hairTailAnimator.transform.parent = null;
        hairTailAnimator.transform.rotation = Quaternion.Euler(0, 180, 0);
        _isHairFly = true;
        StartCoroutine(DisableFly(flyTime));
    }

    private IEnumerator DisableFly(float hairFlyTime)
    {
        yield return new WaitForSeconds(hairFlyTime);
        _cameraChanging.FreezeCamera();
        _isHairFly = false;
        hairTailAnimator.Curling = HairCurlingDefault;
        hairTailAnimator.Slithery = HairSlitheryDefault;
        hairTailAnimator.AngleLimit = HairAngleLimitDefault;
        hairTailAnimator.UseWaving = true;
        hairTailAnimator.Gravity = new Vector3(0, -9, 0);
        var hairObjectCollider = hairTailAnimator.gameObject.AddComponent<CapsuleCollider>();
        hairObjectCollider.radius = 0.1f;
        hairObjectCollider.center = Vector3.up * 1.6f;
        hairTailAnimator.gameObject.AddComponent(typeof(Rigidbody));
        levelComplete.SetActive(true);
    }
}
