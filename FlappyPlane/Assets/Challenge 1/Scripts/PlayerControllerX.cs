using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Joystick joystick;

    public float GetVerticalDir()
    {
        return joystick.Vertical * -1;
    }

    public Vector3 GetHorizontalDir()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        return horizontalInput * Vector3.right;
    }

    void FixedUpdate()
    {
        // move the plane forward at a constant rate
        transform.Translate(Time.deltaTime * speed * Vector3.forward);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Time.deltaTime * rotationSpeed * GetVerticalDir(), 0, 0);
    }
}