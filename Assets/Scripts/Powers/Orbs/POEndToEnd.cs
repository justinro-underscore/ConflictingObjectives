using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POEndToEnd : PowerOrb
{
    private const string END_TO_END_SPRITE_FILENAME = "PointerEndToEnd";
    private const string POINTER_SPRITE_FILENAME = "Pointer";

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    /**
     * Set the sprite to the end to end sprite
     */
    override
    public void StartOrb()
    {
        pointer.playerPointer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + END_TO_END_SPRITE_FILENAME);
    }

    /**
     * Set the sprite back to normal
     */
    override
    public void EndOrb()
    {
        pointer.playerPointer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + POINTER_SPRITE_FILENAME);
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "EndToEnd";
    }
}