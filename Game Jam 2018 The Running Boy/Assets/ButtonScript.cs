using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Scene");
        Time.timeScale = 1;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
