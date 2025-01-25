using UnityEngine;
using System.Collections;

public class SetupBattleState : BattleState
{
    public override void EnterState()
    {
        if (stateAlreadyEntered)return;

        // Running State for the first time
        stateAlreadyEntered = true;

        // Setup Battle
        StartCoroutine(SetupState());
    }

    public override IEnumerator SetupState()
    {
        // DO ALL TIMED ACTIONS HERE
        Debug.Log("Setting Up Battle");
        Debug.Log("Creating Grid");

        GridManager.instance.CreateGrid();
        SpawnManager.instance.SpawnPlayer();
        SpawnManager.instance.SpawnEnemy();

        yield return new WaitForSeconds(.2f);

        // Change State - Manager will handle calling ExitState
        BattleManager.instance.ChangeState(BattleManager.BattleStepState.START);
    }



    public override void ExitState()
    {
        stateAlreadyEntered = false;
        Debug.Log("Exiting Setup Battle State");
    }
}