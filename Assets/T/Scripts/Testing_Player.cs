using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing_Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float scaleBuffSize;
    private Rigidbody2D rb;
    private Vector3 orgScale;
    private bool isUseBuff;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        orgScale = transform.localScale;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            SizeBuff();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
    }

    private void SizeBuff()
    {
        if (!isUseBuff)
        {
            transform.localScale = transform.localScale / scaleBuffSize;
            moveSpeed *= 2;
            Invoke("RevertSizeBuff", 5f);
            isUseBuff = true;
        }
    }
    private void RevertSizeBuff()
    {
        transform.localScale = orgScale;
        moveSpeed /= 2;
        isUseBuff = false;
    }
}
