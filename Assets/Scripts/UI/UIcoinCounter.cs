using TMPro;
using UnityEngine;

public class UIcoinCounter : MonoBehaviour
{
    public static UIcoinCounter Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCoinCounter(float value)
    {
        coinText.text = value.ToString();
    }

}
