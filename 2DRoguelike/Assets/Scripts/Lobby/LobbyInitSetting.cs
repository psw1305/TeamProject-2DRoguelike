using UnityEngine;
using UnityEngine.UI;

public class LobbyInitSetting : MonoBehaviour
{
    #region Field
    private float _bgmVol;
    private float _sfxVol;

    #endregion

    #region UnityFlow
    private void Start()
    {
        CheckAudioSetting();
        SetAudioVolume();
    }
    #endregion

    #region Method
    private void CheckAudioSetting()
    {
        if (!PlayerPrefs.HasKey("BGMSoundValue"))
        {
            PlayerPrefs.SetFloat("BGMSoundValue", 1f);
        }
        if (!PlayerPrefs.HasKey("SFXSoundValue"))
        {
            PlayerPrefs.SetFloat("SFXSoundValue", 1f);
        }
    }
    private void SetAudioVolume()
    {  
        _bgmVol = PlayerPrefs.GetFloat("BGMSoundValue");
        AudioSystem<BGM>.Instance.VolumeScale = _bgmVol;

        _sfxVol = PlayerPrefs.GetFloat("SFXSoundValue");
        AudioSystem<SFX>.Instance.VolumeScale = _sfxVol;
    }
    #endregion
}
