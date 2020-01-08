using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(Constants.SCENES.Endless.ToString());
        }
    }
}