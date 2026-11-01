using UnityEngine;
using System.Collections;
public class EnemyPiranha : Enemy
{

    [SerializeField] private Animator animator;
    [SerializeField] private float attackTime = 0f;
    [SerializeField] private float idleTime = 0f;
    [SerializeField] private ProyectileEnemy proyectilePrefab;
    [SerializeField] private Transform firePoint;
    private bool isAttacking;
    private Coroutine movementRoutine;
    public enum EnemyStatus { Idle, Attack }
    private EnemyStatus CurrentStatus;

    protected override void Start()
    {
        base.Start();
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
            ChangeEnemyStatus(EnemyStatus.Idle);
            yield return new WaitForSeconds(idleTime);

            //Attack
            isAttacking = true;
            float direction = Mathf.Sign(target.position.x - transform.position.x);
            transform.right = new Vector2(-direction, 0f);
            ChangeEnemyStatus(EnemyStatus.Attack);
            ProyectileEnemy proyectile = Instantiate(proyectilePrefab, firePoint.position, Quaternion.identity);
            proyectile.Launch(new Vector2(direction, 0f));
            yield return new WaitForSeconds(attackTime);
            isAttacking = false;
        }
    }
    protected override void Die()
    {
        // acá van las partículas, el sonido y la animación de muerte del Walker
        base.Die(); // después ejecuta lo del padre (destruir el objeto)
    }

    private void ChangeEnemyStatus(EnemyStatus NewStatus)
    {
        if (NewStatus == CurrentStatus) return;
        CurrentStatus = NewStatus;

        switch (CurrentStatus)
        {
            case EnemyStatus.Idle:
                animator.Play("Idle");
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
        else
            ChangeEnemyStatus(EnemyStatus.Idle);
    }

    

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
        ChangeEnemyStatus(EnemyStatus.Idle);
    }

}

