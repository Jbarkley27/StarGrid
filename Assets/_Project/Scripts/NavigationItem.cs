using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationItem: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GridSystem.Direction direction;
    public List<Tile> path = new List<Tile>();

    public void OnClick()
    {
        // Debug.Log("Clicked Path Size: " + path.Count);
        // Debug.Log("Clicked Direction: " + direction);
        GridSystem.instance.MovePlayer(direction, path);
        ClearPath();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetPath();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClearPath();
    }

    public void GetPath()
    {
        path = GridSystem.instance.GetSelectedTiles(direction);
        foreach (Tile tile in path)
        {
            tile.AddState(Tile.TileState.Selected);
        }
    }

    public void ClearPath()
    {
        foreach (Tile tile in path)
        {
            tile.RemoveState(Tile.TileState.Selected);
        }
        path.Clear();
    }
}