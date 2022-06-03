using System.Collections.Generic;
using Character;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finishCanvasWindow;
    [SerializeField] private List<ParticleSystem> confetti;
    [SerializeField] private AudioSource finishSound;
    private const int GirlLayer = 3;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != GirlLayer) return;
        
        FindObjectOfType<CameraChanging>().ChangeCamera(CameraType.Finish);
        finishCanvasWindow.SetActive(true);
        finishSound.Play();
       

        if(other.gameObject.CompareTag("Girl"))
            other.GetComponent<CharacterMovement>().SetFinish();
        else
            other.GetComponentInParent<CharacterMovement>().SetFinish();
        foreach (var particle in confetti)
            particle.Play();
       
        GetComponent<Collider>().enabled = false;
    }
}
