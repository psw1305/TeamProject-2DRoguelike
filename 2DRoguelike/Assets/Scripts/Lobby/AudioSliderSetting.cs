using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderSetting : MonoBehaviour
{
    [SerializeField] Slider audioSettingSlider;

    public void BGMAudioControl(float sound)
    {
        sound = audioSettingSlider.value;
        //AudioSystem<BGM>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("SFXSoundValue", sound);
    }

    public void SFXAudioControl(float sound)
    {
        sound = audioSettingSlider.value;
        //AudioSystem<SFX>.Instance.VolumeScale = sound;
        PlayerPrefs.SetFloat("BGMSoundValue", sound);
    }
}
