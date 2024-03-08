
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
    }
    
    public void checkTrash(string binType, string correctBinType)
    {
        int comboValue = PlayerPrefs.GetInt("ComboValue", 0);

            if (binType == correctBinType)
            {
                Debug.Log("CORRECT");
                calculateScore(baseScoreValue);
                SetIsDragging(false);
                
        }
            else if (binType == "Incinerator")
            {
                Debug.Log("Trash item added to general waste.");
                
                comboValue = 1;
                PlayerPrefs.SetInt("ComboValue", comboValue);
        }
            else
            {
                Debug.Log("INCORRECT");
                SetIsDragging(false);
                comboValue = 1;
                PlayerPrefs.SetInt("ComboValue", comboValue);
          
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

        Debug.Log(scoreToAdd + " points added. Current score: " + playerScore + ". Combo Value: " + comboValue + ".");
    }
}
