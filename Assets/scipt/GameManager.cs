using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public GameOverScreen GameOverScreen2;
    // Start is called before the first frame update
    bool gameHasEnded = false;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void EndGame()
    {
       
            Cursor.lockState = CursorLockMode.None;
            gameHasEnded = true;
            Time.timeScale = 0;
            Debug.Log("Game Pause");
            GameOverScreen.Setup();
            //Restart();
        
        
    }
    public void EndGame2()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Win");
            GameOverScreen2.Setup();
            //Restart();
        }

    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        gameHasEnded = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        GameOverScreen.Return();

    }
    public void Return()
    {
        gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
