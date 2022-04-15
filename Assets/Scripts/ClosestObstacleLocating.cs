using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class ClosestObstacleLocating : MonoBehaviour
{
   [SerializeField] private List<Transform> obstacles;
   [SerializeField] private Transform obstaclesParent;
   [SerializeField] private Transform edgePoint;
   private void Start() => DetectObstacles();

   public Transform FindClosestObstacleToObject(Transform objectTransform)
   {
      var closest = obstacles[0];
      for (var i = 1; i < obstacles.Count; i++)
      {
         if (Vector3.Distance(obstacles[i].position, objectTransform.position) <
             Vector3.Distance(obstacles[i - 1].position, objectTransform.position))
         {
            if(closest.transform.position.z >= objectTransform.transform.position.z)
               closest = obstacles[i];
         }
      }

      if (closest.gameObject.GetInstanceID() == obstacles[0].gameObject.GetInstanceID())
         closest = edgePoint;
      
      return closest;
   }

   private void DetectObstacles()
   {
      var motionlessObstacles = obstaclesParent.GetComponentsInChildren<ObstacleExplosion>();
      var bandits = obstaclesParent.GetComponentsInChildren<EnemyMovement>();

      if (motionlessObstacles.Length > 0)
         foreach (var obstacle in motionlessObstacles)
            obstacles.Add(obstacle.transform);
      
      if (bandits.Length > 0)
         foreach (var bandit in bandits)
            obstacles.Add(bandit.transform);
   }
}
