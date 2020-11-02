using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;  // Array of spawn points to be used.
    [SerializeField] private GameObject[] enemyPrefabs; // Array of different Enemies that are used.
    [SerializeField] private int amountEnemies = 13;  // Total number of enemies to spawn.
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < amountEnemies; i++)
        {   
            var enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // Randomize the different enemies to instantiate.
            var spawnPoint = spawnPoints[i]; // Number of spawn point

            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }        
}     
        
}
