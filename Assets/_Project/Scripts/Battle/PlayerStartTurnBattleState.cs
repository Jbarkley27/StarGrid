using UnityEngine;

public class PlayerStartTurnBattleState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Player Start Turn Battle State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}