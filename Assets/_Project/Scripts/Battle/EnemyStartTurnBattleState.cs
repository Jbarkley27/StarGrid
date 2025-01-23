using UnityEngine;

public class EnemyStartTurnBattleState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy Start Turn Battle State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}