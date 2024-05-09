using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private float playerSpeed;

    private Vector2 input;
    private Rigidbody2D rb;



    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StaticPlayerInput.PlayerInputResponse.Enable();
        input = Vector2.zero;   
    }

    private void Update()
    {
        input = StaticPlayerInput.PlayerInputResponse.MovementMap.Movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = input * playerSpeed;
    }


}
