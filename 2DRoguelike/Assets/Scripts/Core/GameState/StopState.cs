using UnityEngine;

/// <summary>
/// 플레이어 사망시 상태
/// </summary>
public class StopState : IGameState
{
    private readonly GameManager manager;

    public StopState(GameManager manager)
    {
        this.manager = manager;
    }

    public void EnterState()
    {
        Time.timeScale = 0f;

        Main.UI.PlayerUI.GameOver();
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
    }
}
