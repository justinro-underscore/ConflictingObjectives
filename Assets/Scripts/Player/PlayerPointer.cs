using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPointer : MonoBehaviour
{
    private struct InputStrings
    {
        public string x;
        public string y;
    }

    public GameObject playerSprite; // Prefab for player sprite
    [HideInInspector] public int playerNum;
    [HideInInspector] public float angle = 0; // The angle that the pointer is pointing
                                              //  Up -> 0, Left -> 90, Down -> 180, Right -> 270
    private float lastAngle = 0; // Keeps track of the last angle user input

    private Transform playerPointer; // Reference to the pointer that is shown
    private Text scoreText;

    private List<PowerOrb> effects; // List of effects the user is suffering from
    private const int MAX_EFFECTS = 4; // The max amount of effects the user can be suffering from

    private InputStrings inputStrings;
    private int score;

    /**
     * Sets up the pointer
     */
	void Start ()
    {
        playerPointer = gameObject.transform.Find("Pointer");
        effects = new List<PowerOrb>();
        scoreText = gameObject.transform.parent.Find("Canvas/Score").GetComponent<Text>();
        score = 0;
    }

    /**
     * Sets up all of the player information
     * @param num The index of the player
     * @param input How the player should be controlling their pointer
     * @return True if successful
     */
    public bool SetupPlayer(int num, LevelManager.InputType input)
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
        lastAngle = angle;
        GameObject pSprite = Instantiate(playerSprite, gameObject.transform.position + spritePos, Quaternion.identity, gameObject.transform.parent);
        pSprite.GetComponent<SpriteRenderer>().color = playerColor;
        transform.Find("Pointer").GetComponent<SpriteRenderer>().color = playerColor;

        // Sets the pointer's input strings (this is how we will receive input)
        inputStrings = new InputStrings();
        string suffix = "";
        switch (input)
        {
            case LevelManager.InputType.TRY_HARDS:
                suffix = "MainJoy" + playerNum;
                break;
            case LevelManager.InputType.TWO_FOR_PRICE_OF_ONE:
                suffix = (playerNum % 2 != 0 ? "MainJoy" : "SndJoy") + ((playerNum + 1) / 2);
                break;
            case LevelManager.InputType.ALL_TOGETHER_NOW:
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
            case LevelManager.InputType.KEYBOARD:
            default:
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
        {
            angle = Mathf.Atan2(x * -1, y) * 180f / Mathf.PI;
            lastAngle = angle;
        }
        else
            angle = lastAngle;
        ApplyEffects(); // Add the effects

        // Bounds check
        if (angle < 0)
            angle += 360;
        else if (angle >= 360)
            angle -= 360;
    }

    /**
     * Applies the effects that are applied on this player
     */
    private void ApplyEffects()
    {
        foreach (PowerOrb p in effects)
        {
            p.ApplyEffect();
        }
    }

    /**
     * Adds an amount to the player's score
     * @param num The amount to add to the score
     */
    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score: " + score;
    }

    /**
     * Adds a power orb to the player's effects
     * @param orb The power orb to add
     */
    public void AddPower(GameObject orb)
    {
        PowerOrb p = orb.GetComponent<PowerOrb>();
        PrepareForPowerOrb(p);

        // Add the power orb
        p.SetPlayer(this);
        effects.Add(p);
        orb.transform.parent = transform;
        orb.transform.localScale *= 2;
        UpdateEffectsUI();
    }

    /**
     * Prepares to insert a power orb
     *  If we have a duplicate, delete the existing power orb
     *  else, if we have too many, delete the first effect
     * @param power The power orb that is attempting to be added
     */
    private void PrepareForPowerOrb(PowerOrb power)
    {
        // Check for duplicates
        foreach (PowerOrb p in effects)
        {
            if(p.ToString() == power.ToString())
            {
                RemovePower(p);
                return;
            }
        }
        // Check for overflow
        if (effects.Count >= MAX_EFFECTS)
            RemovePower(effects[0]);
    }

    /**
     * Updates the effects UI
     */
    private void UpdateEffectsUI()
    {
        for(int i = 0; i < effects.Count; i++)
        {
            effects[i].gameObject.transform.position = transform.position + new Vector3((i - 2) / 2f, 2);
        }
    }

    /**
     * Removes a power orb from the player's effects
     * @param p The power orb to remove
     */
    public void RemovePower(PowerOrb p)
    {
        // Get rid of power orb
        effects.Remove(p);
        Destroy(p.gameObject);
        UpdateEffectsUI();

        angle = lastAngle; // Reset angle
        ApplyEffects(); // Apply any remaining effects
        playerPointer.rotation = Quaternion.Euler(0, 0, angle); // Then set the player's pointer
    }
}
