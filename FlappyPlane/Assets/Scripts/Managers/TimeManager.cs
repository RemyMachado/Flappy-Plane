using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public void Continue()
        {
            Time.timeScale = 1f;
        }

        public void Stop()
        {
            Time.timeScale = 0f;
        }
    }
}