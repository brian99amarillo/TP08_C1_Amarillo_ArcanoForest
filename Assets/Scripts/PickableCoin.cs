using UnityEngine;

public class PickableCoin : MonoBehaviour
{
    private static float value = 0;
    [SerializeField] private GameObject PickCoinPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            value++;
            AudioManager.Instance.PickCoin();
            UIcoinCounter.Instance.UpdateCoinCounter(value);
            Destroy(gameObject);
        }
    }
}