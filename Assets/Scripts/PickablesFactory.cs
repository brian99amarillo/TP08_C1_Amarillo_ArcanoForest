using UnityEngine;

public class PickablesFactory : MonoBehaviour
{


    // Prefab del objeto recolectable
    [SerializeField] private GameObject pickableHealthPrefab; 
    [SerializeField] private GameObject pickableDamagePrefab; 
    [SerializeField] private GameObject pickableInvincibilityPrefab;

    public enum PickableType { Health, Damage, Invincibility }

    [SerializeField, Range(0f, 1f)] private float dropChance = 0.5f; // Probabilidad de soltar un recolectable al morir un enemigo (0 = nunca, 1 = siempre)

    private void OnEnable() { Enemy.OnEnemyDied += OnEnemyDied; }
    private void OnDisable() { Enemy.OnEnemyDied -= OnEnemyDied; }

    private void OnEnemyDied(Vector3 position)
    {
        if (Random.value >= dropChance) return; // esta vez no suelta nada

        int count = System.Enum.GetValues(typeof(PickableType)).Length;  // obtiene la cantidad de tipos de recolectables del enum
        PickableType type = (PickableType)Random.Range(0, count); // el tipo se sortea en cada muerte
        CreatePickable(type, position);
    }

    public GameObject CreatePickable(PickableType type, Vector3 position)
    {
        GameObject pickablePrefab = null; // Variable para almacenar el prefab correspondiente al tipo de recolectable

        switch (type)
        {
            case PickableType.Health:
                pickablePrefab = pickableHealthPrefab;
                break;
            case PickableType.Damage:
                pickablePrefab = pickableDamagePrefab;
                break;
            case PickableType.Invincibility:
                pickablePrefab = pickableInvincibilityPrefab;
                break;
        }

        if (pickablePrefab != null) // Verifica si el prefab no es nulo antes de instanciar
        {
            return Instantiate(pickablePrefab, position, Quaternion.identity);
        }
        return null;
    }

}
