using UnityEngine;

public class PausedState : IGameState
{
    private readonly GameManager manager;

    public PausedState(GameManager manager)
    {
        this.manager = manager;
    }

    public void EnterState()
    {
        Time.timeScale = 0f;

        Main.UI.PlayerUI.GamePauseEnter();
    }

    // 일시정지 상태 업데이트 로직
    public void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            manager.ChangeState(manager.Playing);
        }
    }

    public void ExitState()
    {
        Main.UI.PlayerUI.GamePauseExit();
    }
}
