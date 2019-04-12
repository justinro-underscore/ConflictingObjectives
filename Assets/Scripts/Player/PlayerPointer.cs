using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    public enum InputType
    {
        TRY_HARDS, // Individual controllers
        TWO_FOR_PRICE_OF_ONE, // Two players to one controller
        ALL_TOGETHER_NOW, // Everyone on one controller
        KEYBOARD // Keyboard -- DEBUG ONLY
    };
    private struct InputStrings
    {
        public string x;
        public string y;
    }

    public GameObject playerSprite; // Prefab for player sprite
    [HideInInspector] public int playerNum;
    [HideInInspector] public float angle = 0; // The angle that the pointer is pointing
                                              //  Up -> 0, Left -> 90, Down -> 180, Right -> 270

    private Transform playerPointer; // Reference to the pointer that is shown
    private Text scoreText;

    private InputStrings inputStrings;
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
     * @param num The index of the player
     * @param input How the player should be controlling their pointer
     * @return True if successful
     */
    public bool SetupPlayer(int num, InputType input)
    {
        // Bounds check
        if (num < 0 || num > 4)
            return false;

        // Set the player's index
        playerNum = num;
        // Set the sprite's position and sets the starting angle and color of the pointer
        Color playerColor = Color.white;
        Vector3 spritePos = new Vector3(3f, 1f);
        switch (playerNum)
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

        // Sets the pointer's input strings (this is how we will receive input)
        inputStrings = new InputStrings();
        string suffix = "";
        switch (input)
        {
            case InputType.TRY_HARDS:
                suffix = "MainJoy" + playerNum;
                break;
            case InputType.TWO_FOR_PRICE_OF_ONE:
                suffix = (playerNum % 2 != 0 ? "MainJoy" : "SndJoy") + ((playerNum + 1) / 2);
                break;
            case InputType.ALL_TOGETHER_NOW:
                switch (playerNum)
                {
                    case 1:
                        suffix = "MainJoy1";
                        break;
                    case 2:
                        suffix = "Btns1";
                        break;
                    case 3:
                        suffix = "DPad1";
                        break;
                    case 4:
                        suffix = "SndJoy1";
                        break;
                }
                break;
            case InputType.KEYBOARD:
                Debug.Log("You are in debug mode");
                suffix = "Keyboard";
                break;
        }
        inputStrings.x = "Horiz" + suffix;
        inputStrings.y = "Vert" + suffix;
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
        // Get the angle
        float x = Input.GetAxis(inputStrings.x);
        float y = Input.GetAxis(inputStrings.y);
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f) // Make sure the user is moving the joystick
            angle = Mathf.Atan2(x * -1, y) * 180f / Mathf.PI;

        // Bounds check
        if (angle < 0)
            angle += 360;
        else if (angle >= 360)
            angle -= 360;
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
        scoreText.text = "Score: " + score;
    }
}
