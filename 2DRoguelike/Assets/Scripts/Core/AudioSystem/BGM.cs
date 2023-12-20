using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : AudioSystem<BGM>
{
    #region Set BGM

    public AudioSource AudioSource { get; set; }
    private float volumeScale = 1.0f;
    public float VolumeScale
    {
        get
        {
            return this.volumeScale;
        }
        set
        {
            this.volumeScale = Mathf.Clamp01(value);
            SetVolume(this.volumeScale);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        this.AudioSource = GetComponent<AudioSource>();
    }

    protected override void SetVolume(float volumeScale)
    {
        SetVolume("BGM", volumeScale);
    }

    #endregion

    [Header("Game")]
    public AudioClip bgm;

    private void Start()
    {
        Play(bgm, true);
    }

    public void Play(AudioClip clip, bool isLoop)
    {
        this.AudioSource.loop = isLoop;
        this.AudioSource.clip = clip;
        this.AudioSource.Play();       
    }
}

