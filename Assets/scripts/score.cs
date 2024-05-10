using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
    int milesLeft = 1000;
    [SerializeField] TMP_Text playerScore;
    [SerializeField] string sceneName;
    void start()
    {
        milesLeft = 1000;
        playerScore = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        playerScore.text = "Miles Left: "+ milesLeft.ToString();
        milesLeft -= 1;
        if(milesLeft < 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
