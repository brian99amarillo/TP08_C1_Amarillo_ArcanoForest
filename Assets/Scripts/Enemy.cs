//using UnityEngine;
//using UnityEngine.UI;

//public class Enemy : MonoBehaviour
//{

//    [SerializeField] protected SettingsEnemySO settingsEnemy;
//    [SerializeField] private Image barLife;
//    [SerializeField] private GameObject deteccionZone;
//    protected Transform target;
//    protected Rigidbody2D rb;

//    protected float life;
//    protected bool isDead;

//    public float Damage => settingsEnemy.Damage;

//    protected virtual void Start()
//    {
//        life = settingsEnemy.MaxLife;
//    }

//    protected virtual void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }
//    public virtual void TakeDamage(float amount)
//    {
//        if (isDead) return;

//        life -= amount;
//        UpdateHealthBar(life, settingsEnemy.MaxLife);
//        Debug.Log("Daño recibido");

//        if (life <= 0f)
//        {
//            Die();
//        }
//    }

//    private void UpdateHealthBar(float currentLife, float maxLife)
//    {
//        barLife.fillAmount = currentLife / maxLife;
//    }

//    protected virtual void Die()
//    {   
//        Destroy(gameObject);
//    }

//    public virtual void OnPlayerDetected(Transform player)
//    {
//        target = player;
//    }

//    public virtual void OnPlayerLost()
//    {
//        target = null;
//    }
//}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected SettingsEnemySO settingsEnemy;
    [SerializeField] protected Animator animator;
    [SerializeField] private Image barLife; 
    [SerializeField] private float deathDuration = 1f;
    [SerializeField] private GameObject deathParticles; // opcional

    // Estados comunes: el nombre de cada uno tiene que coincidir con un estado del Animator
    public enum EnemyStatus { Idle, Walk, Attack, Die }
    protected EnemyStatus currentStatus;

    protected Rigidbody2D rb;
    protected Transform target;      // el jugador, mientras esté en la zona de detección
    protected float life;
    protected bool isDead;
    protected bool isKnockedBack;
    private Coroutine behaviourRoutine;

    public float Damage => settingsEnemy.Damage; // lo lee el jugador al chocar

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        life = settingsEnemy.MaxLife;
        UpdateHealthBar(life, settingsEnemy.MaxLife); // la barra arranca llena
    }

    //private void LateUpdate()
    //{
    //    // la barra (Canvas en World Space) no se da vuelta cuando el enemigo cambia de dirección
    //    if (barLife != null && barLife.canvas.renderMode == RenderMode.WorldSpace)
    //    {
    //        barLife.canvas.transform.rotation = Quaternion.identity;
    //    }
    //}

    // ---------- Comportamiento: cada hijo define el suyo ----------
    protected virtual IEnumerator Behaviour()
    {
        yield break;
    }

    // ---------- Animación ----------
    protected void ChangeStatus(EnemyStatus newStatus)
    {
        if (newStatus == currentStatus) return;
        currentStatus = newStatus;

        if (animator != null)
        {
            animator.Play(newStatus.ToString());
        }
    }

    // ---------- Daño, barra de vida y retroceso ----------
    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        life -= amount;
        UpdateHealthBar(life, settingsEnemy.MaxLife);

        if (life <= 0f)
        {
            Die();
        }
    }

    private void UpdateHealthBar(float currentLife, float maxLife)
    {
        if (barLife == null) return;

        barLife.fillAmount = Mathf.Clamp01(currentLife / maxLife);
    }

    //public void Knockback(Vector2 fromPosition, float force)
    //{
    //    if (isDead || rb == null) return;

    //    float direction = Mathf.Sign(transform.position.x - fromPosition.x); // lejos del jugador
    //    rb.linearVelocity = Vector2.zero;
    //    rb.AddForce(new Vector2(direction, 0.3f).normalized * force, ForceMode2D.Impulse);
    //    StartCoroutine(KnockbackRoutine());
    //}

    //private IEnumerator KnockbackRoutine()
    //{
    //    isKnockedBack = true;
    //    yield return new WaitForSeconds(0.2f);
    //    isKnockedBack = false;
    //}

    // ---------- Detección (los llama DetectionZone) ----------
    public virtual void OnPlayerDetected(Transform player)
    {
        if (isDead) return;

        target = player;

        if (behaviourRoutine == null)
        {
            behaviourRoutine = StartCoroutine(Behaviour());
        }
    }

    public virtual void OnPlayerLost()
    {
        if (isDead) return;

        target = null;

        if (behaviourRoutine != null)
        {
            StopCoroutine(behaviourRoutine);
            behaviourRoutine = null;
        }

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }

        ChangeStatus(EnemyStatus.Idle);
    }

    // ---------- Muerte ----------
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        StopAllCoroutines();
        behaviourRoutine = null;
        rb.simulated = false; // sin física: no daña, no recibe golpes, no activa la zona

        ChangeStatus(EnemyStatus.Die);

        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        AudioManager.Instance.EnemyDie();

        Destroy(gameObject, deathDuration);
    }
}