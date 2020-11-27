using UnityEngine;


namespace Hosthell
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        #region Fields
    
        [SerializeField] private int _amountEnemies = 13;
    
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private GameObject[] _enemyPrefabs;

        #endregion


        #region UnityMethods
    
        private void Start()
        {
            Spawn();
        }

        #endregion


        #region Methods
    
        private void Spawn()
        {
            for (int i = 0; i < _amountEnemies; i++)
            {
                var enemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
                var spawnPoint = _spawnPoints[i];

                var currentEnemy = Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
                currentEnemy.GetComponent<EnemyController>().SetSpawnPoint(spawnPoint);

            }
        }

        #endregion
    }
}
