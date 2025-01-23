using UnityEngine;

public class EnemyEndTurnBattleState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy End Turn Battle State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}