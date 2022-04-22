using FIMSpace.FTail;
using UnityEngine;

public class HairCollideWithObjectsFinding : MonoBehaviour
{
   [SerializeField] private Transform platformsParent;
   private TailAnimator2 _tailAnimator;

   private void Start()
   {
      _tailAnimator = GetComponent<TailAnimator2>();
      var platformsColliders = platformsParent.GetComponentsInChildren<Collider>();
      foreach (var platformCollider in platformsColliders)
         _tailAnimator.AddCollider(platformCollider);
   }
}
