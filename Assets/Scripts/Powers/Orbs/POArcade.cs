using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POArcade : PowerOrb
{
    private float lastAngle;

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    /**
     * Restricts movement to left, right, up, down
     */
    override
    public void ApplyEffect()
    {
        pointer.AngleBoundsCheck();
        
        if (pointer.angle >= 45 && pointer.angle < 135)
            pointer.angle = 90;
        else if (pointer.angle < 225)
            pointer.angle = 180;
        else if (pointer.angle < 315)
            pointer.angle = 270;
        else
            pointer.angle = 0;
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Arcade";
    }
}