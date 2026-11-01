using UnityEngine;

public class ProyectileEnemy : MonoBehaviour
{
    [SerializeField] private float proyectileSpeed = 5f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float damage = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().linearVelocity = direction * proyectileSpeed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthPlayer player = collision.GetComponentInParent<HealthPlayer>();

            if (player != null)
            {
                player.DamageToEnemy(damage);
            }

            Destroy(gameObject);
        }
    }

}
