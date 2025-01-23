using UnityEngine;
using System.Collections;

public class StartBattleState : BattleState
{
    public override void EnterState()
    {
        if (stateAlreadyEntered) return;

        // Running State for the first time
        stateAlreadyEntered = true;

        // Setup Battle
        StartCoroutine(SetupState());
    }

    public override IEnumerator SetupState()
    {
        // DO ALL TIMED ACTIONS HERE
        Debug.Log("Starting Battle");

        Debug.Log("Drawing Cards");

        yield return new WaitForSeconds(.2f);

        // Change State - Manager will handle calling ExitState
        BattleManager.instance.ChangeState(BattleManager.BattleStepState.PLAYER_START_TURN);
    }

    public override void ExitState()
    {
        stateAlreadyEntered = false;
        Debug.Log("Exiting Start Battle State");
    }
}