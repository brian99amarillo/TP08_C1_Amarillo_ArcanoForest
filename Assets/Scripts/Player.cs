using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask ground;
    [SerializeField] private SettingsPlayerSO settingsPlayer;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private float LongLine = 1f;
    private Rigidbody2D rb;
    private bool isWalking;
    private bool isGrounded;
    private bool isDead;
    private float counterJump;
    private float fallLimitY = -4f;

    private void OnEnable() { HealthPlayer.OnPlayerDied += OnDeath; }
    private void OnDisable() { HealthPlayer.OnPlayerDied -= OnDeath; }

    private Vector2 starPos;

    public enum PlayerStatus { Idle, Walk, Jump, Attack, Die }

    private PlayerStatus CurrentStatus;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        starPos = settingsPlayer.StartPosition;
        transform.position = starPos;
    }

    private void Update()
    {   if (isDead) return;
        MovementnPlayer();
    }

    private void MovementnPlayer()
    {
        if (Time.timeScale == 1)
        {
            isGrounded = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + (LongLine * -1)), ground); // Verifica si el objeto está tocando el suelo
            
            if (transform.position.y < fallLimitY)
            {
                transform.position = starPos;
                rb.linearVelocity = Vector2.zero;
            }

            isWalking = false;
            // Reiniciar los saltos solo cuando está en el suelo y está descendiendo
            if (isGrounded && rb.linearVelocity.y <= 0f)
            {
               counterJump = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space) && counterJump < 2)
            {
                counterJump++;

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(new Vector2(0f, settingsPlayer.JumpForce),ForceMode2D.Impulse);
                AudioManager.Instance.PlayerJump(); // Reproduce el sonido de salto
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
   
    //maquina de estados

    public void ChangePlayerStatus(PlayerStatus NewStatus)
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
            case PlayerStatus.Die:
                animator.Play("Die");
                break;
        }
    }

    private void UpdateStatus()
    {
        if (playerAttack.isAttacking)
            ChangePlayerStatus(PlayerStatus.Attack);
        else if (!isGrounded)
            ChangePlayerStatus(PlayerStatus.Jump);
        else if (isWalking)
            ChangePlayerStatus(PlayerStatus.Walk);
        else if (isDead)
            ChangePlayerStatus(PlayerStatus.Die);
        else
            ChangePlayerStatus(PlayerStatus.Idle);

    }

    private void OnDeath()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        ChangePlayerStatus(PlayerStatus.Die);
    }
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 point = new Vector2(transform.position.x, transform.position.y - LongLine);
        Gizmos.DrawWireSphere(point, 0.05f);
    }
}
