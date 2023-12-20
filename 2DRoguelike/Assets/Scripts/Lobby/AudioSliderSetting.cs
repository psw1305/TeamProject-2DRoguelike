using UnityEngine;
using UnityEngine.UI;

public class AudioSliderSetting : MonoBehaviour
{
    [SerializeField] private Slider audioSettingSlider;

    [SerializeField] private string PlayerPrefsKey;
    private float _playerPrefsValue;

    private void Awake()
    {
        InitSlider();
    }
    private void InitSlider()
    {
        _playerPrefsValue = PlayerPrefs.GetFloat(PlayerPrefsKey);
        audioSettingSlider.value = _playerPrefsValue;
    }

    public void BGMAudioControl(float sound)
    {
        sound = audioSettingSlider.value;
        AudioSystem<BGM>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("BGMSoundValue", sound);
    }

    public void SFXAudioControl(float sound)
    {
        sound = audioSettingSlider.value;
        AudioSystem<SFX>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("SFXSoundValue", sound);
    }
}
