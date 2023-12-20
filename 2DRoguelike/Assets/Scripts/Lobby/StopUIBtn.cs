using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopUIBtn : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadLobbyScene()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        SceneManager.LoadScene(0);
    }
}
