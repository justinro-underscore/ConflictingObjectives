using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    public GameObject playerSprite; // Prefab for player sprite
    [HideInInspector] public int playerNum;
    [HideInInspector] public float angle = 0; // The angle that the pointer is pointing
                                              //  Up -> 0, Left -> 90, Down -> 180, Right -> 270

    private Transform playerPointer; // Reference to the pointer that is shown
    private Text scoreText;

    private bool debugWithKeyboard = true; // Runs the controls with the keyboard
    private bool keyboardDebugDebounce = true; // Handles debounce for vertical keys with keyboard input
    private int score;

    /**
     * Sets up the pointer
     */
	void Start ()
    {
        playerPointer = gameObject.transform.Find("Pointer");
        scoreText = gameObject.transform.parent.Find("Canvas/Score").GetComponent<Text>();
        score = 0;
    }

    /**
     * Sets up all of the player information
     * @param playerNum The index of the player
     * @param debug If true, use debug options
     * @return True if successful
     */
    public bool SetupPlayer(int playerNum, bool debug)
    {
        debugWithKeyboard = debug;
        return SetPlayerNum(playerNum);
    }

    /**
     * Sets the player's index
     * @param num The index of the player
     * @return True if successful
     */
    private bool SetPlayerNum(int num)
    {
        // Bounds check
        if (num < 0 || num > 4)
            return false;

        // Set the player's index
        playerNum = num;
        Color playerColor = Color.white;
        // Set the sprite's position and sets the starting angle and color of the pointer
        Vector3 spritePos = new Vector3(3f, 1f);
        switch (num)
        {
            case 1:
                angle = 225;
                playerColor = Color.red;
                break;
            case 2:
                angle = 135;
                spritePos.x *= -1;
                playerColor = Color.blue;
                break;
            case 3:
                angle = 315;
                spritePos.y *= -1;
                playerColor = Color.yellow;
                break;
            case 4:
                angle = 45;
                spritePos.x *= -1;
                spritePos.y *= -1;
                playerColor = Color.green;
                break;
        }
        GameObject pSprite = Instantiate(playerSprite, gameObject.transform.position + spritePos, Quaternion.identity, gameObject.transform.parent);
        pSprite.GetComponent<SpriteRenderer>().color = playerColor;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().color = playerColor;
        return true;
    }

    /**
     * Sets the angle, applies the powers, and rotates the pointers
     */
    void Update ()
    {
        SetAngle(); // Get the angle from the user
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
        float x = Input.GetAxis("Horizontal" + playerNum);
        float y = Input.GetAxis("Vertical" + playerNum);
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

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score: " + num;
    }
}
