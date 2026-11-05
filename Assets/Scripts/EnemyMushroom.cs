//using UnityEngine;
//using System.Collections;
//public class EnemyMushroom : Enemy
//{
//    [SerializeField] private Animator animator;
//    [SerializeField] private float attackTime = 0f;
//    [SerializeField] private float idleTime = 0f;
//    private bool isAttacking;

//    protected override void Start()
//    {
//        base.Start(); 

//    }

//    private void Update()
//    {
//        UpdateStatus();
//    }
//    // rutina del enemigo
//    private IEnumerator RutineMovement()
//    {
//        while (true)
//        {
//            //Idle
//            ChangeEnemyStatus(EnemyStatus.Idle);
//            yield return new WaitForSeconds(idleTime);

//            //Attack
//            isAttacking = true;
//            ChangeEnemyStatus(EnemyStatus.Attack);
//            yield return new WaitForSeconds(attackTime);
//            isAttacking = false;
//        }
//    }

//    protected override void Die()
//    {
//        // acá van las partículas, el sonido y la animación de muerte del Walker
//        base.Die(); // después ejecuta lo del padre (destruir el objeto)
//    }

//    public enum EnemyStatus { Idle, Walk, Attack }
//    private EnemyStatus CurrentStatus;
//    private void ChangeEnemyStatus(EnemyStatus NewStatus)
//    {
//        if (NewStatus == CurrentStatus) return;
//        CurrentStatus = NewStatus;

//        switch (CurrentStatus)
//        {
//            case EnemyStatus.Idle:
//                animator.Play("Idle");
//                break;
//            case EnemyStatus.Attack:
//                animator.Play("Attack");
//                break;
//        }
//    }

//    private void UpdateStatus()
//    {
//        if (isAttacking)
//            ChangeEnemyStatus(EnemyStatus.Attack);
//        else
//            ChangeEnemyStatus(EnemyStatus.Idle);

//    }

//    private Coroutine movementRoutine;

//    public override void OnPlayerDetected(Transform player)
//    {
//        base.OnPlayerDetected(player);

//        if (movementRoutine == null)
//        {
//            movementRoutine = StartCoroutine(RutineMovement());
//        }
//    }

//    public override void OnPlayerLost()
//    {
//        base.OnPlayerLost();

//        if (movementRoutine != null)
//        {
//            StopCoroutine(movementRoutine);
//            movementRoutine = null;
//        }
//        ChangeEnemyStatus(EnemyStatus.Idle);
//    }
//}
using UnityEngine;
using System.Collections;

public class EnemyMushroom : Enemy
{
    [SerializeField] private float idleTime = 1f;
    [SerializeField] private float attackTime = 1f;

    protected override IEnumerator Behaviour()
    {
        while (true)
        {
            // Idle
            ChangeStatus(EnemyStatus.Idle);
            yield return new WaitForSeconds(idleTime);

            // Attack
            ChangeStatus(EnemyStatus.Attack);
            AudioManager.Instance.EnemyHit(); // Reproduce el sonido de ataque del enemigo
            yield return new WaitForSeconds(attackTime);
        }
    }
}