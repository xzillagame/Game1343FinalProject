using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float timeInterval;


    [SerializeField] bool timerPaused = false;
    [SerializeField] bool oneShot;
    [SerializeField] private UnityEvent OnTimerComplete;


    float elapsedTime = 0f;
    bool timerComplete = false;


    public void ResetTimer()
    {
        elapsedTime = 0f;
        timerComplete = false;
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }

    public void UnPauseTimer()
    {
        timerPaused = false;
    }


    // Update is called once per frame
    private void Update()
    {

        if (timerComplete == false && timerPaused == false)
        {
            IncreaseTime();
        }
    }

    private void IncreaseTime()
    {

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeInterval)
        {
            elapsedTime = 0f;

            if(oneShot == true)
            {
                timerComplete = true;
            }       

            OnTimerComplete?.Invoke();
        }

    }

}
