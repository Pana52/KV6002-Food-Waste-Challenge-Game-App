using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void GoToScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Application closed");
    }

    public void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
