using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class RaceManager : MonoBehaviour
{
    #region Slider Variables
    [SerializeField] private Slider player1Progress;
    [SerializeField] private Slider player2Progress;


    [SerializeField] private float raceSpeed;
    #endregion

    //[SerializeField] TMP_Text winnerDisplayText;

    [SerializeField] private PlayerRaceLogic player1Values;
    [SerializeField] private PlayerRaceLogic player2Values;

    [SerializeField] private float TimeAfterWin;


    private PlayerRaceLogic winningPlayer;

    private enum WinState { noOne ,player1Win, player2Win }
    private WinState raceWinState = WinState.noOne;


    [SerializeField] private UnityEvent<string> OnGoalReached;

    private bool goalReached = false;


    public void BeginRace()
    {
        StaticPlayerInput.PlayerInputResponse.MovementMap.Enable();
        StaticPlayerInput.PlayerInputResponse.MovementMap2.Enable();
        StartCoroutine(RaceUpdate());
    }


    private IEnumerator RaceUpdate()
    {
        while (goalReached == false)
        {
            player1Progress.value = Mathf.Clamp(player1Progress.value + Time.deltaTime * raceSpeed * player1Values.CurrentSpeed, 0f, 1f);

            player2Progress.value = Mathf.Clamp(player2Progress.value + Time.deltaTime * raceSpeed * player2Values.CurrentSpeed, 0f, 1f);




            if((player1Progress.value == player2Progress.value) && goalReached == false)
            {
                //Put stuff for a tie
            }
            else if(player1Progress.value == 1f && goalReached == false)
            {
                goalReached = true;
                winningPlayer = player1Values;
                raceWinState = WinState.player1Win;
                break;
            }
            else if(player2Progress.value == 1f && goalReached == false)
            {
                goalReached = true;
                winningPlayer = player2Values;
                raceWinState = WinState.player2Win;
                break;
            }


            yield return null;
        }

        PlayerReachedGoal();


    }

    private void PlayerReachedGoal()
    {

        OnGoalReached.AddListener(winningPlayer.OnWin);

        StaticPlayerInput.PlayerInputResponse.Disable();
        Time.timeScale = 0.5f;

        StartCoroutine(TimerBeforeLoadingWinScreen());

    }


    private IEnumerator TimerBeforeLoadingWinScreen()
    {
        yield return new WaitForSecondsRealtime(TimeAfterWin);

        string sceneToLoad = "";

        if(raceWinState == WinState.player1Win)
        {
            sceneToLoad = "P1Wins";
        }
        else if(raceWinState == WinState.player2Win)
        {
            sceneToLoad = "P2Wins";
        }

        OnGoalReached?.Invoke(sceneToLoad);
    }



    private void OnDisable()
    {
        OnGoalReached.RemoveAllListeners();
    }




}
