using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Vector3 = UnityEngine.Vector3;

public class SelectionManager: MonoBehaviour
{
    public Tile hoveredTile;
    public static SelectionManager instance;
    public TMP_Text selectionText;
    public enum SelectionState {MOVE, SINGLE_ATTACK, MULTI_ATTACK, NONE};
    public SelectionState selectionState = SelectionState.NONE;
    public Vector3 playerStartingPosition;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found an Spawn Manager object, destroying new one.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() 
    {
        selectionState = SelectionState.NONE;
        GlobalDataStore.instance.player.transform.position = playerStartingPosition;
    }

    private void Update() 
    {
        selectionText.text = selectionState.ToString();
    }

    public void SetSelectionState(SelectionState state) 
    {
        selectionState = state;
    }


}