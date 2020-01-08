using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(Constants.SCENES.MainMenu.ToString());
        }
    }
}
