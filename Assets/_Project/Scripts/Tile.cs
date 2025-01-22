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

    

    public void SetPosition(int x, int y)
    {
        InitialAppearance();
        this.x = x;
        this.y = y;
        AddState(TileState.Empty);
    } 

    

    public void InitialAppearance()
    {
        mainState.alpha = 0f;
        emptyStateGO.transform.localScale = Vector3.zero;

        mainState.DOFade(1f, Random.Range(.2f, .3f)).SetEase(Ease.InOutSine);
        emptyStateGO.transform.DOScale(Vector3.one, Random.Range(.3f,0.8f)).SetEase(Ease.OutBack);
    }






    // STATE MANAGEMENT ---------------------------------------------
    public void ReadState()
    {
        float targetAlphaBuild = 0f;

        foreach(TileState tileState in tileStates)
        {
            if (tileState == TileState.Empty)
            {
                targetAlphaBuild += 0.10f;
            }
            else if (tileState == TileState.Occupied)
            {
                targetAlphaBuild += -0.04f;
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