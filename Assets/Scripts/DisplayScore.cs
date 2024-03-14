using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("PlayerScore").ToString();
    }
}


