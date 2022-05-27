using System.Collections;
using UnityEngine;

namespace Character
{
    public class CharacterFootstepsSoundPlaying : MonoBehaviour
    {
        [SerializeField] private AudioSource footstepSound1;
        [SerializeField] private AudioSource footstepSound2;
        [SerializeField] private float walkingSoundPlayDelay;
        [SerializeField] private float runningSoundPlayDelay;

        public bool isWalking;
        private bool _wasPlayedFirstSound;
        private float _currentSoundPlayDelay;

        private void Start()
        {
            SetSoundDelayToRunning();
            StartCoroutine(DelayPlay());
        }
        
        public void SetSoundDelayToRunning() => _currentSoundPlayDelay = runningSoundPlayDelay;

        private void SwapTrack()
        {
            if(!isWalking) return;

            if (_wasPlayedFirstSound)
                footstepSound2.Play();
            else
                footstepSound1.Play();

            _wasPlayedFirstSound = !_wasPlayedFirstSound;
        }


        private IEnumerator DelayPlay()
        {
            yield return new WaitForSeconds(_currentSoundPlayDelay);
            SwapTrack();
            StartCoroutine(DelayPlay());
        }
    }
}
