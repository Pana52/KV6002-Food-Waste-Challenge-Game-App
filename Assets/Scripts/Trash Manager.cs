using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrashManager : GameManager
{
    //Singleton instance.
    private static TrashManager instance;
    private GameObject[] trash;
    //Interval of trash generation. 
    private float trashInterval = 5f;
    //Length of level in seconds. 
    private float levelInterval = 20f;
    private bool levelActive = true;
    //Number of consectutive guesses until game over is triggered. 
    private int incorrectGuessesLimit = 5;
    //Boolean if player achieves new high score. 
    private bool isNewHighScore = false;

    //Boolean to indicate if the coroutine is paused.
    private bool coroutinePaused = false;
    private int currentLevel = 1;

    public GameObject spawnLocation;
    //UI element references. 
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private Text GameOverMessgae;
    


    //Ensure only one instance of TrashManager exists.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        resetPlayerPrefs();
        Dialogue = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items");
        trash = new GameObject[trashItems.Length];
        for (int i = 0; i < trashItems.Length; i++)
        {
            trash[i] = trashItems[i];
        }
        StartCoroutine(GenerateTrashCoroutine());
        StartCoroutine(EndLevelCoroutine());

        //Play dialogue. 
        Dialogue.playDialogue("welcome");






        //--------Work in progress.
        /**
        foreach (GameObject trashObject in trash)
        {
            // Get the component with the script containing the trashType variable
            Trash trashScript = trashObject.GetComponent<Trash>();

            // Check if the trashScript is not null and if its trashType matches the desired value
            if (trashScript != null && trashScript.trashType == "Paper")
            {
                // Instantiate the trashObject if it meets the desired criteria
                Instantiate(trashObject, transform.position, Quaternion.identity);
            }
        }**/
        //--------------

    }
    private void Update()
    {
        //For testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            generateTrash();
        }
        //Loss condition - Can't be triggered during tutorial levels. 
        if (PlayerPrefs.GetInt("IncorrectGuesses") >= incorrectGuessesLimit && levelActive == true && currentLevel > 3)
        {
            levelActive = false;
            GameOver(PlayerPrefs.GetInt("PlayerScore"), PlayerPrefs.GetInt("HighScore"));
            
        }
    }

    public void levelOneTrash()
    {
        /**foreach (GameObject trashObject in trash)
        {
            // Get the component with the script containing the trashType variable
            Trash trashScript = trashObject.GetComponent<Trash>();

            // Check if the trashScript is not null and if its trashType matches the desired value
            if (trashScript != null && trashScript.trashType == "Paper" || trashScript.trashType == "Glass")
            {
                // Instantiate the trashObject if it meets the desired criteria
                Instantiate(trashObject, transform.position, Quaternion.identity);
            }
        }**/
    }
    public void levelTwoTrash()
    {

    }

    public void levelThreeTrash()
    {

    }

    public void generateTrash()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (currentLevel == 1)
        {
            levelOneTrash();
        }
        if (currentLevel == 2)
        {
            levelTwoTrash();
        }
        if (currentLevel == 3)
        {
            levelThreeTrash();
        }
        if (currentLevel > 4)
        {
            Dialogue.playDialogue("level");
            //Debug.Log("Trash Generated");
            int randomIndex = Random.Range(0, trash.Length);
            Vector3 targetPosition = spawnLocation.transform.position;
            Instantiate(trash[randomIndex], targetPosition, Quaternion.Euler(0, 0, 0)); //Random.Range(0, 360), 0));
        }

    }

    IEnumerator GenerateTrashCoroutine()
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



    // Method to pause the coroutine
    public void PauseCoroutine()
    {
        coroutinePaused = true;
    }

    // Method to resume the coroutine
    public void ResumeCoroutine()
    {
        coroutinePaused = false;
    }

    IEnumerator EndLevelCoroutine()
    {
        //Lenght of level in seconds. 
        yield return new WaitForSeconds(levelInterval);
        EndLevel();
    }

    void EndLevel()
    {
        //Increment urrent level number.
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        Debug.Log("END OF LEVEL. Level " + currentLevel + " active.");
        //Play dialogue. 
        Dialogue.playDialogue("level");
        //Check if current level number exceeds maximum number of levels.
        if (currentLevel == 4)
        {
            //Endless  
        }
        else
        {
            DestroyAllTrashObjects();
            StartNextLevel();
        }
        PlayerPrefs.Save();
    }
    void StartNextLevel()
    {
        // Reset levelActive to true for the next level
        levelActive = true;

        // Start the coroutine to generate items for the next level
        //StartCoroutine(GenerateTrashCoroutine());

        // Start the coroutine to end the level after 30 seconds
        StartCoroutine(EndLevelCoroutine());
    }
    public void DestroyAllTrashObjects()
    {
        //Add trash object to array. 
        GameObject[] trashObjects = GameObject.FindGameObjectsWithTag("MoveableObject");

        //Destroy objects. 
        foreach (GameObject trashObject in trashObjects)
        {
            Destroy(trashObject);
        }
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
    void resetPlayerPrefs()
    {
        PlayerPrefs.SetInt("IncorrectGuesses", 0);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("ComboValue", 1);
        PlayerPrefs.Save();
        Debug.Log("New game started, PlayerPrefs reset.");
    }
}
