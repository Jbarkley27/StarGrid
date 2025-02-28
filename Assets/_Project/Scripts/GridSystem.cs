using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public Tile[,] playerGrid;
    public Tile[,] enemyGrid;

    public Tile tilePrefab;

    public int width;
    public int height;

    public GameObject enemyGridRoot;
    public GameObject playerGridRoot;
    public GridLayoutGroup enemyGridLayout;
    public GridLayoutGroup playerGridLayout;
    public static GridSystem instance;
    public Pattern playerPattern;
    public Pattern enemyPattern;


    [Header("Navigation")]
    public Button moveUp;
    public Button moveDown;
    public Button moveLeft;
    public Button moveRight;
    public Vector3 centerPointOfSelectedTiles;
    public bool isPlayerMoving = false;
    public enum Direction {UP, DOWN, LEFT, RIGHT};


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
        CreateGrids();
    }


    private void Update() 
    {
        ReadPlayerGrid();
        HandleNavigationUI();
    }




    private void CreateGrids()
    {
        // remove all children
        foreach (Transform child in enemyGridRoot.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in playerGridRoot.transform)
        {
            Destroy(child.gameObject);
        }

        // create new grid
        playerGrid = new Tile[width, height];
        enemyGrid = new Tile[width, height];

        enemyGridLayout.constraintCount = width;
        playerGridLayout.constraintCount = width;

        // create tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = Instantiate(tilePrefab, enemyGridRoot.transform);
                tile.SetPosition(x, y);
                tile.gameObject.name = "EnemyTile_" + x + "_" + y;
                enemyGrid[x, y] = tile;
            }
        }

        // create tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = Instantiate(tilePrefab, playerGridRoot.transform);
                tile.SetPosition(x, y);
                tile.gameObject.name = "PlayerTile_" + x + "_" + y;
                playerGrid[x, y] = tile;
            }
        }
    }



    public void ReadPlayerGrid()
    {
        if (playerPattern == null) return;

        // set occupied tiles
        foreach (Tile tile in playerPattern.patternTiles)
        {
            playerGrid[tile.x, tile.y].AddState(Tile.TileState.Occupied);
        }
    }


    public void ReadEnemyGrid()
    {
        if (enemyPattern == null) return;

        // set occupied tiles
        foreach (Tile tile in enemyPattern.patternTiles)
        {
            enemyGrid[tile.x, tile.y].AddState(Tile.TileState.Occupied);
        }
    }




    public bool IsValidPosition(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return false;
        }
        return true;
    }

    public bool CanMovePlayer(Direction direction)
    {
        foreach (Tile tile in playerPattern.patternTiles)
        {
            int x = tile.x;
            int y = tile.y;

            switch (direction)
            {
                case Direction.UP:
                    y++;
                    break;
                case Direction.DOWN:
                    y--;
                    break;
                case Direction.LEFT:
                    // Left requires special handling because of the grid layout
                    y-=6;

                    if (y < 0)
                    {
                        int width = 12 + y;
                        x--;
                        y = width;
                    }
                    break;
                case Direction.RIGHT:
                    // Right requires special handling because of the grid layout
                    y+=6;

                    if (y >= 12)
                    {
                        int width = y - 12;
                        x++;
                        y = width;
                    }
                    break;
            }

            if (!IsValidPosition(x, y))
            {
                return false;
            }

            if (playerGrid[x, y].tileStates.Contains(Tile.TileState.Blocked))
            {
                Debug.Log("Blocked Position " + x + " " + y);
                return false;
            }
        }


        return true;
    }

    public List<Tile> GetSelectedTiles(Direction direction)
    {
        List<Tile> selectedTiles = new List<Tile>();

        foreach (Tile tile in playerPattern.patternTiles)
        {
            int x = tile.x;
            int y = tile.y;

            switch (direction)
            {
                case Direction.UP:
                    y++;
                    break;
                case Direction.DOWN:
                    y--;
                    break;
                case Direction.LEFT:
                    // Left requires special handling because of the grid layout
                    y-=6;

                    if (y < 0)
                    {
                        int width = 12 + y;
                        x--;
                        y = width;
                    }
                    break;
                case Direction.RIGHT:
                    // Right requires special handling because of the grid layout
                    y+=6;

                    if (y >= 12)
                    {
                        int width = y - 12;
                        x++;
                        y = width;
                    }
                    break;
            }

            if (!IsValidPosition(x, y))
            {
                Debug.Log("Invalid Position " + x + " " + y);
                return new List<Tile>();
            }

            
            if (!selectedTiles.Contains(playerGrid[x,y])) selectedTiles.Add(playerGrid[x, y]);
        }

        return selectedTiles;
    }


    public Vector3 GetCenterOfSelectedTiles(List<Tile> selectedTiles) {

        if (selectedTiles.Count == 0)
        {
            Debug.LogError("No selected tiles");
            return Vector3.zero;
        }

        Vector3 center = Vector3.zero;

        foreach (Tile tile in selectedTiles) center += tile.transform.position;

        center /= selectedTiles.Count;
        return center;
    }


    public void MovePlayer(Direction direction, List<Tile> selectedTiles)
    {
        if (!CanMovePlayer(direction) || isPlayerMoving)
        {
            return;
        }

        isPlayerMoving = true;
    
        centerPointOfSelectedTiles = GetCenterOfSelectedTiles(selectedTiles);

        GlobalDataStore.instance.player.transform.DOMove(centerPointOfSelectedTiles, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            isPlayerMoving = false;
        });
    }










    // NAVIGATION UI ---------------------------------------------------------------------
    public void HandleNavigationUI()
    {
        moveUp.gameObject.SetActive(CanMovePlayer(Direction.UP) && !isPlayerMoving);
        moveDown.gameObject.SetActive(CanMovePlayer(Direction.DOWN) && !isPlayerMoving);
        moveLeft.gameObject.SetActive(CanMovePlayer(Direction.LEFT) && !isPlayerMoving);
        moveRight.gameObject.SetActive(CanMovePlayer(Direction.RIGHT) && !isPlayerMoving);
    }
    
}
