using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrashManager : GameManager
{

    public static TrashManager Instance { get; private set; }
    private System.Random _random = new System.Random();
    //Trash object array. 
    private GameObject[] trash;
    //Interval of trash generation. 
    private float trashInterval = 6f;
    //Length of level in seconds. 
    private float levelInterval = 60f;
    private bool levelActive = true;
    //Number of consectutive guesses until game over is triggered. 
    private int incorrectGuessesLimit = 6;
    //Boolean if player achieves new high score. 
    private bool isNewHighScore = false;

    //Boolean to indicate if the coroutine is paused.
    private bool coroutinePaused = false;

    private int currentLevel = 1;
    
    //Reference to spawn location of trash.
    public GameObject spawnLocation;
    //UI element references. 
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private Text GameOverMessgae;


    public TextMeshProUGUI objectInfoText;

    //Ensure only one instance of TrashManager exists.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

   
    public TextMeshProUGUI GetObjectInfoText()
    {
        return objectInfoText;
    }

    private void Start()
    {   //Reset ComboValue, IncorrectGuesses and PlayerScore PlayerPrefs. 
        resetPlayerPrefs();
        //Reset level to 1 - for testing only. The currennt level PlayerPref will be set by the main menu at the start of a game. 
        PlayerPrefs.SetInt("CurrentLevel", 1);
        //Reference to dialogue script. 
        Dialogue = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        //Star timer for level. 
        StartCoroutine(EndLevelCoroutine());
        //Start Trash generation.
        StartCoroutine(GenerateTrashCoroutine());
        //Play dialogue. 
        Dialogue.playDialogue("welcome");
    }

    
    void createTrashArray(int level)
    { 
        if (level == 4)
        {
            //Load all items into the array.
            Array.Clear(trash, 0, trash.Length);
            GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items");
            trash = new GameObject[trashItems.Length];
            for (int i = 0; i < trashItems.Length; i++)
            {
                trash[i] = trashItems[i];
            }
            Debug.Log("All Trash Objects loaded into the array.");
        }
        if (level == 1)
        {   
            //Load level 1 items into the array. 
            GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items/Level_1");
            trash = new GameObject[trashItems.Length];
            for (int i = 0; i < trashItems.Length; i++)
            {
                trash[i] = trashItems[i];
            }
            Debug.Log("Level 1 Trash Objects loaded into the array.");
        }
        if (level == 2)
        {
            //Load level 2 items into the array.
            Array.Clear(trash, 0, trash.Length);
            GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items/Level_2");
            trash = new GameObject[trashItems.Length];
            for (int i = 0; i < trashItems.Length; i++)
            {
                trash[i] = trashItems[i];
            }
            Debug.Log("Level 2 Trash Objects loaded into the array.");
        }
        if (level == 3)
        {
            //Load level 3 items into the array.
            Array.Clear(trash, 0, trash.Length);
            GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items/Level_3");
            trash = new GameObject[trashItems.Length];
            for (int i = 0; i < trashItems.Length; i++)
            {
                trash[i] = trashItems[i];
            }
            Debug.Log("Level 3 Trash Objects loaded into the array.");
        }

    }
    private void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateTrash();
        }
        //For Testing. 
        if (Input.GetKeyDown(KeyCode.K))
        {
            EndLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        //Loss condition - Can't be triggered during tutorial levels. 
        if (PlayerPrefs.GetInt("IncorrectGuesses") >= incorrectGuessesLimit && levelActive == true && currentLevel > 3)
        {
            levelActive = false;
            GameOver(PlayerPrefs.GetInt("PlayerScore"), PlayerPrefs.GetInt("HighScore"));
            
        }
    }
    public void generateTrash()
    {
        int randRot = _random.Next(-180, 180);
        Vector3 targetPosition = spawnLocation.transform.position;
        SpawnTrashAtPosition(targetPosition, randRot, _random.Next(0, trash.Length));

        //Random chance to spawn additional items.
        int diceRoll = rollDice();
        if (diceRoll == 5)
        {
            StartCoroutine(SpawnAdditionalTrashWithDelay(targetPosition, randRot, diceRoll));
        }
    }

    void SpawnTrashAtPosition(Vector3 position, int rotation, int index)
    {
        Instantiate(trash[index], position, Quaternion.Euler(0, rotation, 0));
    }

    IEnumerator SpawnAdditionalTrashWithDelay(Vector3 position, int rotation, int diceRoll)
    {
        yield return new WaitForSeconds(0.4f);
        SpawnTrashAtPosition(position, rotation, _random.Next(0, trash.Length));

        //If diceRoll was 5, there's a chance to spawn a third item
        if (diceRoll == 5)
        {
            if (rollDice() == 6)
            {
                yield return new WaitForSeconds(0.4f);
                SpawnTrashAtPosition(position, rotation, _random.Next(0, trash.Length));
            }
        }
    }
    IEnumerator GenerateTrashCoroutine() //Generates trash at an interval specified by the trashInterval variable. 
    {
        while (levelActive)
        {
            if (!coroutinePaused)
            {
                generateTrash();
                yield return new WaitForSeconds(trashInterval);
            }

            yield return null;
        }
    }
    //Ends level after the lenght of time specified by the levelInterval variable. 
    IEnumerator EndLevelCoroutine()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            levelInterval = 20;
            ConveyorSpeed("level");
            if (trashInterval > 4f)
            {
                trashInterval -= 0.1f;
            }
        }
        createTrashArray(PlayerPrefs.GetInt("CurrentLevel"));
        //Lenght of level in seconds. 
        yield return new WaitForSeconds(levelInterval);
        EndLevel();
    }

    void EndLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") < 4)
        {
            //Increment current level number.
            currentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            Debug.Log("END OF LEVEL. Level " + currentLevel + " active.");
            //Play dialogue. 
            Dialogue.playDialogue("level");
            StartNextLevel();
        }
        if (currentLevel == 4)
        {
            StartNextLevel();//Endless  
        }     
        PlayerPrefs.Save();  
    }
    void StartNextLevel()
    {
        //Reset levelActive to true for next level.
        levelActive = true;

        //Start coroutine.
        StartCoroutine(EndLevelCoroutine());
    }
    public void DestroyAllTrashObjects() //Destroys all trash objects in the scene. 
    {
        //Add trash object to array. 
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("MoveableObject");

        //Destroy objects. 
        foreach (GameObject trashObject in trashObjects)
        {
            Destroy(trashObject);
        }
        Cursor.visible = true;
        SetIsDragging(false);
    }
    void GameOver(int score, int highScore)
    {
        //Pause Gameplay. 
        Time.timeScale = 0f;
        //Play dialogue. 
        Dialogue.playDialogue("gameOver");
        //Set game over UI visibility to true.  
        GameOverUI.SetActive(true);
        int previousHighScore = PlayerPrefs.GetInt("PreviousHighScore", highScore);
        //Set new high score. 
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            isNewHighScore = true;
        }
        //Message if new high score achieved.  
        if (isNewHighScore == true)
        {
            GameOverMessgae.text = "Well Done! " + score +  " is a new record! " + "The previous high score was " + previousHighScore + ".";
            Debug.Log("Well Done! " + score + " is a new record! " + "The previous high score was " + previousHighScore + ".");
        }
        //Message if highscore is not beat. 
        else
        {
            GameOverMessgae.text = "You scored " + score + " The current high score is " + highScore + ".";
            Debug.Log("You scored " + score + " The current high score is " + highScore + ".");
        }
        PlayerPrefs.Save();
    }
    void resetPlayerPrefs() //Resets certain PlayerPrefs. 
    {
        PlayerPrefs.SetInt("IncorrectGuesses", 0);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("ComboValue", 1);
        PlayerPrefs.Save();
        Debug.Log("New game started, PlayerPrefs reset.");
    }
    int rollDice()
    {
        int randomNum = new System.Random().Next(1, 10);
        return randomNum;
    }
}
