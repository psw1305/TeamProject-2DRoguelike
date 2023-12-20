using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 사운드 매니저
/// </summary>
public abstract class AudioSystem<T> : MonoBehaviour where T : AudioSystem<T>
{
    #region Singleton
    private static T instance = null;
    public static T Instance
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError(string.Format("허용되지 않은 중복 인스턴스 => {0}", typeof(T)));
            Destroy(this);
            return;
        }

        instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    #endregion

    [SerializeField]
    private AudioMixer audioMixer = null;

    protected abstract void SetVolume(float volumeScale);

    protected void SetVolume(string volumeParameterName, float volumeScale)
    {
        volumeScale = Mathf.Clamp01(volumeScale);

        float min = 0.0001f;    // 0.0001 = -80dB
        float max = 1;          // 1 = 0dB

        // Scale Volume
        float linearValue = Mathf.Lerp(min, max, volumeScale);
        // Convert volume to dB
        float dBValue = 20 * Mathf.Log10(linearValue);

        audioMixer.SetFloat(volumeParameterName, dBValue);
    }
}

