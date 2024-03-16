using TMPro;
using UnityEngine;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        
        if(PlayerPrefs.GetInt("HighScore") > 999 )
        {
            highScoreText.fontSize = 180;
        }
        if (PlayerPrefs.GetInt("HighScore") > 9999)
        {
            highScoreText.fontSize = 130;
        }
    }
}
