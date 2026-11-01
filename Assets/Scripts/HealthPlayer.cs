using System;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public static event Action<float,float> OnHealthChange;

    [SerializeField] private SettingsPlayerSO settingsPlayer;
    private float life;

    private void Start()
    {
        life = settingsPlayer.Life;
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            DamageToEnemy(enemy.Damage);
        }
    }

    public void DamageToEnemy(float damage)
    {
        life = Mathf.Clamp(life - damage, 0f, settingsPlayer.Life);
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
        Debug.Log("daño enemigo");
    }
}
