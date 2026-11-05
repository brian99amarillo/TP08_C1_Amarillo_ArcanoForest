using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SettingsPlayerSO settingsPlayer;
    [SerializeField] private GameObject playerAttack;
    [SerializeField] private float attackDuration = 0.4f;

    private float baseDamage;
    private float bonusDamage;
    private float attackTimer;
    private bool hasHit;
    private Coroutine bonusRoutine;

    public bool isAttacking { get; private set; }

    private float CurrentDamage
    {
        get { return baseDamage + bonusDamage; }
    }

    private void Start()
    {
        baseDamage = settingsPlayer.Damage;
    }

    private void Update()
    {
        AttackingPlayer();
    }

    private void AttackingPlayer()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // click izquierdo: empieza el ataque
        {
            isAttacking = true;
            hasHit = false;
            attackTimer = attackDuration;
            playerAttack.SetActive(true);
            AudioManager.Instance.PlayerAttack(); // Reproduce el sonido de ataque
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
                playerAttack.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAttacking || hasHit) return;

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(CurrentDamage);
            hasHit = true;
        }
    }

    // Lo llama el pickable de daño
    public void ActivateExtraDamage(float amount, float seconds)
    {
        if (bonusRoutine != null)
        {
            StopCoroutine(bonusRoutine); // si ya había uno activo, lo reemplaza y reinicia el tiempo
        }

        bonusRoutine = StartCoroutine(ExtraDamageRoutine(amount, seconds));
    }

    private IEnumerator ExtraDamageRoutine(float amount, float seconds)
    {
        bonusDamage = amount;
        yield return new WaitForSeconds(seconds);
        bonusDamage = 0f;
        bonusRoutine = null;
    }
}