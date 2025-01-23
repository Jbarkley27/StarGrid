using System.Collections;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    public BattleManager.BattleStepState battleState;
    public bool stateAlreadyEntered = false;


    public virtual void EnterState()
    {
        if (stateAlreadyEntered) return;
        stateAlreadyEntered = true;
    }

    public virtual IEnumerator SetupState()
    {
        yield return null;
    }


    public virtual void ExitState()
    {
        stateAlreadyEntered = false;
    }
}