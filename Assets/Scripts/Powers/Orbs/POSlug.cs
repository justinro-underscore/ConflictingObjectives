using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POSlug : PowerOrb
{
    private float lastAngle;

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.magenta;
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
     * Smooths the angle to an obnoxious degree
     */
    override
    public void ApplyEffect()
    {
        float diff = pointer.angle - lastAngle;
        if (diff > 180)
            diff -= 360;
        else if (diff < -180)
            diff += 360;
        pointer.angle = lastAngle + (diff / 20f);
        lastAngle = pointer.angle;
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Slug";
    }
}