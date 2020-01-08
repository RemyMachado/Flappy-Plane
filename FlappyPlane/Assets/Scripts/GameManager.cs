using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;
    public Joystick joystick;

     public SceneFader sceneFader;

    private void Start()
    {
        gameIsOver = false;
    }

    void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        if ((PlayerStats.lives <= 0 || PlayerStats.energy <= 0) && !gameIsOver)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Endgame");
        gameIsOver = true;
        gameOverUI.SetActive(true);
        Destroy(joystick.gameObject);
    }
}
