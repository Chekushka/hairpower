using Character;
using DG.Tweening;
using MoreMountains.Tools;
using UnityEngine;

public class ChargedAttackBarFill : MonoBehaviour
{
   [SerializeField] private float maxPoints;
   private MMProgressBar _bar;
   private DOTweenAnimation _tweenAnimation;
   private CharacterMovement _characterMovement;
   private float _currentPoints;
   private bool _isScaleAnimPlaying;
   private const float MinPointsValue = 0;

   private void Start()
   {
      _bar = GetComponent<MMProgressBar>();
      _tweenAnimation = GetComponent<DOTweenAnimation>();
      _characterMovement = FindObjectOfType<CharacterMovement>();
      ClearBar();
   }

   public void AddBarPoints(float points)
   {
      _currentPoints += points;
      if (_currentPoints >= maxPoints)
      {
         _currentPoints = maxPoints;
         _characterMovement.SetAbleToSpin();
         if (!_isScaleAnimPlaying)
         {
            _tweenAnimation.DOPlay();
            _isScaleAnimPlaying = true;
         }
      }
      _bar.UpdateBar(_currentPoints, MinPointsValue, maxPoints);
   }

   public void ClearBar()
   {
      _bar.SetBar(MinPointsValue, MinPointsValue, maxPoints);
      _currentPoints = MinPointsValue;
      if (_isScaleAnimPlaying)
      {
         _tweenAnimation.DOPause();
         _isScaleAnimPlaying = false;
      }
   }
}
