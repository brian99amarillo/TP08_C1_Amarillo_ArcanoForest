using UnityEngine;
using System.Collections;

public class EnemyPiranha : Enemy
{
    [SerializeField] private float idleTime = 1f;
    [SerializeField] private float attackTime = 1f;
    [SerializeField] private ProyectileEnemy proyectilePrefab; // prefab 
    [SerializeField] private Transform firePoint;

    protected override IEnumerator Behaviour()
    {
        while (true)
        {
            // Idle
            ChangeStatus(EnemyStatus.Idle);
            yield return new WaitForSeconds(idleTime);

            // Attack: dispara hacia el jugador
            float direction = Mathf.Sign(target.position.x - transform.position.x);
            transform.right = new Vector2(-direction, 0f);

            ChangeStatus(EnemyStatus.Attack);
            ProyectileEnemy proyectile = Instantiate(proyectilePrefab, firePoint.position, Quaternion.identity);
            proyectile.Launch(new Vector2(direction, 0f));
            AudioManager.Instance.EnemyShoot(); // Reproduce el sonido de ataque del enemigo
            yield return new WaitForSeconds(attackTime);
        }
    }
}