using UnityEngine;

public class BattleWinState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Battle Win State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}