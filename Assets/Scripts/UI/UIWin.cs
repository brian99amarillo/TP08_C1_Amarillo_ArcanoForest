using UnityEngine;
using System.Collections;

public class UIWin: Pickable
{
    public static UIWin Instance { get; private set; }
    [SerializeField] private GameObject panelWin;
    [SerializeField] private float delayTime = 0.3f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        panelWin.SetActive(false);
    }
    public void ShowWin()
    {
        StartCoroutine(ShowWinCoroutine());
    }

    private IEnumerator ShowWinCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        panelWin.SetActive(true);
        Time.timeScale = 0f; // pausa el juego
    }

}
