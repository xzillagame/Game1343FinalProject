using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;   // Basic stat
    private Rigidbody2D rb;

    [SerializeField] private bool isChasePlayer;    // Chase option
    [SerializeField] private float horizontalSpeed;
    private Transform player;

    [SerializeField] private bool isRotate;         // Rotate option
    [SerializeField] private float rotateForce;

    [SerializeField] private bool isMoveSideWays;   // Launch Horizontal option
    [SerializeField] private float horizontalForce;

    [SerializeField] private bool isStretchable;    // Strech and Shrink option
    [SerializeField] private float strechMaxScale;
    private bool isStretching;
    private Vector3 orgScale;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        orgScale = transform.localScale;
    }
    private void Start()
    {
        if (isRotate) ProjectileRotate();
        if (isMoveSideWays) ProjectileMoveHorizontal();
    }
    private void Update()
    {
        if (isChasePlayer) ProjectileChasePlayer();
        if (isStretchable) ProjectileStretchAndShrink();
    }
    private void FixedUpdate()
    {
        ProjectileMoveVertical();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) ProjectileDestroy();  // Destroy projectile, replace number with (... layer) index
        if (collision.gameObject.layer == 8)
        {
            if (isStretchable) isStretching = !isStretching;
            if (isRotate) rb.angularVelocity = -rb.angularVelocity;
            Helper_SetVelocity(-rb.velocity.x, rb.velocity.y);  // Bouncing effect, replace number with (Wall layer) index
        }
    }


    private void ProjectileMoveVertical()
    {
        Helper_SetVelocity(rb.velocity.x, -verticalSpeed);
    }
    private void ProjectileChasePlayer()
    {
        transform.position += (transform.position.x <= player.position.x ? horizontalSpeed : -horizontalSpeed) * Vector3.right * Time.deltaTime;
    }
    private void ProjectileRotate()
    {
        int ranDir = Random.Range(0, 2);
        rb.AddTorque(ranDir == 0 ? rotateForce : -rotateForce, ForceMode2D.Impulse);
    }
    private void ProjectileMoveHorizontal()
    {
        int ranDir = Random.Range(0, 2);
        rb.AddForce(Vector2.right * (ranDir == 0 ? horizontalForce : -horizontalForce), ForceMode2D.Impulse);
    }
    private void ProjectileStretchAndShrink()
    {
        Vector3 scaleSpeed = Vector3.right * Time.deltaTime * 5;        // 5 is scale speed
        transform.localScale += isStretching ? scaleSpeed : -scaleSpeed;

        if (isStretching && transform.localScale.x >= strechMaxScale * orgScale.x) isStretching = false;
        else if (!isStretching && transform.localScale.x < orgScale.x) isStretching = true;
    }
    private void ProjectileDestroy()
    {
        Destroy(this.gameObject, 0);
    }


    private void Helper_SetVelocity(float x, float y)
    { 
        rb.velocity = new Vector2 (x, y);
    }
}
