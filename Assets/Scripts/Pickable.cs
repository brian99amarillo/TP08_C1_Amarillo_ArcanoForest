using UnityEngine;

public class Pickable : MonoBehaviour
{

    [SerializeField] protected Color colorToApply;
    [SerializeField] protected float durationNewColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Player")) return;

        // cada hijo decide qué hace y si se pudo aplicar
        if (Apply(collision.gameObject))
        {
            TintPlayer(collision.gameObject);
            Destroy(gameObject);
        }
    }

    // el hijo la sobrescribe; devuelve true si el efecto se aplicó
    protected virtual bool Apply(GameObject player)
    {
        return true;
    }

    private void TintPlayer(GameObject player)
    {
        SpriteRenderer playerRenderer = player.GetComponentInParent<SpriteRenderer>();

        if (playerRenderer == null)
        {
            playerRenderer = player.GetComponentInChildren<SpriteRenderer>(true);
        }

        if (playerRenderer != null)
        {
            ChangeColor.Change(playerRenderer, colorToApply, durationNewColor);
        }
    }
}