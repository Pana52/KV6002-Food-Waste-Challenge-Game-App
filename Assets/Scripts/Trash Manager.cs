using System.Collections;
using UnityEngine;

public class TrashManager : GameManager
{
    //Singleton instance.
    private static TrashManager instance;
    private GameObject[] trash;

    private float trashInterval = 5f;
    private float levelInterval = 10f;
    private bool levelActive = true;

    //Boolean to indicate if the coroutine is paused.
    private bool coroutinePaused = false;
    private int currentLevel = 1;

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
        GameObject[] trashItems = Resources.LoadAll<GameObject>("Prefabs/Trash_Items");
        trash = new GameObject[trashItems.Length];
        for (int i = 0; i < trashItems.Length; i++)
        {
            trash[i] = trashItems[i];
        }
        StartCoroutine(GenerateTrashCoroutine());
        StartCoroutine(EndLevelCoroutine());

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
    }

    public void levelOneTrash()
    {
        foreach (GameObject trashObject in trash)
        {
            // Get the component with the script containing the trashType variable
            Trash trashScript = trashObject.GetComponent<Trash>();

            // Check if the trashScript is not null and if its trashType matches the desired value
            if (trashScript != null && trashScript.trashType == "Paper" || trashScript.trashType == "Glass")
            {
                // Instantiate the trashObject if it meets the desired criteria
                Instantiate(trashObject, transform.position, Quaternion.identity);
            }
        }
    }
    public void levelTwoTrash()
    {

    }

    public void levelThreeTrash()
    {

    }

    public void generateTrash()
    {

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
        /**
                Debug.Log("Trash Generated");
                int randomIndex = Random.Range(0, trash.Length);
                Vector3 spawnLocation = new Vector3(-13, 2, 10);
                Instantiate(trash[randomIndex], spawnLocation, Quaternion.identity);
            **/
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



    //Method to pause the coroutine.
    public void PauseCoroutine()
    {
        coroutinePaused = true;
    }

    //Method to resume the coroutine.
    public void ResumeCoroutine()
    {
        coroutinePaused = false;
    }



    IEnumerator EndLevelCoroutine()
    {
        //Wait for 30 seconds before ending the level.
        yield return new WaitForSeconds(levelInterval);

        //End the level.
        EndLevel();
    }

    void EndLevel()
    {
        //Increment the current level number.
        currentLevel++;
        Debug.Log("END OF LEVEL. Level " + currentLevel + " active.");
        //Check if the current level number exceeds the maximum number of levels.
        if (currentLevel == 3)
        {
            //Endless.
            
        }
        else
        {
            //If not, start the next level.
            StartNextLevel();
        }
    }
    void StartNextLevel()
    {
        //Reset levelActive to true for the next level.
        levelActive = true;

        //Start the coroutine to generate items for the next level.
        //StartCoroutine(GenerateTrashCoroutine());

        //Start the coroutine to end the level after 30 seconds.
        StartCoroutine(EndLevelCoroutine());
    }

    void EndGame()
    {
        // Perform end-game logic here...
    }
}
