using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 1;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
