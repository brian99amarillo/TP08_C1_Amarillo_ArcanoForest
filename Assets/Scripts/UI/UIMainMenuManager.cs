using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroupMainMenu;
    [SerializeField] private CanvasGroup canvasGroupPlay;
    [SerializeField] private CanvasGroup canvasGroupSettings;
    [SerializeField] private CanvasGroup canvasGroupCredits;
    [SerializeField] private CanvasGroup canvasGroupExit;

    [Header("Main Menu")]
    [SerializeField] private Button buttonMainPlay;
    [SerializeField] private Button buttonMainSettings;
    [SerializeField] private Button buttonMainCredits;
    [SerializeField] private Button buttonMainExit;

    [Header("Play")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonPlayBack;

    [Header("Settings")]
    [SerializeField] private Button buttonSettingsBack;

    [Header("Credits")]
    [SerializeField] private Button buttonCreditsBack;

    [Header("Exit")]
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonExitBack;

    private IEnumerator fadeCanvasGroup;
    [SerializeField] private float duration = 1.0f;

    private void Awake()
    {
       buttonMainPlay.onClick.AddListener(OnButtonMainPlayClicked);
       buttonMainSettings.onClick.AddListener(OnButtonMainSettingsClicked);
       buttonMainCredits.onClick.AddListener(OnButtonMainCreditsClicked);
       buttonMainExit.onClick.AddListener(OnButtonMainExitClicked);

       buttonPlayBack.onClick.AddListener(OnButtonPlayBackClicked);
       buttonSettingsBack.onClick.AddListener(OnButtonSettingsBackClicked);
       buttonCreditsBack.onClick.AddListener(OnButtonCreditsBackClicked);    
       buttonExitBack.onClick.AddListener(OnButtonExitBackClicked);


       buttonPlay.onClick.AddListener(OnButtonPlayClicked);
       buttonExit.onClick.AddListener(OnButtonExitClicked);
    }
    private void OnDestroy()
    {
        buttonMainPlay.onClick.RemoveListener(OnButtonMainPlayClicked);
        buttonMainSettings.onClick.RemoveListener(OnButtonMainSettingsClicked);
        buttonMainCredits.onClick.RemoveListener(OnButtonMainCreditsClicked);
        buttonMainExit.onClick.RemoveListener(OnButtonMainExitClicked);

        buttonPlayBack.onClick.RemoveListener(OnButtonPlayBackClicked);
        buttonSettingsBack.onClick.RemoveListener(OnButtonSettingsBackClicked);
        buttonCreditsBack.onClick.RemoveListener(OnButtonCreditsBackClicked);
        buttonExitBack.onClick.RemoveListener(OnButtonExitBackClicked);
        
        
        buttonPlay.onClick.RemoveListener(OnButtonPlayClicked);
        buttonExit.onClick.RemoveListener(OnButtonExitClicked);
    }

    private void Start()
    {
        canvasGroupMainMenu.alpha = 1f;
        canvasGroupMainMenu.interactable = true;
        canvasGroupMainMenu.blocksRaycasts = true;
        AudioManager.Instance.ActiveMusicGameplay();
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup from,CanvasGroup to)
    {
        from.blocksRaycasts = false;

        
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float lerp = time / duration;
            from.alpha = 1- lerp;
            to.alpha = lerp;
            yield return null;
        }

        from.alpha = 0f;
        from.interactable = false;

        to.alpha = 1.0f;
        to.interactable = true;
        to.blocksRaycasts = true;

        fadeCanvasGroup = null;
    }


    private void SwitchCanvasGroup(CanvasGroup from, CanvasGroup to)    
    {
        if (fadeCanvasGroup==null)
        {
            fadeCanvasGroup = FadeCanvasGroup(from, to);
            StartCoroutine(fadeCanvasGroup);
        }
    }

    private void OnButtonMainPlayClicked()
    {
        SwitchCanvasGroup(canvasGroupMainMenu, canvasGroupPlay);
        AudioManager.Instance.ButtonClickeaded();
    }

    private void OnButtonMainSettingsClicked()
    {
        SwitchCanvasGroup(canvasGroupMainMenu, canvasGroupSettings);
        AudioManager.Instance.ButtonClickeaded();
    }


    private void OnButtonMainCreditsClicked()
    {
        SwitchCanvasGroup(canvasGroupMainMenu, canvasGroupCredits);
        AudioManager.Instance.ButtonClickeaded();
    }


    private void OnButtonMainExitClicked()
    {
        SwitchCanvasGroup(canvasGroupMainMenu, canvasGroupExit);
        AudioManager.Instance.ButtonClickeaded();
    }


    private void OnButtonPlayBackClicked()
    {
        SwitchCanvasGroup(canvasGroupPlay, canvasGroupMainMenu);
        AudioManager.Instance.ButtonClickeadedBack();
    }

    private void OnButtonSettingsBackClicked()
    {
        SwitchCanvasGroup(canvasGroupSettings, canvasGroupMainMenu);
        AudioManager.Instance.ButtonClickeadedBack();
    }

    private void OnButtonCreditsBackClicked()
    {
        SwitchCanvasGroup(canvasGroupCredits, canvasGroupMainMenu);
        AudioManager.Instance.ButtonClickeadedBack();
    }


    private void OnButtonExitBackClicked()
    {
        SwitchCanvasGroup(canvasGroupExit, canvasGroupMainMenu);
        AudioManager.Instance.ButtonClickeadedBack();
    }


    private void OnButtonPlayClicked()
    {
        SceneManager.LoadScene("Game");
        AudioManager.Instance.ButtonClickeaded();
    }

    private void OnButtonExitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR && !UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
