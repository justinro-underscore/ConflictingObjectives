using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POSteeringWheel : PowerOrb
{
    private float lastAngle;

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    /**
     * Set the initial angle
     */
    override
    public void StartOrb()
    {
        lastAngle = pointer.angle;
    }

    /**
     * Moves the pointer solely from the horizontal movement of the input
     */
    override
    public void ApplyEffect()
    {
        pointer.angle = lastAngle - (Input.GetAxis(pointer.inputStrings.x) * 3);
        lastAngle = pointer.angle;
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "SteeringWheel";
    }
}