using UnityEngine;

public class UIManager 
{
    #region Properties

    public UI_Player PlayerUI { get; private set; }
    public UI_Minimap Minimap { get; private set; }

    #endregion

    #region Init

    public void Initialize(Transform parent)
    {
        var playerUI = GameObject.Instantiate(Main.Resource.GetObject("UI_Player"), parent);
        PlayerUI = playerUI.GetComponent<UI_Player>();

        var minimapPrefab = GameObject.Instantiate(Main.Resource.GetObject("UI_Minimap"), parent);
        Minimap = minimapPrefab.GetComponent<UI_Minimap>();
    }

    #endregion
}
