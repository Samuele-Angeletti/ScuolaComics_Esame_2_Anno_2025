using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] float damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.TakeDamage(damageAmount);
        }
    }
}
