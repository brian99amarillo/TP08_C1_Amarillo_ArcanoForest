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
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            
            yield return new WaitForSeconds(idleTime);

            // Run: va hacia el jugador
            float direction = Mathf.Sign(target.position.x - transform.position.x);
            transform.right = new Vector2(direction, 0f);
            ChangeStatus(EnemyStatus.Walk);
            AudioManager.Instance.EnemyDamage(); // Reproduce el sonido de ataque del enemigo

            float timer = 0f;
            while (timer < runTime)
            {
                rb.linearVelocity = new Vector2(direction * settingsEnemy.Speed, rb.linearVelocity.y);
                timer += Time.deltaTime;
                yield return null;
            }
            
        }
    }
}
