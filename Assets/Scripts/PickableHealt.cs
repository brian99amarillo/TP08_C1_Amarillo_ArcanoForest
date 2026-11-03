using UnityEngine;

public class PickableHealt : MonoBehaviour
{
    [SerializeField] private float healthValue = 100f;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthPlayer player = collision.GetComponentInParent<HealthPlayer>();
            if (player != null)
            {
                player.Heal(healthValue);
                //AudioManager.Instance.PlayerHealt();
                Destroy(gameObject);
            }
        }
    }

}
