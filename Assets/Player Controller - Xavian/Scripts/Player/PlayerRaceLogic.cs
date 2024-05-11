using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerRaceLogic : MonoBehaviour
{

    [SerializeField][Range(1, 2)] private int PlayerNumber = 1;

    [SerializeField] private float deathTime = 1f;
    [SerializeField] private float hitStunTime = 1f;
    [SerializeField] private float colliderEnableDelay = 1f;

    [SerializeField] private Timer shrinkTimer;
    [SerializeField] private Timer defenseTimer;

    [SerializeField] private float shrinkSizeDivide;

    [SerializeField] private Shield DefenseShieldPrefab;
    private Shield playerShieldRef;


    //Events

    [SerializeField] private UnityEvent onShieldGained;
    [SerializeField] private UnityEvent onShieldExpire;



    [SerializeField] private UnityEvent onShrinkBuffGained;
    [SerializeField] private UnityEvent onShrinkBuffLost;




    [SerializeField] private float maxSpeed;

    public float MaxSpeed { get { return maxSpeed; } }

    private float currentSpeed;
    public float CurrentSpeed 
    {
        get { return currentSpeed; }
        private set 
        { 
            currentSpeed = value;
            if(currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }
        }
    }

    Collider2D playerCollider;


    public void OnWin()
    {
        DisableCollider();
    }

    public void OnDeath()
    {
        StaticPlayerInput.PlayerInputResponse.Disable();
        StartCoroutine(PauseBeforeDestory());
    }

    public void OnHit()
    {
        DisableCollider();
        CurrentSpeed = 0f;
        StartCoroutine(RemoveControlTemporarilyWhenHit());
    }


    #region Shrink Buff
    public void BeginShrinkBuff()
    {

        onShrinkBuffGained?.Invoke();

        shrinkTimer.ResetTimer();
        shrinkTimer.UnPauseTimer();

        //Put in Logic for Shrinking
        transform.localScale = Vector2.one / shrinkSizeDivide;
    }

    public void EndShrinkBuff() 
    {
        onShrinkBuffLost?.Invoke();

        shrinkTimer.PauseTimer();
        shrinkTimer.ResetTimer();

        //Put in Logic to Reverse Shrinking
        transform.localScale = Vector2.one;
    }
    #endregion

    #region Defense Buff
    public void BeginDefenseBuff()
    {

        onShieldGained?.Invoke();

        defenseTimer.ResetTimer();
        defenseTimer.UnPauseTimer();

        if(playerShieldRef == null)
        {
            playerShieldRef = Instantiate(DefenseShieldPrefab, transform);
        }
        else
        {
            Destroy(playerShieldRef.gameObject);
            playerShieldRef = Instantiate(DefenseShieldPrefab, transform);
        }


    }

    public void EndDefenseBuff()
    {

        defenseTimer.PauseTimer();
        defenseTimer.ResetTimer();

        if(playerShieldRef != null)
        {
            onShieldExpire?.Invoke();
            Destroy(playerShieldRef.gameObject);
        }


    }
    #endregion


    private void OnEnable()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        currentSpeed = 0f;
    }

    private void Update()
    {
        CurrentSpeed += Time.deltaTime;
    }

    private void EnableCollider()
    {
        playerCollider.enabled = true;
    }

    private void DisableCollider()
    {
        playerCollider.enabled = false;
    }

    private IEnumerator RemoveControlTemporarilyWhenHit()
    {
        switch (PlayerNumber)
        {
            case 1:
                StaticPlayerInput.PlayerInputResponse.MovementMap.Movement.Disable();
                yield return new WaitForSeconds(hitStunTime);
                StaticPlayerInput.PlayerInputResponse.Enable();
                yield return new WaitForSeconds(colliderEnableDelay);
                EnableCollider();
                break;

            case 2:
                StaticPlayerInput.PlayerInputResponse.MovementMap2.Movement.Disable();
                yield return new WaitForSeconds(hitStunTime);
                StaticPlayerInput.PlayerInputResponse.MovementMap2.Movement.Enable();
                yield return new WaitForSeconds(colliderEnableDelay);
                EnableCollider();
                break;

        }



    }

    private IEnumerator PauseBeforeDestory()
    {
        yield return new WaitForSeconds(deathTime);
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
