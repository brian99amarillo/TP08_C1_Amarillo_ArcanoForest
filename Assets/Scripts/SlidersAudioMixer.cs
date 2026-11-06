using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SlidersAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private Slider sliderUi;
    [SerializeField] private SoundsSO settings;
    private void Awake()
    {
        sliderMaster.onValueChanged.AddListener(OnSliderMasterChanged);
        sliderMusic.onValueChanged.AddListener(OnSliderMusicChanged);
        sliderSfx.onValueChanged.AddListener(OnSliderSfxChanged);
        sliderUi.onValueChanged.AddListener(OnSliderUiChanged);
    }

    private void Start()
    {
        sliderMaster.value = settings.Master;
        sliderMusic.value = settings.Music;
        sliderSfx.value = settings.Sfx;
        sliderUi.value = settings.Ui;

        ModifyMixerVolume("VolumeMaster", settings.Master);
        ModifyMixerVolume("VolumeMusic", settings.Music);
        ModifyMixerVolume("VolumeSfx", settings.Sfx);
        ModifyMixerVolume("VolumeUi", settings.Ui);
    }

    private void OnDestroy()
    {
        sliderMaster.onValueChanged.RemoveListener(OnSliderMasterChanged);
        sliderMusic.onValueChanged.RemoveListener(OnSliderMusicChanged);
        sliderSfx.onValueChanged.RemoveListener(OnSliderSfxChanged);
        sliderUi.onValueChanged.RemoveListener(OnSliderUiChanged);
    }
    private void OnSliderMasterChanged(float value)
    {
        ModifyMixerVolume("VolumeMaster", value);
    }

    private void OnSliderMusicChanged(float value)
    {
        ModifyMixerVolume("VolumeMusic", value);
    }

    private void OnSliderSfxChanged(float value)
    {
        ModifyMixerVolume("VolumeSfx", value);
    }
    private void OnSliderUiChanged(float value)
    {
        ModifyMixerVolume("VolumeUi", value);
    }

    private void ModifyMixerVolume (string parameter, float volume)
    {
        float finalVolume = Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f);
        mixer.SetFloat(parameter, finalVolume);
        AudioManager.Instance.SliderClickeaded();

    }
}
