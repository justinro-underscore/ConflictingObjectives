using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int MAX_ITEMS = 10;

    public GameObject coinPrefab; // Prefab for a coin
    
    private int playersInRange = 0; // Counts how many players are in range at one time

    /**
     * Starts the spawner
     */
	void Start ()
    {
        Invoke("SpawnSpawnable", Random.Range(0f, 2f));
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
           GameObject.FindGameObjectsWithTag("Spawnable").Length < MAX_ITEMS)
        {
            Instantiate(coinPrefab, gameObject.transform);
            Invoke("SpawnSpawnable", Random.Range(3f, 6f));
        }
        else
            Invoke("SpawnSpawnable", Random.Range(1f, 4f));
    }
}
