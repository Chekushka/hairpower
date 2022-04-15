using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleParticlesDisappearTimeSetting : MonoBehaviour
{
    private DOTweenAnimation _tween;
    private const float MinDelayValue = 2f;
    private const float MaxDelayValue = 4f;

    private void Awake()
    {
        _tween = GetComponent<DOTweenAnimation>();
        _tween.delay = Random.Range(MinDelayValue, MaxDelayValue);
    }
}
