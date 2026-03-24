using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float GetDirection(Transform self, Transform target) => Mathf.Sign(target.position.x - self.position.x);
}