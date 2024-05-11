using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{

    [SerializeField][Range(1, 2)] int PlayerNumber = 1;

    [SerializeField] private float playerSpeed;

    private Vector2 input;
    private Rigidbody2D rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void Start()
    {
        //StaticPlayerInput.PlayerInputResponse.Enable();
        input = Vector2.zero;   
    }

    private void Update()
    {
        switch (PlayerNumber)
        {
            case 1:
                input = StaticPlayerInput.PlayerInputResponse.MovementMap.Movement.ReadValue<Vector2>();
                break;
            case 2:
                input = StaticPlayerInput.PlayerInputResponse.MovementMap2.Movement.ReadValue<Vector2>();
                break;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = input * playerSpeed;
    }


}
