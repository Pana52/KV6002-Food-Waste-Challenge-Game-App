using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject textBG;

    private Coroutine dialogueCoroutine = null; // Reference to the currently running showDialogue coroutine
    private float textDuration = 3f;

    public void playDialogue(string type)
    {
        if (type == "welcome")
        {
            welcomeDialogue();
        }
        else if (type == "success")
        {
            successDiaglogue();
        }
        else if (type == "fail")
        {
            failDialogue();
        }
        else if (type == "gameOver")
        {
            GameOverDialogue();
        }
    }

    private void welcomeDialogue()
    {
        string[] message = { "Welcome!" };
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }

    private void successDiaglogue()
    {
        int combo = PlayerPrefs.GetInt("ComboValue");
        string[] message;
        if (combo == 5 || combo == 10)
        {
            message = new string[] { $"Nice! That's {combo} correct bins in a row!",
                                     $"Keep it up, that's {combo} correct bins in a row! "};
        }
        else
        {
            message = new string[] { "Welldone!", "Good Job!" };
        }
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }

    private void failDialogue()
    {
        int mistakes = PlayerPrefs.GetInt("IncorrectGuesses");
        string[] message;
        if (mistakes == 3 || mistakes == 5)
        {
            message = new string[] { $"That's {mistakes} mistakes in a row, please be careful. Improper recycling is harmful to the environment.",
                                     $"That's {mistakes} mistakes in a row. Please refer to your guide if you get stuck." };
        }
        else
        {
            message = new string[] { "That's not the correct bin, try again.",
                                     "Hmmmm... That's not right." };
        }
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }

    private void GameOverDialogue()
    {
        string[] message = { "Alright, get outta here, you're messing up the place." };
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }

    // New method to manage dialogue display
    private void ShowDialogue(string message, float duration)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
        dialogueCoroutine = StartCoroutine(ShowDialogueCoroutine(message, duration));
    }

    private IEnumerator ShowDialogueCoroutine(string message, float duration)
    {
        textBG.SetActive(true);
        dialogueText.text = message;

        yield return new WaitForSeconds(duration);

        dialogueText.text = "";
        textBG.SetActive(false);

        dialogueCoroutine = null; // Reset the coroutine reference
    }
}
