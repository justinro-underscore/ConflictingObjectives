using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [HideInInspector] public float angle; // The angle that the pointer is pointing
                                          //  Up -> 0, Left -> 90, Down -> 180, Right -> 270

    private Transform truePointer; // Reference to the player's true input
    private Transform playerPointer; // Reference to the pointer that is shown

    private bool debugWithKeyboard = false; // Runs the controls with the keyboard
    private bool keyboardDebugDebounce = true; // Handles debounce for vertical keys with keyboard input

    /**
     * Sets up the pointer
     */
	void Start ()
    {
        angle = 0;
        playerPointer = gameObject.transform.Find("Pointer");
        truePointer = gameObject.transform.Find("TruePointer");

        debugWithKeyboard = !Input.GetJoystickNames()[0].Contains("Controller");
    }

    /**
     * Sets the angle, applies the powers, and rotates the pointers
     */
    void Update ()
    {
        SetAngle();
        truePointer.rotation = Quaternion.Euler(0, 0, angle); // Set the true pointer
        ApplyEffects(); // Then add the effects
        playerPointer.rotation = Quaternion.Euler(0, 0, angle); // Then set the player's pointer
    }

    /**
     * Sets the angle of the pointer
     */
    private void SetAngle()
    {
        if (debugWithKeyboard)
            SetAngleKeyboard();
        else
            SetAngleJoystick();

        // Bounds check
        if (angle < 0)
            angle += 360;
        else if (angle >= 360)
            angle -= 360;
    }

    /**
     * Sets the angle of the pointer from the keyboard
     */
    private void SetAngleKeyboard()
    {
        angle -= Input.GetAxis("Horizontal") * 3; // Move the angle
        // Flip the pointer
        if (keyboardDebugDebounce)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                angle -= 180;
                keyboardDebugDebounce = false;
            }
        }
        else
        {
            if (Input.GetAxis("Vertical") == 0)
                keyboardDebugDebounce = true;
        }
    }

    /**
     * Sets the angle of the pointer from the joystick
     */
    private void SetAngleJoystick()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f) // Make sure the user is moving the joystick
            angle = Mathf.Atan2(x * -1, y) * 180f / Mathf.PI;
    }

    /**
     * Applies the effects tat are applied on this player
     */
    private void ApplyEffects()
    {
        // TODO
    }
}
