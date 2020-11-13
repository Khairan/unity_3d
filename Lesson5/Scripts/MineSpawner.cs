using UnityEngine;


namespace Hosthell
{
    public sealed class MineSpawner : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _mine;
        [SerializeField] private Transform _mineSpawnPlace;
        
        #endregion


        #region UnityMethods
        
        private void Update()
        {
            PlaceMine();
        }

        #endregion


        #region Methods

        private void PlaceMine()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(_mine, _mineSpawnPlace.position, _mineSpawnPlace.rotation);
            }
        }

        #endregion
    }
}
