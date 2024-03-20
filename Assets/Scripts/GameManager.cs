
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private bool isDragging;
    private const int baseScoreValue = 10;
    private TextMeshProUGUI ScoreText;
    //Initialize class reference. 
    DialogueManager dialogue;

    AudioManager audioManager;

    public DialogueManager Dialogue { get => dialogue; set => dialogue = value; }

    private void Awake()
    {
        // Attempt to find and reference the AudioManager on Awake
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("Failed to find an AudioManager in the scene.");
        }
    }

    public bool GetIsDragging()
    {
        return isDragging;
    }
    public void SetIsDragging(bool value)
    {
        //Debug.LogWarning("is Dragging = " + value);
        isDragging = value;
    }
    private void Start()
    {
        //Get playerPrefs
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int comboValue = PlayerPrefs.GetInt("ComboValue", 1); 
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses", 0);
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

    public void checkTrash(string binType, string correctBinType)
    {
        int comboValue = PlayerPrefs.GetInt("ComboValue", 0);
        int incorrectGuesses = PlayerPrefs.GetInt("IncorrectGuesses", 0);
        
        if (binType == correctBinType)
        {
            ConveyorSpeed("correct");
            //Debug.Log("CORRECT");
            calculateScore(baseScoreValue);
            SetIsDragging(false);
            incorrectGuesses = 0;
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
            //Play dialogue. 
            Dialogue.playDialogue("success");

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.itemRight);
            }
        }

        if (binType == "ConveyorEndCollider")
        {
            //Debug.Log("Trash item added to general waste.");
            ConveyorSpeed("mistake");
            comboValue = 1;
            incorrectGuesses += 1;
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
        }
        else if (binType != correctBinType)
        {
            //Debug.Log("INCORRECT");
            SetIsDragging(false);
            ConveyorSpeed("mistake");
            comboValue = 1;
            incorrectGuesses += 1;
            PlayerPrefs.SetInt("ComboValue", comboValue);
            PlayerPrefs.SetInt("IncorrectGuesses", incorrectGuesses);
            //Play dialogue. 
            Dialogue.playDialogue("fail");

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.itemWrong);
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
            ScoreText.text = playerScore.ToString(); 
            Debug.Log((scoreToAdd) + " points added. Current score: " + playerScore + ". Combo Value: " + comboValue + ".");
        }
        else
        {
            ScoreText.text = "0";
        }
    }

   
    public void ConveyorSpeed(string type)
    {

        float combo = PlayerPrefs.GetInt("ComboValue");
        float value = 0.01f;
        float speed = PlayerPrefs.GetFloat("ConveyorSpeed");
        
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            //Condition ensure speed cannot go below default value. 
            if (type == "mistake" && speed > 0.7)
            {
                speed -= value;
                Debug.Log("Speed decreased by: " + value);
            }
            if (type == "correct")
            {
                speed += value;
                Debug.Log("Speed increased by: " + value);
            }
            if (type == "level")
            {
                speed += value;
                Debug.Log("Speed increased by: " + value);
            }
            PlayerPrefs.SetFloat("ConveyorSpeed", speed);
            Debug.Log("Conveyor Belt Speed is now: " + speed);
            PlayerPrefs.Save();
        }

        
    }
}
