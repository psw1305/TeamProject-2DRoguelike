using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFX : AudioSystem<SFX>
{
    #region Set SFX

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
        SetVolume("SFX", volumeScale);
    }

    #endregion

    [Header("UI")]
    public AudioClip btnClick;

    [Header("Item")]
    public AudioClip pickupHeart;
    public AudioClip pickupCoin;
    public AudioClip pickupKey;
    public AudioClip pickupBomb;
    public AudioClip openChest;
    public AudioClip getPassiveItem;

    [Header("Player")]
    public AudioClip playerAtk;
    public AudioClip playerHit;
    public AudioClip useKey;
    public AudioClip useBomb;
    public AudioClip explodeBomb;

    [Header("Enemy")]
    public AudioClip enemyAtk;
    public AudioClip bossIntro;

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f)
    {
        this.AudioSource.PlayOneShot(clip, volumeScale);
    }
}