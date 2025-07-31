using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragObject : MonoBehaviour
{
    Camera cam;
    Rigidbody2D rb;
    bool dragging = false;
    Collider2D _collider;
    Vector3 offset;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        rb.freezeRotation = true;
        //rb.gravityScale = 1;
    }

    private void OnMouseDown()
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging= false;
        rb.gravityScale = 1;
    }

    private void Update()
    {
        if (dragging)
        {
            transform.position = cam.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
}
