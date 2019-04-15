using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum InputType
    {
        TRY_HARDS, // Individual controllers
        TWO_FOR_PRICE_OF_ONE, // Two players to one controller
        ALL_TOGETHER_NOW, // Everyone on one controller
        KEYBOARD // Keyboard -- DEBUG ONLY
    };
    public const int MAX_ITEMS_ON_BOARD = 10;
    public static LevelManager instance = null;

    public GameObject playerPointer; // Prefab of a player pointer
    public GameObject level; // Prefab of a level
    
    // Involved with keeping track of what orbs are in the level currently
    private static bool[] orbsOnBoard;
    private static int numOrbsLeft;

    /**
     * Sets up the level
     */
    void Awake ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        // Set up power orbs
        orbsOnBoard = new bool[Constants.POWER_ORB_SCRIPTS.Length];
        numOrbsLeft = Constants.POWER_ORB_SCRIPTS.Length;

        // Check if we have controllers connected
        //  If not, we are in debug mode
        //  TODO Remove
        bool debug = true;
        if (Input.GetJoystickNames().Length > 0)
            debug = !Input.GetJoystickNames()[0].Contains("Controller");

        // Set up the players
        int numPlayers = 1; // Number of players, [1,4]
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
            p.GetComponentInChildren<PlayerPointer>().SetupPlayer(num, (debug ? InputType.KEYBOARD : InputType.TRY_HARDS));
        }

        // Create the level
        if (GameObject.Find("Level") == null)
            Instantiate(level);
    }

    /**
     * Gets the next available orb index
     *  Next available orb index is the index of an orb that is
     *  not on the board currently
     * @return index of the orb, coming from Constants.POWER_ORB_SCRIPTS
     */
    public int GetNextPowerOrbIndex()
    {
        int rand = (int)Random.Range(0, numOrbsLeft);
        for (int i = 0; i < orbsOnBoard.Length; i++)
        {
            if (!orbsOnBoard[i])
            {
                if (rand == 0)
                {
                    orbsOnBoard[i] = true;
                    numOrbsLeft--;
                    return i;
                }
                else
                    rand--;
            }
        }
        return -1;
    }

    /**
     * Removes a power orb from the board
     *  Resets its status on orbsOnBoard array
     * @param p The orb to remove
     */
    public void RemoveOrb(PowerOrb p)
    {
        for (int i = 0; i < Constants.POWER_ORB_SCRIPTS.Length; i++)
        {
            if (Constants.POWER_ORB_SCRIPTS[i].ToString() == p.ToString())
            {
                orbsOnBoard[i] = false;
                numOrbsLeft++;
                return;
            }
        }
    }
}
