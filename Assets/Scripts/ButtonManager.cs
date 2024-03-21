using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void GoToScene()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 4);
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

    public void LoadLevel1()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 1);
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 2);
        SceneManager.LoadScene(1);
    }
    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 3);
        SceneManager.LoadScene(1);
    }
}
