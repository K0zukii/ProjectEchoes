using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemyNavMesh;

    public Vector3 GetRandomDestination(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;

        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    public bool HasReachedDestination()
    {
        bool isArrived = false;
        if(enemyNavMesh.remainingDistance <= enemyNavMesh.stoppingDistance && !enemyNavMesh.pathPending)
        {
            isArrived = true;
        }
        return isArrived;
    }

    public void MoveToPosition(Vector3 target)
    {
        enemyNavMesh.isStopped = false;
        enemyNavMesh.SetDestination(target);
    }

    public void StopMoving()
    {
        enemyNavMesh.isStopped = true;
    }

    public void SetSpeed(float speed)
    {
        enemyNavMesh.speed = speed;
    }
}
