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


    [SerializeField] private UnityEvent OnGoalReached;

    private bool goalReached = false;


    public void BeginRace()
    {
        StaticPlayerInput.PlayerInputResponse.Enable();
        StartCoroutine(RaceUpdate());
    }


    private IEnumerator RaceUpdate()
    {
        while (goalReached == false)
        {
            Debug.Log(player1Progress.gameObject.name);
            player1Progress.value = Mathf.Clamp(player1Progress.value + Time.deltaTime * raceSpeed * player1Values.CurrentSpeed, 0f, 1f);

            if (player1Progress.value == 1f && goalReached == false)
            {
                goalReached = true;
                PlayerReachedGoal(player1Values);

                break;

            }
            yield return null;
        }

        OnGoalReached?.Invoke();

    }

    private void PlayerReachedGoal(PlayerRaceLogic winningPlayer)
    {

        OnGoalReached.AddListener(winningPlayer.OnWin);

        //winnerDisplayText.text = "Winner: " + winningPlayer.name + "!!!";

        StaticPlayerInput.PlayerInputResponse.Disable();
        Time.timeScale = 0.5f;
    }

    private void OnDisable()
    {
        OnGoalReached.RemoveAllListeners();
    }


    public void TestWin()
    {
        Debug.Log("Goal Reached");
    }


}
