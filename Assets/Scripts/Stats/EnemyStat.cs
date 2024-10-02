using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    // Data member
    [SerializeField] float enemyHealth = 100f;

    // Getter and setter
    public float GetEnemyHealth()
    {
        return enemyHealth;
    }

    public void SetEnemyHealth(float new_health)
    {
        enemyHealth = new_health;
    }
}
