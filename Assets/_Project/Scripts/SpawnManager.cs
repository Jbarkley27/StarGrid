using UnityEngine;

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
            Combatant newPlayer = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, randomTile.transform.GetChild(0));


            newPlayer.transform.position = Vector3.zero;

            // newPlayer.transform.position = new Vector3(
            //     newPlayer.transform.position.x,
            //     randomTile.gameObject.transform.position.y + combatantHeightOffset,
            //     randomTile.gameObject.transform.position.z
            // );
            
            
            randomTile.SetCombatant(newPlayer);
        } else
        {
            Debug.LogError("No available tiles to spawn player on.");
        }
    }

    public void SpawnEnemy()
    {
        Combatant newEnemy = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Tile randomTile = GridManager.instance.GetRandomAvailableTile();

        if (randomTile != null)
        {
            Debug.Log("Spawning enemy on tile: " + randomTile.name);
            Debug.Log("Position: " + randomTile.gameObject.transform.position);
            newEnemy.transform.position = new Vector3(
                randomTile.gameObject.transform.position.x,
                randomTile.gameObject.transform.position.y + combatantHeightOffset,
                randomTile.gameObject.transform.position.z
            );

            randomTile.SetCombatant(newEnemy);
        } else
        {
            Debug.LogError("No available tiles to spawn player on.");
        }
    }
}