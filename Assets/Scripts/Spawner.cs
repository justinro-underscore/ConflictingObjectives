using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab for a coin
    public GameObject powerOrbPrefab; // Prefab for a power orb
    
    private int playersInRange = 0; // Counts how many players are in range at one time

    /**
     * Starts the spawner
     */
	void Start ()
    {
        Invoke("SpawnSpawnable", Random.Range(0f, 1f));
    }

    /**
     * When a player comes within range, stop spawning
     * @param other The other object it collides with
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playersInRange++;
    }

    /**
     * When a player leaves range, start spawning again
     * @param other The other object it collides with
     */
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playersInRange--;
    }

    /**
     * Tries to spawn a spawnable item
     */
    private void SpawnSpawnable ()
    {
        // Check if can spawn
        if(playersInRange == 0 &&
           gameObject.transform.childCount == 0 &&
           GameObject.FindGameObjectsWithTag("Spawnable").Length < LevelManager.MAX_ITEMS_ON_BOARD)
        {
            int rand = (int)Random.Range(0, 100);
            if(rand < Constants.PERCENTAGE_COIN)
                Instantiate(coinPrefab, gameObject.transform);
            else
                AddOrbToBoard();
            Invoke("SpawnSpawnable", Random.Range(3f, 6f));
        }
        else
            Invoke("SpawnSpawnable", Random.Range(1f, 4f));
    }

    /**
     * Tries to add an orb to the board
     */
    private void AddOrbToBoard()
    {
        int index = LevelManager.instance.GetNextPowerOrbIndex();
        if (index < 0) // If we get negative, we cannot add an orb to the board
            Instantiate(coinPrefab, gameObject.transform);
        else
        {
            // Create the power orb
            GameObject powerOrb = Instantiate(powerOrbPrefab, gameObject.transform);
            switch (index)
            {
                case 0:
                    powerOrb.AddComponent<POMirror>();
                    break;
                case 1:
                    powerOrb.AddComponent<POClock>();
                    break;
                case 2:
                    powerOrb.AddComponent<POFlip>();
                    break;
                case 3:
                    powerOrb.AddComponent<POSlug>();
                    break;
                case 4:
                    powerOrb.AddComponent<POArcade>();
                    break;
                case 5:
                    powerOrb.AddComponent<POStopSign>();
                    break;
                default:
                    Debug.Log("ERROR: Power orb not implemented: " + index);
                    Destroy(powerOrb);
                    break;
            }
        }
    }
}
