using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected SettingsEnemySO settingsEnemy;
    [SerializeField] protected Animator animator;
    [SerializeField] private Image barLife; 
    [SerializeField] private float deathDuration = 1f;
 
    
    public static event Action<Vector3> OnEnemyDied;  // Avisa dónde murió un enemigo (lo escucha PickablesFactory para soltar un pickable)


    // Estados de los enemigos
    public enum EnemyStatus { Idle, Walk, Attack, Die }
    protected EnemyStatus currentStatus;

    protected Rigidbody2D rb;
    protected Transform target;      // el jugador, mientras esté en la zona de detección
    protected float life;
    protected bool isDead;
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

    // Comportamiento: cada enemigo hace su comportamiento en su propia clase, que hereda de esta
    protected virtual IEnumerator Behaviour()
    {
        yield break;
    }

    // Maquina de estados de los enemigos
    protected void ChangeStatus(EnemyStatus newStatus)
    {
        if (newStatus == currentStatus) return;
        currentStatus = newStatus;

        if (animator != null)
        {
            animator.Play(newStatus.ToString());
        }
    }

    // Daño recibido por el jugador 
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

    // Actualiza la barra de vida 
    private void UpdateHealthBar(float currentLife, float maxLife)
    {
        if (barLife == null) return;

        barLife.fillAmount = Mathf.Clamp01(currentLife / maxLife);
    }

    //  Detección del jugador (los llama DetectionZone) 
    public virtual void OnPlayerDetected(Transform player)
    {
        if (isDead) return;

        target = player;

        if (behaviourRoutine == null)
        {
            behaviourRoutine = StartCoroutine(Behaviour());
        }
    }

    // Perdida del jugador (los llama DetectionZone)
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

    //  Muerte del enemigo: desactiva la física, la zona de detección y destruye el objeto
    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        StopAllCoroutines();
        behaviourRoutine = null;
        rb.simulated = false; // sin física: no daña, no recibe golpes, no activa la zona

        ChangeStatus(EnemyStatus.Die);

        OnEnemyDied?.Invoke(transform.position); // Avisa dónde murió un enemigo y le pasa la posición (lo escucha PickablesFactory para soltar un pickable)
        AudioManager.Instance.EnemyDie();
        Destroy(gameObject, deathDuration);
    }
}