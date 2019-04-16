using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PowerOrb : MonoBehaviour
{
    protected const string POWER_ORB_PREFIX = "PO"; // Used with ToString method
    protected PlayerPointer pointer = null; // Reference to the player pointer that has this effect

    private Image timer; // Shows how much time is left on this power orb
    private const int EXPIRATION_TIME_SEC = 20; // Defines how long an orb lasts in seconds
    private const int TIMER_STEP = 4; // The amount of steps per second the timer runs
    private int timeRemaining; // How much time is remaining for this orb

    /**
     * Setup the sprite
     */
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        SetSprite();
    }

    /**
     * Defines what sprite the orb should show
     */
    protected abstract void SetSprite();


    /**
     * Sets the player being effected by the orb's effects
     * @param p Reference to the player that has this orb
     */
    public void SetPlayer(PlayerPointer p)
    {
        LevelManager.instance.RemoveOrb(this); // Remove the orb from the board

        // Set the references
        pointer = p;
        timer = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();

        StartOrb();

        // Start the timer
        timeRemaining = TIMER_STEP * EXPIRATION_TIME_SEC;
        timer.fillAmount = 1;
        InvokeRepeating("UpdateOrb", 1f / TIMER_STEP, 1f / TIMER_STEP);
    }

    /**
     * Checks if the orb has expired & updates the timer graphic
     */
    protected void UpdateOrb()
    {
        timeRemaining--;
        timer.fillAmount = ((float)timeRemaining / (TIMER_STEP * EXPIRATION_TIME_SEC));
        if (timeRemaining == 0)
            pointer.RemovePower(this);
    }

    /**
     * Defines what should happen when the orb is started
     */
    public virtual void StartOrb() { }

    /**
     * Defines what effect should be applied to the player
     */
    public virtual void ApplyEffect() { }

    /**
     * Defines what should happen when the orb is destroyed
     */
    public virtual void EndOrb() { }

    /**
     * Name of the script being run. Used for comparisons
     */
    override
    public abstract string ToString();
}