using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;

    public void playDialogue(string type)
    {
        if(type == "welcome")
        {
            welcomeDialogue();
        }
        if (type == "success")
        {
            successDiaglogue();
        }
        if (type == "fail")
        {
            failDialogue();
        }
        if (type == "gameOver")
        {
            GameOverDialogue();
        }
        if (type == "level")
        {
            levelComplete();
        }
    }
    void welcomeDialogue()
    {
        string[] message = {"Welcome!" };
        StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
    }
    void successDiaglogue()
    {
        int combo = PlayerPrefs.GetInt("ComboValue");
        if (combo == 5 || combo == 10 )
        {
            string[] message = { "Nice! That's " + combo + " correct bins in a row!", 
                                  "Keep it up, that's " + combo + " correct bins in a row! "};
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
        else
        {   
            string[] message = { "Welldone!", "Good Job!" };
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
    }
    void failDialogue()
    {
        int mistakes = PlayerPrefs.GetInt("IncorrectGuesses");
        if (mistakes == 3 || mistakes == 5 )
        {
            string[] message = { "That's " + mistakes +  " mistakes in a row, please be careful. Improper recycling is harmful to the environment.",
                                 "That's " + mistakes +  " mistakes in a row. Please refer to your guide if you get stuck." };
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
        else
        {
            string[] message = {"That's not the correct bin, try again.",
                                "Hmmmm... That's not right." };
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
    }
    void levelComplete()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            string[] message = { "Endless Mode" };
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
        else
        {
            string[] message = { "Level " + PlayerPrefs.GetInt("CurrentLevel").ToString() + "."};
            StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
        }
    }
    void GameOverDialogue()
    {
        string[] message = {"Alright, get outta here, you're messing up the place."};
        StartCoroutine(showDialogue(message[Random.Range(0, message.Length)], 5f));
    }

    IEnumerator showDialogue(string message, float duration)
    {
        
        dialogueText.text = message;
        yield return new WaitForSeconds(duration);
        dialogueText.text = "";
    }
    
}
