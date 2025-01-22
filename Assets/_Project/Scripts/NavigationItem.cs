using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationItem: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GridSystem.Direction direction;
    public List<Tile> path = new List<Tile>();

    public void OnClick()
    {
        GridSystem.instance.MovePlayer(direction);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        path = GridSystem.instance.GetSelectedTiles(direction);
        foreach (Tile tile in path)
        {
            tile.AddState(Tile.TileState.Selected);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Tile tile in path)
        {
            tile.RemoveState(Tile.TileState.Selected);
        }
        path.Clear();
    }
}