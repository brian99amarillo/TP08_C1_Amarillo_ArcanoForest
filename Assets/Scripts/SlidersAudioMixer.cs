using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SlidersAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderBackground;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderUI;
    [SerializeField] private SoundsSO settings;
    private void Awake()
    {
        sliderMaster.onValueChanged.AddListener(OnSliderMasterChanged);
        sliderBackground.onValueChanged.AddListener(OnSliderBackgroundChanged);
        sliderSFX.onValueChanged.AddListener(OnSliderSFXChanged);
        sliderUI.onValueChanged.AddListener(OnSliderUIChanged);
    }

    private void Start()
    {
        sliderMaster.value = settings.Master;
        sliderBackground.value = settings.Background;
        sliderSFX.value = settings.Sfx;
        sliderUI.value = settings.Ui;

        ModifyMixerVolume("VolumeMaster", settings.Master);
        ModifyMixerVolume("VolumeBackground", settings.Background);
        ModifyMixerVolume("VolumeSFX", settings.Sfx);
        ModifyMixerVolume("VolumeUI", settings.Ui);
    }

    private void OnDestroy()
    {
        sliderMaster.onValueChanged.RemoveListener(OnSliderMasterChanged);
        sliderBackground.onValueChanged.RemoveListener(OnSliderBackgroundChanged);
        sliderSFX.onValueChanged.RemoveListener(OnSliderSFXChanged);
        sliderUI.onValueChanged.RemoveListener(OnSliderUIChanged);
    }
    private void OnSliderMasterChanged(float value)
    {
        ModifyMixerVolume("VolumeMaster", value);
    }

    private void OnSliderBackgroundChanged(float value)
    {
        ModifyMixerVolume("VolumeBackground", value);
    }

    private void OnSliderSFXChanged(float value)
    {
        ModifyMixerVolume("VolumeSFX", value);
    }
    private void OnSliderUIChanged(float value)
    {
        ModifyMixerVolume("VolumeUI", value);
    }

    private void ModifyMixerVolume (string parameter, float volume)
    {
        float finalVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        mixer.SetFloat(parameter, finalVolume);

    }
}
