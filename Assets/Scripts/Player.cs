using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
 
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask ground;
    [SerializeField] private SettingsPlayerSO settingsPlayer;
    [SerializeField] private GameObject playerAttack;
    [SerializeField] private float LongLine = 1f;
    [SerializeField] private float attackDuration = 0.4f;
    private Rigidbody2D rb;
    private bool isWalking;
    private bool isGrounded;
    private bool isAttacking;
    private bool hasHit;
    private float attackTimer;
    private float currentDamage;
    private Vector2 starPos;
    public enum PlayerStatus { Idle, Walk, Jump, Attack }
    private PlayerStatus CurrentStatus;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
        starPos = settingsPlayer.StartPosition;
        transform.position = starPos;
        currentDamage = settingsPlayer.Damage;
    }
    private void Update()
    {
        MovementnPlayer();
        AttackingPlayer();
    }

    private void MovementnPlayer()
    {
        if (Time.timeScale == 1)
        {
            isGrounded = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + (LongLine * -1)), ground); // Verifica si el objeto está tocando el suelo

            isWalking = false;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(new Vector2(0, settingsPlayer.JumpForce), ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.A)) // Si la tecla A está presionada el objeto se mueve hacia la izquierda
            {
                rb.linearVelocity = new Vector2(-settingsPlayer.Speed, rb.linearVelocity.y);
                transform.right = Vector2.left;
                isWalking = true;

            }
            else if (Input.GetKey(KeyCode.D)) // Si la tecla D está presionada el objeto se mueve hacia la derecha
            {
                rb.linearVelocity = new Vector2(settingsPlayer.Speed, rb.linearVelocity.y);
                transform.right = Vector2.right;
                isWalking = true;
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            }
            UpdateStatus();
        }
    }
    private void AttackingPlayer()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking) // Si se apreta el click izquierdo del mouse avisa que esta atacando
        {
            isAttacking = true;
            hasHit = false;
            attackTimer = attackDuration;
            playerAttack.SetActive(true);
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

    //maquina de estados

    private void ChangePlayerStatus(PlayerStatus NewStatus)
    {
        if (NewStatus == CurrentStatus) return;
        CurrentStatus = NewStatus;

        switch(CurrentStatus)
        {
            case PlayerStatus.Idle:
                animator.Play("Idle");
                break;
            case PlayerStatus.Walk:
                animator.Play("Walk");
                break;
            case PlayerStatus.Jump:
                animator.Play("Jump");
                break;
            case PlayerStatus.Attack:
                animator.Play("Attack");
                break;
        }
    }

    private void UpdateStatus()
    {
        if (isAttacking)
            ChangePlayerStatus(PlayerStatus.Attack);
        else if (!isGrounded)
           ChangePlayerStatus(PlayerStatus.Jump);
        else if (isWalking)
            ChangePlayerStatus(PlayerStatus.Walk);
        else
            ChangePlayerStatus(PlayerStatus.Idle);

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isAttacking || hasHit) return;

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(currentDamage);
            hasHit = true; 
        }
    }


    //private void Damage(Collider2D collision)
    //{

    //    life -= collision.GetComponent<BulletEnemy>().Damage;
    //    AudioManager.Instance.PlayerDamege();
    //    image.fillAmount = life / settingsPlayer.Life;
    //    if (life <= 0)
    //    {
    //        Die();
    //    }
    //}

    //private void DamageCollision(Collider2D collision)
    //{
    //    Enemy enemy = collision.GetComponent<Enemy>();
    //    AudioManager.Instance.PlayerDamege();
    //    life -= enemy.Settings.Damage;
    //    image.fillAmount = life / settingsPlayer.Life;
    //    if (life <= 0)
    //    {
    //        Die();
    //    }
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        DamageCollision(collision.collider);
    //    }
    //    if (collision.gameObject.CompareTag("Finish"))
    //    {
    //        Winner();
    //    }

    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("EnemyBullet"))
    //    {
    //        if (PowerUpInvisibility.activedInvincibility) return;
    //        Damage(collision);
    //    }
    //}

    //public void Heal(float amount)
    //{
    //    life = Mathf.Min(life + amount, settingsPlayer.Life);
    //    UIHealtPlayer.Instance.UpdateHealthBar(life, settingsPlayer.Life);
    //}
    //private void ReturnToLastGround()
    //{
    //    life -= settingsPlayer.FallDamage;
    //    AudioManager.Instance.PlayerDamege();
    //    image.fillAmount = life / settingsPlayer.Life;

    //    if (life <= 0)
    //    {
    //        Die();
    //        return;
    //    }

    //    rb.linearVelocity = Vector2.zero;
    //    transform.position = lastGroundPosition;
    //}
    //private void Die()
    //{
    //  CanvasGameOver.SetActive(true);
    //  AudioManager.Instance.PauseMusicGameplay();
    //  AudioManager.Instance.GameOver();
    //  Time.timeScale = 0f;
    //}

    //private void Winner()
    //{
    //    UIWinner.Instance.ScoreWinner();
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 point = new Vector2(transform.position.x, transform.position.y - LongLine);
        Gizmos.DrawWireSphere(point, 0.05f);
    }
}
