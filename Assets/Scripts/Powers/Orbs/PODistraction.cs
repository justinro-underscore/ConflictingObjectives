using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PODistraction : PowerOrb
{
    private GameObject distractPointer; // Keeps track of the distract pointer gameobject
    private float distractAngle; // Determines where the pointer's angle is
    private int sin; // Used to create smooth waving

    override
    protected void SetSprite()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    /**
     * Creates a new pointer that will move around wildly
     */
    override
    public void StartOrb()
    {
        distractPointer = Instantiate(pointer.playerPointer.gameObject, pointer.transform);
        distractPointer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Pointer"); // Make sure we are still a pointer
        distractAngle = distractPointer.transform.rotation.z;
        sin = 0;
    }

    /**
     * Wave the distraction pointer
     */
    override
    public void ApplyEffect()
    {
        distractAngle += (Random.Range(0, 10) * Mathf.Sin(sin++ / 60f));
        if (distractAngle < 0)
            distractAngle += 360;
        else if (distractAngle >= 360)
            distractAngle -= 360;
        distractPointer.transform.rotation = Quaternion.Euler(0, 0, distractAngle);
    }

    /**
     * Destroy the distraction pointer
     */
    override
    public void EndOrb()
    {
        Destroy(distractPointer);
    }

    override
    public string ToString()
    {
        return POWER_ORB_PREFIX + "Distraction";
    }
}