using UnityEngine;

public class PlayerEndTurnBattleState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Player End Turn Battle State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}