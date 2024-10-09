using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovements : MonoBehaviour
{
    // Data members
    [SerializeField] float groundEnemySpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        EnemyAutoMove();
    }

    void EnemyAutoMove()
    {
        if (PlayerPrefs.GetInt("GamePaused") == 0) {
            transform.Translate(Vector3.left * groundEnemySpeed * Time.fixedDeltaTime);
        }
    }

    public void SetGroundEnemySpeed(float newSpeed)
    {
        groundEnemySpeed = newSpeed;
    }
}
