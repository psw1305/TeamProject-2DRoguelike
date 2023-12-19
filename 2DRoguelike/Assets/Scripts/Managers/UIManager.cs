using UnityEngine;

public class UIManager 
{
    #region Properties

    public UI_Minimap Minimap { get; private set; }

    #endregion

    #region Init

    public void Initialize()
    {
        var minimapPrefab = GameObject.Instantiate(Main.Resource.GetObject("UI_Minimap"));
        Minimap = minimapPrefab.GetComponent<UI_Minimap>();
    }

    #endregion
}
