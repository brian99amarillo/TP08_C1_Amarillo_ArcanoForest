using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource ui;

    [Header("Audio Player")]
    [SerializeField] private AudioClip playerAttack;
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private AudioClip playerWalk;

    [Header("Audio Pickables")]
    [SerializeField] private AudioClip PickableHealth;
    [SerializeField] private AudioClip PickableInvincibility;
    [SerializeField] private AudioClip PickableDamage;

    [Header("Audio Enemies")]
    [SerializeField] private AudioClip enemyHit; // Hongo
    [SerializeField] private AudioClip enemyShoot;  // piraña
    [SerializeField] private AudioClip enemyDamage; // Slime
    [SerializeField] private AudioClip enemyDie;

    [Header("Audio UI")]
    [SerializeField] private AudioClip buttonClickeaded;
    [SerializeField] private AudioClip buttonClickeadedBack;
    [SerializeField] private AudioClip sliderClickeaded;

    [Header("Audio Music")]
    [SerializeField] private AudioClip musicGameplay;
    [SerializeField] private AudioClip musicPause;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip musicWin;

    private void Awake()
    {
        Instance = this;
        music.loop = true; // la música se repite sola
    }

    private void Start()
    {
        ActiveMusicGameplay();
    }

    public void PlayerAttack() => sfx.PlayOneShot(playerAttack);
    public void PlayerJump() => sfx.PlayOneShot(playerJump);
    public void PlayerWalk() => sfx.PlayOneShot(playerWalk);


    public void PickHealth() => sfx.PlayOneShot(PickableHealth);
    public void PickInvincibility() => sfx.PlayOneShot(PickableInvincibility);
    public void PickDamage() => sfx.PlayOneShot(PickableDamage);

    public void EnemyHit() => sfx.PlayOneShot(enemyHit);
    public void EnemyShoot() => sfx.PlayOneShot(enemyShoot);
    public void EnemyDamage() => sfx.PlayOneShot(enemyDamage);
    public void EnemyDie() => sfx.PlayOneShot(enemyDie);


    public void ButtonClickeaded() => ui.PlayOneShot(buttonClickeaded);
    public void ButtonClickeadedBack() => ui.PlayOneShot(buttonClickeadedBack);

    public void SliderClickeaded()
    {
        if (ui.isPlaying && ui.clip == sliderClickeaded) return; // al arrastrar el slider no se reinicia en cada frame

        ui.clip = sliderClickeaded;
        ui.Play();
    }


    public void ActiveMusicGameplay()
    {
        music.clip = musicGameplay;
        music.Play();
    }
    public void ActiveMusicMenuPause()
    {
        music.clip = musicPause;
        music.Play();
    }

    public void PauseMusicGameplay()
    {
        music.Pause();
    }

    public void StopMusicMenuPause()
    {
        music.Stop();
    }
    public void GameOver() => ui.PlayOneShot(gameOver);

    public void ActiveMusicWin()
    {
        music.Stop();
        ui.PlayOneShot(musicWin);
    }
}