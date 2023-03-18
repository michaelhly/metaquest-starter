using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    // Appears in the Inspector view from where you can set the speed
    public float speed;

    // Rigidbody variable to hold the player ball's rigidbody instance
    private Rigidbody rb;

    private InputDevice targetDevice;

    // Called before the first frame update
    void Start()
    {
        // Assigns the player ball's rigidbody instance to the variable
        rb = GetComponent<Rigidbody>();
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Called once per frame
    private void Update()
    {
        // The float variables, moveHorizontal and moveVertical, holds the value of the virtual axes, X and Z.

        // It records input from the XR controller.
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        float moveHorizontal = primary2DAxisValue.x;
        float moveVertical = primary2DAxisValue.y;

        // Vector3 variable, movement, holds 3D positions of the player ball in form of X, Y, and Z axes in the space.
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Adds force to the player ball to move around.
        rb.AddForce(movement * speed * Time.deltaTime);
    }
}