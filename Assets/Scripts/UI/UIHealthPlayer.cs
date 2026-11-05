using UnityEngine;
using UnityEngine.UI;

public class UIHealthPlayer : MonoBehaviour
{
    [SerializeField] private Image barLife;

    private void OnEnable() { HealthPlayer.OnHealthChange += UpdateHealthBar; }
    private void OnDisable() { HealthPlayer.OnHealthChange -= UpdateHealthBar; }

    private void UpdateHealthBar(float currentLife, float maxLife)
    {
        barLife.fillAmount = currentLife / maxLife;
    }


}
