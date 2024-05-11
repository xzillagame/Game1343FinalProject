using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InitalizeRace : MonoBehaviour
{
    [SerializeField] private int startingCountNum = 3;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private UnityEvent OnRaceStart;


    private float elapsedTime = 0f;

    void Start()
    {
        Time.timeScale = 1f;
        //countdownText.text = startingCountNum.ToString();
    }

    void Update()
    {

        elapsedTime += Time.deltaTime;
        
        if(elapsedTime >= 1f)
        {
            startingCountNum -= 1;
            //countdownText.text = startingCountNum.ToString();
            elapsedTime= 0f;
        }

        if (startingCountNum == -1)
        {
            //countdownText.text = "";

            OnRaceStart?.Invoke();

            Destroy(gameObject);
        }


    }
}
