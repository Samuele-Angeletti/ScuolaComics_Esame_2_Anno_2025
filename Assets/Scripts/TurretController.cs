using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Targeting")]
    public float range = 5f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;

    [Header("References")]
    public Transform cannonGraphics;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        var enemies = hits
            .Where(h => h.GetComponent<EnemyController>() != null)
            .Select(h => h.transform)
            .ToList();

        if (enemies.Count == 0)
            return;

        Transform target = enemies
            .OrderBy(t => Vector2.Distance(transform.position, t.position))
            .First();

        Vector2 dir = (target.position - cannonGraphics.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        cannonGraphics.rotation = Quaternion.Euler(0f, 0f, angle);

        if (fireCooldown <= 0f)
        {
            Shoot(dir);
            fireCooldown = 1f / fireRate;
        }
    }

    private void Shoot(Vector2 direction)
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        var rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * rb.linearVelocity.magnitude;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
