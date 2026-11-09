using System.Collections;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private float delayTime = 1.5f;
    private void OnEnable() { HealthPlayer.OnPlayerDied += OnPlayerDied; }
    private void OnDisable() { HealthPlayer.OnPlayerDied -= OnPlayerDied; }

    private void Start()
    {
        panelGameOver.SetActive(false);
    }

    private void OnPlayerDied()
    {
        StartCoroutine(ShowGameOver());
    }
    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(delayTime);
        panelGameOver.SetActive(true);
        Time.timeScale = 0f; // pausa el juego
    }
}