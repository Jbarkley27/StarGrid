using UnityEngine;
using System.Collections.Generic;

public class Pattern: MonoBehaviour
{
    // Represents the players shape on the board
    public List<Tile> patternTiles = new List<Tile>();
    

    // TRIGGER EVENTS ---------------------------------------------
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "tile") 
        {   
            // check if it has a Tile component
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile != null) {
                patternTiles.Add(tile);
                tile.AddState(Tile.TileState.Occupied);
            }
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "tile") {
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile != null) {
                patternTiles.Remove(tile);
                tile.RemoveState(Tile.TileState.Occupied);
            }
        }
    }

}