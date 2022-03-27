using System.Collections;
using UnityEngine;

public class ObjectDestroying : MonoBehaviour
{
   [SerializeField] private float delay;

   private void Start() => StartCoroutine(DelayedDestroy());

   private IEnumerator DelayedDestroy()
   {
      yield return new WaitForSeconds(delay);
      Destroy(gameObject);
   }
}
