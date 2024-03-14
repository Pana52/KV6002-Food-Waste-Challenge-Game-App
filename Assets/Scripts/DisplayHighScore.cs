using TMPro;
using UnityEngine;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
