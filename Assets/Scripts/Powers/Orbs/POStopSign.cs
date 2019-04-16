using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POStopSign : PowerOrb
{
    private float lastAngle;

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    /**
     * Restricts movement to left, right, up, down, and halfway inbetween each
     */
    override
    public void ApplyEffect()
    {
        pointer.AngleBoundsCheck();

        if (pointer.angle >= 0 && pointer.angle < 45)
            pointer.angle = 22.5f;
        else if (pointer.angle < 90)
            pointer.angle = 67.5f;
        else if (pointer.angle < 135)
            pointer.angle = 112.5f;
        else if (pointer.angle < 180)
            pointer.angle = 157.5f;
        else if (pointer.angle < 225)
            pointer.angle = 202.5f;
        else if (pointer.angle < 270)
            pointer.angle = 247.5f;
        else if (pointer.angle < 315)
            pointer.angle = 292.5f;
        else
            pointer.angle = 337.5f;
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Slug";
    }
}