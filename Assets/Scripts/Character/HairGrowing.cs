using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    public class HairGrowing : MonoBehaviour
    {
        [SerializeField] private List<GameObject> hairSegments;
        [SerializeField] private List<Collider> hairSegmentsBones;
        [SerializeField] private int activeSegmentsCount;
        [SerializeField] private ParticleSystem bubbles;
        [SerializeField] private MMF_Player hairGrowingFeedback;

        private MMF_Scale _feedbackGrowScale;
        private MMF_SetActive _feedbackSetActive;
        private MMF_ParticlesInstantiation _feedbackParticles;

        private void Start()
        {
            activeSegmentsCount = 0;
            UpdateActiveSegmentsCount();
            
            _feedbackGrowScale = hairGrowingFeedback.GetFeedbackOfType<MMF_Scale>();
            _feedbackSetActive = hairGrowingFeedback.GetFeedbackOfType<MMF_SetActive>();
            _feedbackParticles = hairGrowingFeedback.GetFeedbackOfType<MMF_ParticlesInstantiation>();
        }

        public void GrowHair()
        {
            if (activeSegmentsCount >= hairSegments.Count) return;
            _feedbackGrowScale.AnimateScaleTarget = hairSegmentsBones[activeSegmentsCount].transform;
            _feedbackSetActive.TargetGameObject = hairSegments[activeSegmentsCount];
            _feedbackParticles.InstantiateParticlesPosition = hairSegments[activeSegmentsCount].transform;
            hairGrowingFeedback.PlayFeedbacks();
            
            hairSegmentsBones[activeSegmentsCount].enabled = true;
            activeSegmentsCount++;
        }

        private void UpdateActiveSegmentsCount()
        {
            foreach (var t in hairSegments)
            {
                if (t.activeInHierarchy)
                    activeSegmentsCount++;
            }
        }
    }
}
