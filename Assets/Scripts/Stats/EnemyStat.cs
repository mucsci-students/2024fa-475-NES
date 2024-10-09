using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    // Data member
    [SerializeField] float enemyHealth = 100f;
    [SerializeField] int enemyDamage = 20;

    // Getter and setter
    public float GetEnemyHealth()
    {
        return enemyHealth;
    }

    public int GetEnemyDamage()
    {
        return enemyDamage;
    }

    public void SetEnemyHealth(float new_health)
    {
        enemyHealth = new_health;
    }

    public void SetEnemyDamage(int new_damage)
    {
        enemyDamage = new_damage;
    }
}
