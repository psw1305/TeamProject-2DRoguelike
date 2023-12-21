using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopUIBtn : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        Main.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        BGM.Instance.Play(BGM.Instance.bgm, true);
    }
    public void LoadLobbyScene()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.btnClick);
        Main.Clear();
        SceneManager.LoadScene(0);
        BGM.Instance.Play(BGM.Instance.bgm, true);
    }
}
