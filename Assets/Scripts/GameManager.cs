/// <summary>
/// Script Summary - Manages game states. item dragging, dialogue, score tracking.
///                  Handles items going into the right bin and conveyor belt speed.
/// @Author - Luke Walpole + Others
/// </summary>

using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private bool isDragging;
    private const int baseScoreValue = 10;
    private TextMeshProUGUI ScoreText;
    DialogueManager dialogue;
    public DialogueManager Dialogue { get => dialogue; set => dialogue = value; }

    public AudioManager audioManager;

    private void Awake()
    {
        // Find AudioManager + Basic error handling
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    }

    //Getters and setters for if player is dragging item. 
    public bool GetIsDragging()
    {
        return isDragging;
    }

    public void SetIsDragging(bool value)
    {
        isDragging = value;
    }

    private void Start()
    {
        //Get playerPrefs.
        PlayerPrefs.GetInt("PlayerScore", 0);
        PlayerPrefs.GetInt("ComboValue", 1);
        PlayerPrefs.GetInt("IncorrectGuesses", 0);
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.GetInt("CurrentLevel") < 4)
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
            ScoreText.text = "0";
        }

        PlayerPrefs.Save();
        //Reference to DiologueManager class. 
        Dialogue = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void checkTrash(string binType, string correctBinType, string subType)
    {
        int comboValue = PlayerPrefs.GetInt("ComboValue");
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses");
        
        //Correct bin. 
        if (binType == correctBinType)
        {
            ConveyorSpeed("correct");
            calculateScore(baseScoreValue);
            SetIsDragging(false);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
            //Play dialogue. 

            if (binType == "Bin_Hazard")
            {
                Dialogue.playDialogue("redCorrect");
            }
            if (binType == "Bin_Paper")
            {
                Dialogue.playDialogue("yellowCorrect");
            }
            if (binType == "Bin_General")
            {
                Dialogue.playDialogue("blackCorrect");
            }
            if (binType == "Bin_Recycled")
            {
                Dialogue.playDialogue("blueCorrect", subType);
            }
            if (binType == "Bin_Glass")
            {
                Dialogue.playDialogue("greenCorrect");
            }

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.itemRight, 0.35f);
            }
        }
        //End of conveyor belt. 
        if (binType == "ConveyorEndCollider")
        {
            ConveyorSpeed("mistake");
            
            if (PlayerPrefs.GetInt("CurrentLevel") > 3)
            {
                incorrectGuesses += 1;
                comboValue = 1;
            }
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
        }
        //Incorrect bin. 
        else if (binType != correctBinType)
        {
            SetIsDragging(false);
            
            ConveyorSpeed("mistake");
            
            if (PlayerPrefs.GetInt("CurrentLevel") > 3)
            {
                incorrectGuesses += 1;
                comboValue = 1;
            }
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
            PlayerPrefs.SetInt("ComboValue", comboValue);

            //Play dialogue. 

            if (binType == "Bin_Hazard")
            {
                Dialogue.playDialogue("redWrong");
            }
            if (binType == "Bin_Paper")
            {
                Dialogue.playDialogue("yellowWrong");
            }
            if (binType == "Bin_General")
            {
                Dialogue.playDialogue("blackWrong");
            }
            if (binType == "Bin_Recycled")
            {
                Dialogue.playDialogue("blueWrong");
            }
            if (binType == "Bin_Glass")
            {
                Dialogue.playDialogue("greenWrong");
            }
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.itemWrong, 0.28f);
            }
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
            //Display on screen. 
            ScoreText.text = playerScore.ToString(); 
        }
        else
        {
            ScoreText.text = "0";
        }
    }

    public void ConveyorSpeed(string type) //Controls conveyor belt speed. 
    {
        float value = 0.01f;
        float speed = PlayerPrefs.GetFloat("ConveyorSpeed");
        
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            //Condition ensure speed cannot go below default value. 
            if (type == "mistake" && speed > 0.7)
            {
                speed -= value;
            }
            if (type == "correct")
            {
                speed += value;
            }
            if (type == "level")
            {
                speed += value;
            }
            PlayerPrefs.SetFloat("ConveyorSpeed", speed);
            PlayerPrefs.Save();
        } 
    }
}