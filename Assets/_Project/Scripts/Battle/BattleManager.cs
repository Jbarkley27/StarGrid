using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum BattleStepState { SETUP, START, PLAYER_START_TURN, PLAYER_END_TURN, ENEMY_START_TURN, ENEMY_END_TURN, WIN, LOSE };


    public static BattleManager instance;

    [Header("States")]
    public SetupBattleState setupState;
    public StartBattleState startState;
    public PlayerStartTurnBattleState playerStartTurnState;
    public PlayerEndTurnBattleState playerEndTurnState;
    public EnemyStartTurnBattleState enemyStartTurnState;
    public EnemyEndTurnBattleState enemyEndTurnState;
    public BattleWinState winState;
    public BattleLoseState loseState;


    public BattleStepState battleState;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Battle Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        battleState = BattleStepState.SETUP;
    }


    private void Update()
    {
        switch (battleState)
        {
            case BattleStepState.SETUP:
                setupState.EnterState();
                break;
            case BattleStepState.START:
                startState.EnterState();
                break;
            case BattleStepState.PLAYER_START_TURN:
                playerStartTurnState.EnterState();
                break;
            case BattleStepState.PLAYER_END_TURN:
                playerEndTurnState.EnterState();
                break;
            case BattleStepState.ENEMY_START_TURN:
                enemyStartTurnState.EnterState();
                break;
            case BattleStepState.ENEMY_END_TURN:
                enemyEndTurnState.EnterState();
                break;
            case BattleStepState.WIN:
                winState.EnterState();
                break;
            case BattleStepState.LOSE:
                loseState.EnterState();
                break;
        }
    }


    public void ChangeState(BattleStepState newState)
    {
        switch (battleState)
        {
            case BattleStepState.SETUP:
                setupState.ExitState();
                break;
            case BattleStepState.START:
                startState.ExitState();
                break;
            case BattleStepState.PLAYER_START_TURN:
                playerStartTurnState.ExitState();
                break;
            case BattleStepState.PLAYER_END_TURN:
                playerEndTurnState.ExitState();
                break;
            case BattleStepState.ENEMY_START_TURN:
                enemyStartTurnState.ExitState();
                break;
            case BattleStepState.ENEMY_END_TURN:
                enemyEndTurnState.ExitState();
                break;
            case BattleStepState.WIN:
                winState.ExitState();
                break;
            case BattleStepState.LOSE:
                loseState.ExitState();
                break;
        }

        battleState = newState;
    }
}
