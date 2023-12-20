using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyBtn : MonoBehaviour
{
    [SerializeField] GameObject optionUIBundle;
    public void LoadGameScene()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene("test");
    }
    public void ActiveOptionUI()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        optionUIBundle.SetActive(true);
    }
}
