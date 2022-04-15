using DG.Tweening;
using UnityEngine;

public class ParkourObstacleHitting : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation tween;

    private const int GirlLayer = 3;
    private const int EnemyLayer = 10;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == GirlLayer || other.gameObject.layer == EnemyLayer)
            tween.DOPlay();
    }
}
