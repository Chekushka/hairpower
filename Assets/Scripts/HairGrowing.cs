using System.Collections.Generic;
using UnityEngine;

public class HairGrowing : MonoBehaviour
{
    [SerializeField] private List<GameObject> hairSegments;
    [SerializeField] private List<Collider> hairSegmentsBones;
    [SerializeField] private int activeSegmentsCount;
    [SerializeField] private ParticleSystem bubbles;

    private void Start()
    {
        activeSegmentsCount = 0;
        UpdateActiveSegmentsCount();
    }

    public void GrowHair()
    {
        var segment = hairSegments[activeSegmentsCount];
        segment.transform.localScale = Vector3.zero;
        segment.SetActive(true);
        hairSegmentsBones[activeSegmentsCount].enabled = true;
        activeSegmentsCount++;
        Instantiate(bubbles, segment.transform.position, Quaternion.identity);
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
