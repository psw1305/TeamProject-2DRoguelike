using UnityEngine;
/// <summary>
/// UI 매니저
/// </summary>
public class UIManager 
{
    #region Properties

    public UI_Minimap Minimap { get; private set; }

    #endregion

    #region Init

    public void MinimapGenerate()
    {
        var minimapPrefab = GameObject.Instantiate(Main.Resource.GetObject("UI_Minimap"));
        Minimap = minimapPrefab.GetComponent<UI_Minimap>();
        Minimap.CreatMinimap();
    }

    #endregion
}
