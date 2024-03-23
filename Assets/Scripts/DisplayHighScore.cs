using TMPro;
using UnityEngine;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        
        //Decrease font size if score above 999. 
        if(PlayerPrefs.GetInt("HighScore") > 999 )
        {
            highScoreText.fontSize = 180;
        }
        //Decrease font size if score above 9999. 
        if (PlayerPrefs.GetInt("HighScore") > 9999)
        {
            highScoreText.fontSize = 130;
        }
    }
}
