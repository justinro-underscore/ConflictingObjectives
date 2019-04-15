using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POClock : PowerOrb
{
    private int offset = 0;

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    /**
     * Slowly adds a radial offset to the pointer angle
     */
    override
    public void ApplyEffect()
    {
        offset++;
        pointer.angle = pointer.angle - (offset / 2f);
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Clock";
    }
}