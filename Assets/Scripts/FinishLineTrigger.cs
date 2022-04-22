using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    private const string GirlTag = "Girl";
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag(GirlTag)) return;
        other.GetComponent<FinishHairShooting>().SetWaitForFinalAction();
    }
}
