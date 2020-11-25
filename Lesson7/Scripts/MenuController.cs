using UnityEngine;
using UnityEngine.SceneManagement;


namespace Hosthell
{
    public sealed class MenuController : MonoBehaviour
    {
        #region UnityMethods
        
        void Start()
        {

        }

        void Update()
        {

        }

        #endregion

        #region Methods

        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion
    }
}
