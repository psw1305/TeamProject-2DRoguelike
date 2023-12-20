using UnityEngine;

public class PlayingState : IGameState
{
    private readonly GameManager manager;

    public PlayingState(GameManager manager)
    {
        this.manager = manager;
    }

    public void EnterState()
    {
        Time.timeScale = 1f;
    }

    public void UpdateState()
    {
        // 게임 중 업데이트 로직
        if (Input.GetKeyDown(KeyCode.P))
        {
            manager.ChangeState(manager.Paused);
        }
    }

    public void ExitState()
    {
    }
}
