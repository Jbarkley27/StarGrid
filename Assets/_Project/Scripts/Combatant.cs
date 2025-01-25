using UnityEngine;

public class Combatant : MonoBehaviour
{
    public Tile onTile = null;


    public void AssignTile(Tile tile)
    {
        onTile = tile;
    }

    public void RemoveTile()
    {
        onTile = null;
    }
}