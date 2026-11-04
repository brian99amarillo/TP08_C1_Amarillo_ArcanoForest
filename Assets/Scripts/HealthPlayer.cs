using System;
using UnityEngine;
using System.Collections;

public class HealthPlayer : MonoBehaviour
{
    public static event Action<float,float> OnHealthChange;
    private bool isInvulnerable = false;

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
        if (isInvulnerable) return;

        life = Mathf.Clamp(life - damage, 0f, settingsPlayer.Life);
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
        Debug.Log("daño del enemigo");
    }

    public bool Heal(float amount)
    {
        if (life >= settingsPlayer.Life) return false; 

        life = Mathf.Clamp(life + amount, 0f, settingsPlayer.Life);
        OnHealthChange?.Invoke(life, settingsPlayer.Life);
        return true;
    }

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
}
