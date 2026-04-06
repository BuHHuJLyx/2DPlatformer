using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _enemyMask;

    public Health FindClosest(Vector3 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _radius, _enemyMask);

        Health closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Health health))
            {
                float distance = (hit.transform.position - position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = health;
                }
            }
        }

        return closestEnemy;
    }
}
