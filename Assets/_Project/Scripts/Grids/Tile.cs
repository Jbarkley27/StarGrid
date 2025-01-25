using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int x;
    public int y;

    [Header("UI")]
    public CanvasGroup mainState;

    [Header("Physical State")]
    public GameObject emptyStateGO;

    [Header("Tile States")]
    public float currentTargetAlpha = 0f;
    public List<TileState> tileStates = new List<TileState>();
    public float transitionSpeed = 5f;

    public Combatant combatant;
    public Transform combatantSpawnPoint;

    public enum TileState
    {
        Empty,
        Occupied,
        Hovered,
        Selected,
        Checking,
        Blocked
    }

    private void Update()
    {
        ReadState();
        mainState.alpha = Mathf.Lerp(mainState.alpha, currentTargetAlpha, Time.deltaTime * transitionSpeed);
    }


    private void Awake() {
        mainState = GetComponent<CanvasGroup>();
        AddState(TileState.Empty);
    }


    private void Start() 
    {
        // mainState.alpha = 0f;
        mainState.DOFade(1f, Random.Range(.1f, .5f)).OnComplete(() => {
            AddState(TileState.Empty);
        });
    }


    public void SetCombatant(Combatant combatant)
    {
        if (this.combatant != null)
        {
            // disconnect combatant from tile
            this.combatant.RemoveTile();
        }

        // connect combatant to tile
        this.combatant = combatant;
        this.combatant.AssignTile(this);
        AddState(TileState.Occupied);
    }

    public void RemoveCombatant()
    {
        combatant = null;
        RemoveState(TileState.Occupied);
    }




    // STATE MANAGEMENT ---------------------------------------------
    public void ReadState()
    {
        float targetAlphaBuild = 0f;

        foreach(TileState tileState in tileStates)
        {
            if (tileState == TileState.Empty)
            {
                targetAlphaBuild += 0.15f;
            }
            else if (tileState == TileState.Occupied)
            {
                targetAlphaBuild += 0.30f;
            }
            else if (tileState == TileState.Hovered)
            {
                targetAlphaBuild += .50f;
            }
            else if (tileState == TileState.Selected)
            {
                targetAlphaBuild = 0.90f;
            }
        }

        currentTargetAlpha = Mathf.Clamp(targetAlphaBuild, 0f, 1f);
    }

    public void AddState(TileState state)
    {
        if (!tileStates.Contains(state))
        {
            tileStates.Add(state);
        }
    }


    public void RemoveState(TileState state)
    {
        if (tileStates.Contains(state))
        {
            tileStates.Remove(state);
        }
    }




    // POINTER EVENTS ---------------------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        AddState(TileState.Hovered);
        SelectionManager.instance.hoveredTile = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveState(TileState.Hovered);
        SelectionManager.instance.hoveredTile = null;
    }
}