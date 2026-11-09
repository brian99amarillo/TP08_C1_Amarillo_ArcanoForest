using System;
using UnityEngine;
using System.Collections;

public class HealthPlayer : MonoBehaviour
{
    public static event Action<float,float> OnHealthChange;
    public static event Action OnPlayerDied;
    [SerializeField] private SettingsPlayerSO settingsPlayer;
    private bool isInvulnerable = false;
    private bool isDead;
    private float life;


    private void Start()
    {
        life = settingsPlayer.Life;
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
    }

    // Avisa si fue tocado por el enemigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            DamageToEnemy(enemy.Damage);
        }
    }

    //Avias si fue dañado por el enemigo/proyectil    
    public void DamageToEnemy(float damage)
    {   
        if (isDead || isInvulnerable) return;

        life = Mathf.Clamp(life - damage, 0f, settingsPlayer.Life);
        OnHealthChange?.Invoke(life, settingsPlayer.Life);

        if (life <= 0)
        {
            Die();
        }
    }

    // Si agarra el pickable de vida, se cura
    public bool Heal(float amount)
    {
        if (isDead || life >= settingsPlayer.Life) return false; 

        life = Mathf.Clamp(life + amount, 0f, settingsPlayer.Life);
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
        return true;
    }

    // Hace que el jugador sea invulnerable por un tiempo determinado
    public void SetInvulnerable(float seconds)
    {
        StopAllCoroutines(); // si recoge otro, reinicia el tiempo
        StartCoroutine(InvulnerableRoutine(seconds));
    }

    private IEnumerator InvulnerableRoutine(float seconds)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(seconds);
        isInvulnerable = false;
    }

    public void Die()
    {
        isDead = true;
        OnPlayerDied?.Invoke();
    }
}
