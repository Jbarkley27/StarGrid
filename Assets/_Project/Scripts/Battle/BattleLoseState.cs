using UnityEngine;  


public class BattleLoseState : BattleState
{
    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Battle Lose State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}