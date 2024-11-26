using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openworld_player_movement : MonoBehaviour
{

    Rigidbody2D body;

    public float screenborder;

    public float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //movement-horizontal

        float verticalInput = Input.GetAxis("Vertical"); //movement vertical
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}