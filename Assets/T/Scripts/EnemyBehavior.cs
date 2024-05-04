using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool isRotate;
    [SerializeField] private float rotateForce;
    [SerializeField] private bool isMoveSideWays;
    [SerializeField] private float horizontalForce;
    [SerializeField] private bool isStretchable;
    [SerializeField] private float strechMaxScale;  // Note: Different scale between testing and merging
    private bool scaleDir;
    private Vector3 orgScale;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        orgScale = transform.localScale;
    }
    /* 
    Requirement: Object must have a Rigidbody2D and no contraint is selected
    How it work:
    Apply a rotational force when spawn, gravity and bouncy(optional) will handle the rest
    Apply a horizontal force when spawn, it will also decide which angle it will go (Recommend lower speed for larger object)
     */
    private void Start()
    {
        if(isRotate) rb.AddTorque(rotateForce, ForceMode2D.Impulse);
        if(isMoveSideWays) rb.AddForce(Vector2.right * horizontalForce, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (isStretchable) StretchAndShrink();
    }
    private void StretchAndShrink()
    {
        Vector3 scaleSpeed = Vector3.right * Time.deltaTime * 6;    // Note: Different scale between testing and merging
        transform.localScale += scaleDir ? scaleSpeed : -scaleSpeed;

        if (scaleDir && transform.localScale.x >= strechMaxScale) scaleDir = false;
        else if(!scaleDir && transform.localScale.x < orgScale.x) scaleDir = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        scaleDir = !scaleDir;
    }
}
