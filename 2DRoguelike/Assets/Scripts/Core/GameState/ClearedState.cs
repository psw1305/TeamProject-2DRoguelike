using UnityEngine;

public class ClearedState : IGameState
{
    private readonly GameManager manager;

    public ClearedState(GameManager manager)
    {
        this.manager = manager;
    }

    public void EnterState()
    {
        Time.timeScale = 0f;

        Main.UI.PlayerUI.GameClear();
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
    }
}
