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
