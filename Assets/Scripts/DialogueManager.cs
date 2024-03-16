using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

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
        showDialogue(message[Random.Range(0, message.Length)]);
        Debug.Log(message[Random.Range(0, message.Length)]);
    }
    void successDiaglogue()
    {
        int combo = PlayerPrefs.GetInt("ComboValue");
        if (combo == 5 || combo == 10 )
        {
            string[] message = { "Nice! That's " + combo + " correct bins in a row!", 
                                  "Keep it up, that's " + combo + " correct bins in a row! "};
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
        else
        {   
            string[] message = { "Welldone!", "Good Job!" };
            Debug.Log(message[Random.Range(0, message.Length)]);
            showDialogue(message[Random.Range(0, message.Length)]);
        }
    }
    void failDialogue()
    {
        int mistakes = PlayerPrefs.GetInt("IncorrectGuesses");
        if (mistakes == 3 || mistakes == 5 )
        {
            string[] message = { "That's " + mistakes +  " mistakes in a row, please be careful. Improper recycling is harmful to the environment.",
                                 "That's " + mistakes +  " mistakes in a row. Please refer to your guide if you get stuck." };
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
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            string[] message = { "Endless Mode" };
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
        else
        {
            string[] message = { "Good Job, now lets how you do with this new trash delivery that just arrived." };
            showDialogue(message[Random.Range(0, message.Length)]);
            Debug.Log(message[Random.Range(0, message.Length)]);
        }
    }
    void GameOverDialogue()
    {
        string[] message = {"Alright, get outta here, you're messing up the place."};
        showDialogue(message[Random.Range(0, message.Length)]);
        Debug.Log("GAME OVER: " + message[Random.Range(0, message.Length)]);
    }

    void showDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
    }
    
}
