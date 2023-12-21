using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Lobby : MonoBehaviour
{
    #region Field

    [Header("Popup")]
    [SerializeField] private GameObject optionPopup;

    [Header("Buttons")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button returnBtn;

    /// <summary>
    /// 슬라이더를 넣어서 UI_Lobby 관리 가능
    /// </summary>

    #endregion

    #region Unity Flow

    private void Start()
    {
        startBtn.onClick.AddListener(OnStartGame);
        optionBtn.onClick.AddListener(OnActiveOption);
        returnBtn.onClick.AddListener(ReturnLobby);

        InitAudio();
    }

    #endregion

    #region Init

    private void InitAudio()
    {
        if (!PlayerPrefs.HasKey("BGMSoundValue"))
        {
            PlayerPrefs.SetFloat("BGMSoundValue", 1f);
        }
        if (!PlayerPrefs.HasKey("SFXSoundValue"))
        {
            PlayerPrefs.SetFloat("SFXSoundValue", 1f);
        }

        var bgmVolume = PlayerPrefs.GetFloat("BGMSoundValue");
        BGM.Instance.VolumeScale = bgmVolume;

        var sfxVolume = PlayerPrefs.GetFloat("SFXSoundValue");
        SFX.Instance.VolumeScale = sfxVolume;
    }

    #endregion

    #region OnClick

    // 게임 시작
    private void OnStartGame()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene("Game");
    }

    // 옵션 활성화
    private void OnActiveOption()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        optionPopup.SetActive(true);
    }

    // 옵션 => 로비로 돌아가기
    public void ReturnLobby()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        optionPopup.SetActive(false);
    }

    #endregion
}
