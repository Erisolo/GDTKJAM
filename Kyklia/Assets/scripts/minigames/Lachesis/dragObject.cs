using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragObject : MonoBehaviour
{
    Camera cam;
    Rigidbody2D rb;
    bool dragging = false;
    Vector3 offset;
    public int weight;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        //rb.gravityScale = 1;
    }

    public void startDrag()
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
        dragging = true;
    }

    public void stopDrag()
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
