using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POMirror : PowerOrb
{
    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    /**
     * Flips horizontal input
     * Left -> Right
     * Right -> Left
     * Up -> Up
     * Down -> Down
     */
    override
    public void ApplyEffect()
    {
        pointer.angle = 360 - pointer.angle;
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Mirror";
    }
}