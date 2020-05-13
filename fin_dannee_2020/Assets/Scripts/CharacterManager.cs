using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float moveSpeed;
    private float x, z;

    Vector3 velocity;
    public float gravity = -9.81f;

    public CharacterController playerControl;
    public bool isInactive = false;

    void Update()
    {
        if (!isInactive)
            Movement();
    }

    void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        playerControl.Move(move.normalized * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        playerControl.Move(velocity * Time.deltaTime);
    }
}
