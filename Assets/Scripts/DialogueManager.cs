using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject textBG;

    private Coroutine dialogueCoroutine = null; // Reference to the currently running showDialogue coroutine
    private float textDuration = 3f;

    public void playDialogue(string type, string subType)
    {
        if (type == "welcome")
        {
            welcomeDialogue();
        }
        else if (type == "redCorrect" || type == "yellowCorrect" || type == "blackCorrect" || type == "blueCorrect" || type == "greenCorrect")
        {
            successDiaglogue(type, subType);
        }
        else if (type == "redWrong" || type == "yellowWrong" || type == "blackWrong" || type == "blueWrong" || type == "greenWrong")
        {
            failDialogue(type);
        }
        else if (type == "gameOver")
        {
            GameOverDialogue();
        }
        else if (type == "level")
        {
            levelComplete();
        }
    }


    private void welcomeDialogue()
    {
        string[] message = { "Welcome!" };
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }
    void levelComplete()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            string[] message = { "Endless Mode" };
               ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
        }
        else
        {
            string[] message = { "Level " + PlayerPrefs.GetInt("CurrentLevel").ToString() + "." };
                ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
        }

    }
    private void successDiaglogue(string bin, string subType)
    {
        int combo = PlayerPrefs.GetInt("ComboValue");
        string[] message;
        bin += subType;
        if (combo == 5 || combo == 10)
        {
            message = new string[] { $"Nice! That's {combo} correct bins in a row!",
                                     $"Keep it up, that's {combo} correct bins in a row! "};
        }
        else if (binMessages.ContainsKey(bin))
        {
            message = binMessages[bin];
        }
        else
        {
            message = new string[] { "Welldone!", "Good Job!" };
        }
        ShowDialogue(message[Random.Range(0, message.Length)], textDuration);
    }

    private Dictionary<string, string[]> binMessages = new Dictionary<string, string[]>
    {
        {"redWrong", new string[] { "Wrong bin. Only hazardous materials belong in the hazard bin." }},
        {"yellowWrong", new string[] { "Wrong bin. Only recyclable paper belongs in the paper bin." }},
        {"blackWrong", new string[] { "Wrong bin. Only non-recyclable plastics belong in the General Bin."}},
        {"blueWrong", new string[] { "Wrong bin. Only recyclable materials belong in the recycle bin" }},
        {"greenWrong", new string[] { "Wrong bin. Only glass materials belong in the green bin" }},

        {"redCorrect", new string[] { "Correct! Hazardous materials must be disposed of separately to prevent harm to people, wildlife, and the environment." }},
        {"yellowCorrect", new string[] { "Correct! Paper is recyclable as it can be processed into new products, conserving resources." }},
        {"blackCorrect", new string[] { "Correct! Non-recyclable plastics go in the general bin to prevent recycling contamination and disruption of recycling process." }},
        {"blueCorrectMetal", new string[] { "Correct! Recycling metal fuels circular economies, reducing virgin mining and enabling sustainable resource use." }},
        {"blueCorrectPlastic", new string[] { "Correct! Recyclable plastics can be remelted and reshaped multiple times, preserving their quality and reducing the demand for new materials." }},
        {"greenCorrect", new string[] { "Correct! Recycling glass bottles and jars save raw materials and energy, aiding environmental health." }},
    };

    private void failDialogue(string bin)
    {
        int mistakes = PlayerPrefs.GetInt("IncorrectGuesses");
        string[] message;

        if (mistakes == 3 || mistakes == 5)
        {
            message = new string[] {
            $"That's {mistakes} mistakes in a row, please be careful. Improper recycling is harmful to the environment.",
            $"That's {mistakes} mistakes in a row. Please refer to your guide if you get stuck."
        };
        }
        else if (binMessages.ContainsKey(bin))
        {
            message = binMessages[bin];
        }
        else
        {
            
            message = new string[] { "That wasn't correct." };
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
