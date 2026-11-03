using UnityEngine;
using System.Collections;

public class EnemySlimeGreen : Enemy
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private bool isAttacking = false;
    private bool isRunning = false;
    private float direction = -1f;
    [SerializeField] private float runTime = 0f;
    [SerializeField] private float idleTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start(); 
        transform.right = new Vector2(-direction, 0f);
    }

    private void Update()
    {
        UpdateStatus();
    }
    private IEnumerator RutineMovement()
    {
        while (true)
        {   
            //Idle
            rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
            ChangeEnemyStatus(EnemyStatus.Idle);
            yield return new WaitForSeconds(idleTime);

            //Run
            direction = Mathf.Sign(target.position.x - transform.position.x); 
            transform.right = new Vector2(direction, 0);
            isRunning = true;
            ChangeEnemyStatus(EnemyStatus.Walk);
            rb.linearVelocity = new Vector2(direction * settingsEnemy.Speed, rb.linearVelocityY);
            yield return new WaitForSeconds(runTime);
            isRunning = false;
        }
    }
    protected override void Die()
    {
        // acá van las partículas, el sonido y la animación de muerte del Walker
        base.Die(); // después ejecuta lo del padre (destruir el objeto)
    }

    public enum EnemyStatus { Idle, Walk, Attack }
    private EnemyStatus CurrentStatus;
    private void ChangeEnemyStatus(EnemyStatus NewStatus)
    {
        if (NewStatus == CurrentStatus) return;
        CurrentStatus = NewStatus;

        switch(CurrentStatus)
        {
            case EnemyStatus.Idle:
                animator.Play("Idle");
                break;
            case EnemyStatus.Walk:
                animator.Play("Walk");
                break;
            case EnemyStatus.Attack:
                animator.Play("Attack");
                break;
        }
    }

    private void UpdateStatus()
    {
        if (isAttacking)
            ChangeEnemyStatus(EnemyStatus.Attack);
        else if (isRunning)
            ChangeEnemyStatus(EnemyStatus.Walk);
        else
            ChangeEnemyStatus(EnemyStatus.Idle);

    }

    private Coroutine movementRoutine;

    public override void OnPlayerDetected(Transform player)
    {
        base.OnPlayerDetected(player);

        if (movementRoutine == null) 
        {
            movementRoutine = StartCoroutine(RutineMovement());
        }
    }

    public override void OnPlayerLost()
    {
        base.OnPlayerLost();

        if (movementRoutine != null)
        {
            StopCoroutine(movementRoutine);
            movementRoutine = null;
        }

        isRunning = false;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        ChangeEnemyStatus(EnemyStatus.Idle);
    }

}

