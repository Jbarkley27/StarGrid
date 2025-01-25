using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Combatant playerPrefab;
    public Combatant enemyPrefab;
    public float combatantHeightOffset;

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

    public void SpawnPlayer()
    {
        
        Tile randomTile = GridManager.instance.GetRandomAvailableTile();

        if (randomTile != null)
        {
            Debug.Log("Spawning player on tile: " + randomTile.name);
            Debug.Log("Position: " + randomTile.x + ", " + randomTile.y);
            Combatant newPlayer = Instantiate(playerPrefab, randomTile.combatantSpawnPoint);

            // Set the player's position to match the tile's position
            newPlayer.transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InSine);   
            randomTile.SetCombatant(newPlayer);
        } else
        {
            Debug.LogError("No available tiles to spawn player on.");
        }
    }

    public void SpawnEnemy()
    {
        Tile randomTile = GridManager.instance.GetRandomAvailableTile();

        if (randomTile != null)
        {
            Combatant newEnemy = Instantiate(playerPrefab, randomTile.combatantSpawnPoint);
            Debug.Log("Spawning enemy on tile: " + randomTile.name);
            Debug.Log("Position: " + randomTile.gameObject.transform.position);
        

            // Set the player's position to match the tile's position
            newEnemy.transform.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InSine);   
            randomTile.SetCombatant(newEnemy);
        } else
        {
            Debug.LogError("No available tiles to spawn player on.");
        }
    }
}