using UnityEngine;
using UnityEngine.SceneManagement;


namespace Hosthell
{
    public sealed class MenuController : MonoBehaviour
    {
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
