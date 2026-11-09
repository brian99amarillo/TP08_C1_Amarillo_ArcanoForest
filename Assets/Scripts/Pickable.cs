using UnityEngine;

public class Pickable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // cada hijo decide qué hace y si se pudo aplicar
        if (Apply(collision.gameObject))
        {
            Destroy(gameObject);
        }
    }

    // el hijo la sobrescribe; devuelve true si el efecto se aplicó
    protected virtual bool Apply(GameObject player)
    {
        return true;
    }
}