using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{
    public float jumpForce;

    private Rigidbody2D rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // when player clicks the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("bird jumped");
            // make the bird jump
            rBody.velocity = Vector2.zero;
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
}
