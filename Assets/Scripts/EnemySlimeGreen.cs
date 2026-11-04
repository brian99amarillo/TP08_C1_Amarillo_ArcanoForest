//using UnityEngine;
//using System.Collections;

//public class EnemySlimeGreen : Enemy
//{
//    [SerializeField] private Animator animator;
//    [SerializeField] private float runTime = 0f;
//    [SerializeField] private float idleTime = 0f;
//    [SerializeField] private float deathDuration = 1f;
//    public enum EnemyStatus { Idle, Walk, Attack, Die }
//    private Coroutine movementRoutine;
//    private EnemyStatus CurrentStatus;

//    private IEnumerator RutineMovement()
//    {
//        while (true)
//        {   
//            //Idle
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
//            ChangeEnemyStatus(EnemyStatus.Idle);
//            yield return new WaitForSeconds(idleTime);

//            //Run
//           float  direction = Mathf.Sign(target.position.x - transform.position.x); 
//            transform.right = new Vector2(direction, 0);
//            ChangeEnemyStatus(EnemyStatus.Walk);
//            rb.linearVelocity = new Vector2(direction * settingsEnemy.Speed, rb.linearVelocityY);
//            yield return new WaitForSeconds(runTime);
//        }
//    }

//    private void ChangeEnemyStatus(EnemyStatus NewStatus)
//    {
//        if (NewStatus == CurrentStatus) return;
//        CurrentStatus = NewStatus;

//        switch(CurrentStatus)
//        {
//            case EnemyStatus.Idle:
//                animator.Play("Idle");
//                break;
//            case EnemyStatus.Walk:
//                animator.Play("Walk");
//                break;
//            case EnemyStatus.Attack:
//                animator.Play("Attack");
//                break;
//            case EnemyStatus.Die:
//                animator.Play("Die");
//                break;
//        }
//    }

//    protected override void Die()
//    {
//        if (isDead) return;
//        isDead = true;

//        StopAllCoroutines();     // corta el movimiento y los disparos
//        rb.simulated = false;    // sin física: no daña, no recibe golpes, no activa la zona
//        ChangeEnemyStatus(EnemyStatus.Die);

//        Destroy(gameObject, deathDuration);
//        // acá van las partículas, el sonido y la animación de muerte del Walker
//       // base.Die(); // después ejecuta lo del padre (destruir el objeto)
//    }


//    public override void OnPlayerDetected(Transform player)
//    {
//        if (isDead) return;
//        base.OnPlayerDetected(player);

//        if (movementRoutine == null) 
//        {
//            movementRoutine = StartCoroutine(RutineMovement());
//        }
//    }

//    public override void OnPlayerLost()
//    {
//        if (isDead) return;
//        base.OnPlayerLost();

//        if (movementRoutine != null)
//        {
//            StopCoroutine(movementRoutine);
//            movementRoutine = null;
//        }
//        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//        ChangeEnemyStatus(EnemyStatus.Idle);
//    }

//}

using UnityEngine;
using System.Collections;

public class EnemySlimeGreen : Enemy
{
    [SerializeField] private float idleTime = 1f;
    [SerializeField] private float runTime = 1.5f;

    protected override IEnumerator Behaviour()
    {
        while (true)
        {
            // Idle
            ChangeStatus(EnemyStatus.Idle);
            if (!isKnockedBack)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }
            yield return new WaitForSeconds(idleTime);

            // Run: va hacia el jugador
            float direction = Mathf.Sign(target.position.x - transform.position.x);
            transform.right = new Vector2(direction, 0f);
            ChangeStatus(EnemyStatus.Walk);

            float timer = 0f;
            while (timer < runTime)
            {
                if (!isKnockedBack)
                {
                    rb.linearVelocity = new Vector2(direction * settingsEnemy.Speed, rb.linearVelocity.y);
                }

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
