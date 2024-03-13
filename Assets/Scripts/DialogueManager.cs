using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : GameManager
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

    void playDialogue(string type)
    {
        if(type == "welcome")
        {
            welcomeDialogue();
        }
        if (type == "succes")
        {
            successDiaglgue();
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
        showDialogue(message[Random.Range(0, message.Length)]);
        Debug.Log(message[Random.Range(0, message.Length)]);
    }
    void successDiaglgue()
    {
        if (PlayerPrefs.GetInt("ComboValue") == 5)
        {
            string[] message = { "Nice! That's 5 correct bins in a row!"};
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
        else
        {
            string[] message = { "Welldone!", "Good Job!" };
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
    }
    void failDialogue()
    {
        if (PlayerPrefs.GetInt("IncorrectGuesses") == 3)
        {
            string[] message = { "That's 3 incorrect bins in a row, please be careful. Improper recycling is harmful to the environment."};
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
        else
        {
            string[] message = {"That's not the correct bin, try again.",
                            "Hmmmm... That's not right." };
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
    }
    void levelComplete()
    {
        string[] message = {"Good Job, now lets how you do with this new trash delivery that just arrived." };
        showDialogue(message[Random.Range(0, message.Length)]);
        Debug.Log(message[Random.Range(0, message.Length)]);
    }
    void GameOverDialogue()
    {
        string[] message = {"Alright, get outta here, you're messing up the garbage."};
        showDialogue(message[Random.Range(0, message.Length)]);
        Debug.Log(message[Random.Range(0, message.Length)]);
    }

    void showDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
    }
    
}
