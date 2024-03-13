
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float conveyorSpeed;
    private bool isDragging;
    private const int baseScoreValue = 10;
    private bool isClean;

    public bool GetIsDragging()
    {
        return isDragging;
    }
    public void SetIsDragging(bool value)
    {
        isDragging = value;
    }
    public float GetConveyorSpeed()
    {
        return conveyorSpeed;
    }
    public void SetConveyorSpeed (float value)
    {
        conveyorSpeed = value;
    }
    
    private void Start()
    {
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int comboValue = PlayerPrefs.GetInt("ComboValue", 1); 
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses", 0);
    }
    
    public void checkTrash(string binType, string correctBinType)
    {
        int comboValue = PlayerPrefs.GetInt("ComboValue", 0);
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses", 0);

        if (binType == correctBinType)
            {
            Debug.Log("CORRECT");
            calculateScore(baseScoreValue);
            SetIsDragging(false);
            incorrectGuesses = 0;
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
            }

        else if (binType == "Incinerator")
        {
            Debug.Log("Trash item added to general waste.");
                
            comboValue = 1;
            incorrectGuesses += 1;
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
        }
        else
        {
            Debug.Log("INCORRECT");
            SetIsDragging(false);
            comboValue = 1;
            incorrectGuesses += 1;
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);

        }       
        PlayerPrefs.Save();
        Destroy(gameObject);
    }
    private void calculateScore(int score)
    {
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int comboValue = PlayerPrefs.GetInt("ComboValue", 1);

        int scoreToAdd = score * comboValue;
        playerScore += scoreToAdd;
        comboValue++;

        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("ComboValue", comboValue);
        PlayerPrefs.Save();

        Debug.Log((scoreToAdd) + " points added. Current score: " + playerScore + ". Combo Value: " + comboValue + ".");
    }
}
