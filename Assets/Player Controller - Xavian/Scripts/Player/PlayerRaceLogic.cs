using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerRaceLogic : MonoBehaviour
{
    [SerializeField] private float deathTime = 1f;
    [SerializeField] private float hitStunTime = 1f;
    [SerializeField] private float colliderEnableDelay = 1f;

    [SerializeField] private float maxSpeed;
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
        StaticPlayerInput.PlayerInputResponse.Disable();
        yield return new WaitForSeconds(hitStunTime);
        StaticPlayerInput.PlayerInputResponse.Enable();
        yield return new WaitForSeconds(colliderEnableDelay);
        EnableCollider();

    }

    private IEnumerator PauseBeforeDestory()
    {
        yield return new WaitForSeconds(deathTime);
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
