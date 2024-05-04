using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    private Rigidbody2D rb;
    private int playerLayer, destroyerLayer, enemyLayer;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLayer = 6;
        enemyLayer = 7;
        destroyerLayer = 13;
    }
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer); 
        rb.gravityScale = 0;
    }
    private void FixedUpdate()
    {
        rb.velocity = (Vector2.right * rb.velocity.x) + (Vector2.down * verticalSpeed);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == destroyerLayer)   
        {
            Destroy(this.gameObject, 0);
        }
    }
    /*
    Requirement: Add a blank child to Player GameObject    
    Child should have: EnemyDestroy layer and a same Collider as parent with isTrigger toggled
    Purpose: Prevent from affecting player's movement but still destroy itself when collide
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == destroyerLayer)   
        {
            Destroy(this.gameObject, 0);
        }
    }
    public void SetVerticalSpeed(float value)
    { 
        verticalSpeed = value;
    }
}
