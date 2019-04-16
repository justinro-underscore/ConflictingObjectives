using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POFlip : PowerOrb
{
    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    /**
     * Set the pointer to the background sprite
     */
    override
    public void StartOrb()
    {
        pointer.playerPointer.rotation = Quaternion.Euler(0, 0, 0);
        pointer.playerPointer = pointer.transform.Find("Background");
    }

    /**
     * Sets the pointer back to the pointer sprite
     */
    override
    public void EndOrb()
    {
        pointer.playerPointer.rotation = Quaternion.Euler(0, 0, 0);
        pointer.playerPointer = pointer.transform.Find("Pointer");
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Flip";
    }
}