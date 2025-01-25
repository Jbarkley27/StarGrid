using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public Tile[,] playGrid;

    public int size;

    public GameObject playGridRoot;

    public GridLayoutGroup playGridCanvasGroup;

    public static GridManager instance;
    public Tile tilePrefab;





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

    }


    private void Update() 
    {
       
    }



    public void CreateGrid()
    {
        // clear all existing children
        foreach (Transform child in playGridRoot.transform)
        {
            Destroy(child.gameObject);
        }

        playGrid = new Tile[size, size];

        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                Tile tile = Instantiate(tilePrefab, playGridRoot.transform);
                tile.gameObject.name = "Tile_" + x + "_" + y;
                tile.x = x;
                tile.y = y;
                playGrid[x, y] = tile;

                // Create a sphere at the position of the tile
                // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // sphere.transform.position = new Vector3(x, 0, y);
            }
        }
    }

   

    public void ReadPlayerGrid()
    {

    }


    public void ReadEnemyGrid()
    {

    }




    public bool IsValidPosition(int x, int y)
    {
        if (x < 0 || x >= size || y < 0 || y >= size)
        {
            return false;
        }
        return true;
    }


    public Tile GetRandomAvailableTile()
    {
        List<Tile> availableTiles = new List<Tile>();

        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                if (playGrid[x, y].tileStates.Contains(Tile.TileState.Empty))
                {
                    availableTiles.Add(playGrid[x, y]);
                }
            }
        }

        if (availableTiles.Count == 0)
        {
            return null;
        }

        return availableTiles[Random.Range(0, availableTiles.Count)];
    }
    
}
