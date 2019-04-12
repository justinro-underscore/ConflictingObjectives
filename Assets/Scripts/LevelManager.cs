using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject playerPointer; // Prefab of a player pointer
    public GameObject level; // Prefab of a level

    /**
     * Sets up the level
     */
	void Awake ()
    {
        // Check if we have controllers connected
        //  If not, we are in debug mode
        //  TODO Change
        bool debug = true;
        if (Input.GetJoystickNames().Length > 0)
            debug = !Input.GetJoystickNames()[0].Contains("Controller");

        // Set up the players
        int numPlayers = 4; // Number of players, [1,4]
        for (int num = 1; num <= numPlayers; num++)
        {
            Vector3 pointerPos = new Vector3(5.65f, 1.6f);
            switch (num)
            {
                case 1:
                    pointerPos.x *= -1;
                    break;
                case 2:
                    break;
                case 3:
                    pointerPos.x *= -1;
                    pointerPos.y *= -1;
                    break;
                case 4:
                    pointerPos.y *= -1;
                    break;
            }
            GameObject p = Instantiate(playerPointer, pointerPos, Quaternion.identity);
            p.GetComponentInChildren<PlayerPointer>().SetupPlayer(num, (debug ? PlayerPointer.InputType.KEYBOARD : PlayerPointer.InputType.ALL_TOGETHER_NOW));
        }

        // Create the level
        if (GameObject.Find("Level") == null)
            Instantiate(level);
    }
}
