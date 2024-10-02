using System.Collections;
using UnityEngine;

public class AirEnemyMovements : MonoBehaviour
{
    // Data members
    [SerializeField] float airEnemySpeed = 1f;
    [SerializeField] float strikeSpeed = 5f;            // Speed for striking towards the player
    [SerializeField] float straightDuration = 2f;       // Time before the enemy starts striking

    GameObject player;
    private Vector2 targetPosition;           // Player's position to strike towards
    private bool isStriking = false;          // Track whether the enemy is in striking phase

    void Start()
    {
        if (player == null)
            player = GameObject.Find("PlayerSetup");
        StartCoroutine(StraightFlight());
    }

    void Update()
    {
        // Update target position to follow the player if striking
        if (isStriking && player != null)
        {
            targetPosition = player.transform.position;
        }
        EnemyAutoMove();
    }

    void EnemyAutoMove()
    {
        if (isStriking)
        {
            // Move towards the player's position when in striking phase
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * strikeSpeed * Time.deltaTime);
        }
        else
        {
            // Move straight left before switching to striking phase
            transform.Translate(Vector2.left * airEnemySpeed * Time.deltaTime);
        }
    }

    IEnumerator StraightFlight()
    {
        // Fly straight for the specified duration
        yield return new WaitForSeconds(straightDuration);

        // Set the striking phase to true
        isStriking = true;

        // Capture the player's current position at the start of the striking phase
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
    }
}
