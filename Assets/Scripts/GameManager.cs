
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private float conveyorSpeed;
    private bool isDragging;
    private const int baseScoreValue = 10;
    private bool isClean;

    private TextMeshProUGUI ScoreText;
    //Initialize class reference. 
    DialogueManager dialogue;

    public DialogueManager Dialogue { get => dialogue; set => dialogue = value; }

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
        //Get playerPrefs
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int comboValue = PlayerPrefs.GetInt("ComboValue", 1); 
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses", 0);
        ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("CurrentLevel") < 4)
        {
            PlayerPrefs.SetInt("PlayerScore", -1);
        }
        if (PlayerPrefs.GetInt("CurrentLevel") == 4 && (PlayerPrefs.GetInt("PlayerScore") == -1))
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
            ScoreText.text = "Score: " + ScoreText.text.ToString();
        }
        PlayerPrefs.Save();
        //Reference to DiologueManager class. 
        Dialogue = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
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
            //Play dialogue. 
            Dialogue.playDialogue("success");
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
            //Play dialogue. 
            Dialogue.playDialogue("fail");

        }
        PlayerPrefs.Save();
        Destroy(gameObject);
    }

    public void calculateScore(int score)
    {
        if (PlayerPrefs.GetInt("CurrentLevel") > 3)
        {
            int playerScore = PlayerPrefs.GetInt("PlayerScore");
            int comboValue = PlayerPrefs.GetInt("ComboValue");

            int scoreToAdd = score * comboValue;
            playerScore += scoreToAdd;
            comboValue++;

            PlayerPrefs.SetInt("PlayerScore", playerScore);
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.Save();
            ScoreText.text = "Score: " + playerScore.ToString(); 
            Debug.Log((scoreToAdd) + " points added. Current score: " + playerScore + ". Combo Value: " + comboValue + ".");
        }
        else
        {
            ScoreText.text = "";
        }
    }
}
