using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBtn : MonoBehaviour
{
    [SerializeField] GameObject optionUIBundle;
    public void LoadGameScene()
    {
        //로드할 씬 코드
    }
    public void ActiveOptionUI()
    {
        optionUIBundle.SetActive(true);
    }
}
