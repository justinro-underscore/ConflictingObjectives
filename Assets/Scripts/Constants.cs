using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    /**
     * Defines the names of all power orb scripts
     *  When creating a new power orb script, make sure their
     *  name is added here.
     *  Linked with Spawner.AddOrbScripts(GameObject, int)
     */
    public static string[] POWER_ORB_SCRIPTS =
    {
        "POMirror",
        "POClock",
        "POFlip",
        "POSlug",
        "POArcade",
        "POStopSign",
        "PODistraction",
        "POEndToEnd",
        "POSteeringWheel",
    };

    // Percentage of spawnables -> SHOULD ADD TO 100
    public const int PERCENTAGE_COIN = 60;
    public const int PERCENTAGE_POWER_ORB = 40;
}
