using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles all movement of the player's sprite
 *   Player has no direct input on this sprite
 */
public class PlayerSprite : MonoBehaviour
{
    private PlayerPointer pointer; // Reference to the pointer of the player

    private Rigidbody2D rb2d; // Reference to the rigidbody
    private int speed; // How fast the player can move

    /**
     * Setup the player's sprite and its speed
     */
	void Start ()
    {
        pointer = gameObject.transform.parent.Find("PlayerPointer").GetComponent<PlayerPointer>();
        rb2d = GetComponent<Rigidbody2D>();
        speed = 2;
    }
	
    /**
     * Move the player
     */
	void Update ()
    {
        float angleRad = pointer.angle * Mathf.PI / 180f; // Convert to radians
        rb2d.velocity = new Vector2(Mathf.Sin(angleRad) * -1, Mathf.Cos(angleRad)) * speed;
    }

    /**
     * If the player runs into a spawnable, consume it
     * @param other The other object it collides with
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spawnable"))
        {
            if (other.gameObject.name.Contains("Coin"))
                pointer.AddScore(1);
            Destroy(other.gameObject);
        }
    }

    /**
     * @return The player's index
     */
    public int GetPlayerNum()
    {
        return pointer.playerNum;
    }
}
