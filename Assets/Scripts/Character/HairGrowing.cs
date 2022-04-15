using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Rendering;

namespace Character
{
    public class HairGrowing : MonoBehaviour
    {
        [SerializeField] private List<GameObject> hairSegments;
        [SerializeField] private List<Collider> hairSegmentsBones;
        [SerializeField] private int activeSegmentsCount;
        [SerializeField] private Transform hairEndTransform;
        [SerializeField] private Transform lastHairBone;
        [SerializeField] private MMF_Player hairGrowingFeedback;

        private MMF_Scale _feedbackGrowScale;
        private MMF_SetActive _feedbackSetActive;
        private MMF_ParticlesInstantiation _feedbackParticles;
        private Collider _hairEndCollider;

        private void Start()
        {
            activeSegmentsCount = 0;
            UpdateActiveSegmentsCount();
            
            _feedbackGrowScale = hairGrowingFeedback.GetFeedbackOfType<MMF_Scale>();
            _feedbackSetActive = hairGrowingFeedback.GetFeedbackOfType<MMF_SetActive>();
            _feedbackParticles = hairGrowingFeedback.GetFeedbackOfType<MMF_ParticlesInstantiation>();
            _hairEndCollider = hairEndTransform.GetComponent<Collider>();
        }

        public void GrowHair()
        {
            if (activeSegmentsCount < hairSegments.Count)
            {
                _feedbackGrowScale.AnimateScaleTarget = hairSegmentsBones[activeSegmentsCount].transform;
                _feedbackSetActive.TargetGameObject = hairSegments[activeSegmentsCount];
                _feedbackParticles.InstantiateParticlesPosition = hairSegments[activeSegmentsCount].transform;

                hairSegmentsBones[activeSegmentsCount].enabled = true;

                hairEndTransform.parent = 
                    activeSegmentsCount < hairSegments.Count - 1 ? hairSegmentsBones[activeSegmentsCount + 1].transform 
                        : lastHairBone;

                hairEndTransform.localPosition = Vector3.zero;
                hairEndTransform.localScale = Vector3.one * 0.05f;
                hairEndTransform.localRotation = Quaternion.Euler(70, 200, 180);

                activeSegmentsCount++;
            }
            
            hairGrowingFeedback.PlayFeedbacks();
        }

        public void SetAbleValueForHairColliders(bool value)
        {
            for(var i = 0; i < activeSegmentsCount; i++)
                hairSegmentsBones[i].enabled = value;
            _hairEndCollider.enabled = value;
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
