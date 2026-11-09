//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class UIPauseMenuManager : MonoBehaviour
//{
//    [SerializeField] private CanvasGroup canvasGroupPauseMenu;
//    [SerializeField] private CanvasGroup canvasGroupGame;
//    [SerializeField] private CanvasGroup canvasGroupPlay;
//    [SerializeField] private CanvasGroup canvasGroupSettings;
//    [SerializeField] private CanvasGroup canvasGroupCredits;
//    [SerializeField] private CanvasGroup canvasGroupExit;

//    [Header("Main Menu")]
//    [SerializeField] private Button buttonContinue;
//    [SerializeField] private Button buttonSettings;
//    [SerializeField] private Button buttonCredits;
//    [SerializeField] private Button buttonExit;

//    [Header("Continue")]
//    [SerializeField] private Button buttonContinueGame;
//    [SerializeField] private Button buttonBack;

//    [Header("Settings")]
//    [SerializeField] private Button buttonSettingsBack;

//    [Header("Credits")]
//    [SerializeField] private Button buttonCreditsBack;

//    [Header("Exit")]
//    [SerializeField] private Button buttonExitGame;
//    [SerializeField] private Button buttonExitBack;

//    private IEnumerator fadeCanvasGroup;
//    [SerializeField] private float duration = 1.0f;

//    private void Awake()
//    {
//        buttonContinue.onClick.AddListener(OnButtonMainContinueClicked);
//        buttonSettings.onClick.AddListener(OnButtonMainSettingsClicked);
//        buttonCredits.onClick.AddListener(OnButtonMainCreditsClicked);
//        buttonExit.onClick.AddListener(OnButtonMainExitClicked);

//        buttonBack.onClick.AddListener(OnButtonPlayBackClicked);
//        buttonSettingsBack.onClick.AddListener(OnButtonSettingsBackClicked);
//        buttonCreditsBack.onClick.AddListener(OnButtonCreditsBackClicked);
//        buttonExitBack.onClick.AddListener(OnButtonExitBackClicked);


//        buttonContinue.onClick.AddListener(OnButtonContinueClicked);
//        buttonExit.onClick.AddListener(OnButtonExitClicked);
//    }
//    private void OnDestroy()
//    {
//        buttonContinue.onClick.RemoveListener(OnButtonMainContinueClicked);
//        buttonSettings.onClick.RemoveListener(OnButtonMainSettingsClicked);
//        buttonCredits.onClick.RemoveListener(OnButtonMainCreditsClicked);
//        buttonExit.onClick.RemoveListener(OnButtonMainExitClicked);

//        buttonBack.onClick.RemoveListener(OnButtonPlayBackClicked);
//        buttonSettingsBack.onClick.RemoveListener(OnButtonSettingsBackClicked);
//        buttonCreditsBack.onClick.RemoveListener(OnButtonCreditsBackClicked);
//        buttonExitBack.onClick.RemoveListener(OnButtonExitBackClicked);


//        buttonContinue.onClick.RemoveListener(OnButtonContinueClicked);
//        buttonExit.onClick.RemoveListener(OnButtonExitClicked);
//    }

//    private void Start()
//    {
//        canvasGroupPauseMenu.alpha = 0f;
//        canvasGroupPauseMenu.interactable = false;
//        canvasGroupPauseMenu.blocksRaycasts = false;
//        //AudioManager.Instance.ActiveMusicGameplay();
//    }

//    private IEnumerator FadeCanvasGroup(CanvasGroup from, CanvasGroup to)
//    {
//        from.blocksRaycasts = false;


//        float time = 0;

//        while (time < duration)
//        {
//            time += Time.deltaTime;
//            float lerp = time / duration;
//            from.alpha = 1 - lerp;
//            to.alpha = lerp;
//            yield return null;
//        }

//        from.alpha = 0f;
//        from.interactable = false;

//        to.alpha = 1.0f;
//        to.interactable = true;
//        to.blocksRaycasts = true;

//        fadeCanvasGroup = null;
//    }


//    private void Update()
//    {
//        if ((Input.GetKeyDown(KeyCode.Escape)) || Input.GetKeyDown(KeyCode.P))  // Pausa el juego al presionar la tecla Escape o P, y abre el menu de pausa
//        {
//            IsPauseActive();
//        }
//        else
//        {
//            IsPauseInactive();
//        }
//    }

//    private void IsPauseActive()
//    {
//        SwitchCanvasGroup(canvasGroupGame, canvasGroupPauseMenu);
//        Time.timeScale = 0f; // Detiene el tiempo
//        AudioManager.Instance.ActiveMusicMenuPause();
//    }

//    private void IsPauseInactive()
//    {
//        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupGame);
//        Time.timeScale = 1f; // Reanuda el tiempo
//        AudioManager.Instance.ActiveMusicGameplay();
//    }

//    private void SwitchCanvasGroup(CanvasGroup from, CanvasGroup to)
//    {
//        if (fadeCanvasGroup == null)
//        {
//            fadeCanvasGroup = FadeCanvasGroup(from, to);
//            StartCoroutine(fadeCanvasGroup);
//        }
//    }

//    private void OnButtonMainContinueClicked()
//    {
//        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupPlay);
//        AudioManager.Instance.ButtonClickeaded();
//    }

//    private void OnButtonMainSettingsClicked()
//    {
//        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupSettings);
//        AudioManager.Instance.ButtonClickeaded();
//    }


//    private void OnButtonMainCreditsClicked()
//    {
//        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupCredits);
//        AudioManager.Instance.ButtonClickeaded();
//    }


//    private void OnButtonMainExitClicked()
//    {
//        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupExit);
//        AudioManager.Instance.ButtonClickeaded();
//    }


//    private void OnButtonPlayBackClicked()
//    {
//        SwitchCanvasGroup(canvasGroupPlay, canvasGroupPauseMenu);
//        AudioManager.Instance.ButtonClickeadedBack();
//    }

//    private void OnButtonSettingsBackClicked()
//    {
//        SwitchCanvasGroup(canvasGroupSettings, canvasGroupPauseMenu);
//        AudioManager.Instance.ButtonClickeadedBack();
//    }

//    private void OnButtonCreditsBackClicked()
//    {
//        SwitchCanvasGroup(canvasGroupCredits, canvasGroupPauseMenu);
//        AudioManager.Instance.ButtonClickeadedBack();
//    }


//    private void OnButtonExitBackClicked()
//    {
//        SwitchCanvasGroup(canvasGroupExit, canvasGroupPauseMenu);
//        AudioManager.Instance.ButtonClickeadedBack();
//    }


//    private void OnButtonContinueClicked()
//    {
//        SceneManager.LoadScene("Game");
//        AudioManager.Instance.ButtonClickeaded();
//    }

//    private void OnButtonExitClicked()
//    {
//        Application.Quit();
//#if UNITY_EDITOR && !UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
//}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupPauseMenu;
    [SerializeField] private CanvasGroup canvasGroupGame;
    [SerializeField] private CanvasGroup canvasGroupSettings;
    [SerializeField] private CanvasGroup canvasGroupCredits;
    [SerializeField] private CanvasGroup canvasGroupExit;

    [Header("Pause Menu")]
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonSettings;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExit;

    [Header("Settings")]
    [SerializeField] private Button buttonSettingsBack;

    [Header("Credits")]
    [SerializeField] private Button buttonCreditsBack;

    [Header("Exit")]
    [SerializeField] private Button buttonExitGame;
    [SerializeField] private Button buttonExitBack;
    [SerializeField] private Button buttonRetryGame;
    [SerializeField] private Button buttonExitToMainMenu;

    [SerializeField] private float duration = 0.2f;

    private Coroutine fadeRoutine;
    private CanvasGroup currentGroup; // el panel que se está mostrando ahora
    private bool isPaused;
    private bool canPause = true;

    private void Awake()
    {
        buttonContinue.onClick.AddListener(OnContinueClicked);
        buttonSettings.onClick.AddListener(OnButtonSettingsClicked);
        buttonCredits.onClick.AddListener(OnButtonCreditsClicked);
        buttonExit.onClick.AddListener(OnButtonExitClicked);

        buttonSettingsBack.onClick.AddListener(OnButtonBackClicked);
        buttonCreditsBack.onClick.AddListener(OnButtonBackClicked);
        buttonExitBack.onClick.AddListener(OnButtonBackClicked);

        buttonExitGame.onClick.AddListener(OnButtonExitGameClicked);
        buttonRetryGame.onClick.AddListener(OnButtonRetryGameClicked);
        buttonExitToMainMenu.onClick.AddListener(OnButtonExitGameClicked);
    }

    private void OnDestroy()
    {
        buttonContinue.onClick.RemoveListener(OnContinueClicked);
        buttonSettings.onClick.RemoveListener(OnButtonSettingsClicked);
        buttonCredits.onClick.RemoveListener(OnButtonCreditsClicked);
        buttonExit.onClick.RemoveListener(OnButtonExitClicked);

        buttonSettingsBack.onClick.RemoveListener(OnButtonBackClicked);
        buttonCreditsBack.onClick.RemoveListener(OnButtonBackClicked);
        buttonExitBack.onClick.RemoveListener(OnButtonBackClicked);

        buttonExitGame.onClick.RemoveListener(OnButtonExitGameClicked);
        buttonRetryGame.onClick.RemoveListener(OnButtonRetryGameClicked);
        buttonExitToMainMenu.onClick.RemoveListener(OnButtonExitGameClicked);
    }

    // Si el jugador muere, ya no se puede pausar 
    private void OnEnable() { HealthPlayer.OnPlayerDied += DisablePause; }
    private void OnDisable() { HealthPlayer.OnPlayerDied -= DisablePause; }

    private void DisablePause()
    {
        canPause = false;
    }

    private void Start()
    {
        Time.timeScale = 1f; 

        currentGroup = canvasGroupGame;
        canvasGroupPauseMenu.alpha = 0f;
        canvasGroupPauseMenu.interactable = false;
        canvasGroupPauseMenu.blocksRaycasts = false;
    }

    private void Update()
    {
        if (!canPause || fadeRoutine != null) return; // no pausa si murió o si hay una transición en curso

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                OnContinueClicked();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // detiene el juego
        SwitchCanvasGroup(canvasGroupGame, canvasGroupPauseMenu);
        AudioManager.Instance.PauseMusicGameplay();
        AudioManager.Instance.ActiveMusicMenuPause();
    }

    private void OnContinueClicked()
    {
        if (fadeRoutine != null) return;

        isPaused = false;
        Time.timeScale = 1f; // reanuda el juego
        SwitchCanvasGroup(currentGroup, canvasGroupGame); // oculta el panel que esté abierto
        AudioManager.Instance.StopMusicMenuPause();
        AudioManager.Instance.ActiveMusicGameplay();
    }

    private void SwitchCanvasGroup(CanvasGroup from, CanvasGroup to)
    {
        if (fadeRoutine == null)
        {
            fadeRoutine = StartCoroutine(FadeCanvasGroup(from, to));
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup from, CanvasGroup to)
    {
        from.interactable = false;
        from.blocksRaycasts = false;

        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // con timeScale = 0, deltaTime vale 0 y el fade no avanzaría
            float lerp = Mathf.Clamp01(time / duration);
            from.alpha = 1f - lerp;
            to.alpha = lerp;
            yield return null;
        }

        from.alpha = 0f;

        to.alpha = 1f;
        to.interactable = true;
        to.blocksRaycasts = true;

        currentGroup = to;
        fadeRoutine = null;
    }

    // ---------- Botones ----------
    private void OnButtonSettingsClicked()
    {
        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupSettings);
        AudioManager.Instance.ButtonClickeaded();
    }

    private void OnButtonCreditsClicked()
    {
        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupCredits);
        AudioManager.Instance.ButtonClickeaded();
    }

    private void OnButtonExitClicked()
    {
        SwitchCanvasGroup(canvasGroupPauseMenu, canvasGroupExit);
        AudioManager.Instance.ButtonClickeaded();
    }

    private void OnButtonBackClicked()
    {
        SwitchCanvasGroup(currentGroup, canvasGroupPauseMenu); // vuelve al menú de pausa desde cualquier subpanel
        AudioManager.Instance.ButtonClickeadedBack();
    }

    private void OnButtonExitGameClicked()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }
    private void OnButtonRetryGameClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

}
