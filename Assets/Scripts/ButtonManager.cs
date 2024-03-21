using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Reset highscore.
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }
    public void GoToScene()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 4);
        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel2()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel3()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("CurrentLevel", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
