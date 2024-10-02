using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    GameObject gun;
    GameObject player;

    void Start()
    {
        if (gun == null)
            gun = GameObject.Find("Gun"); // Alternatively, replace "Gun" with the name of your gun GameObject
        if (player == null)
            player = GameObject.Find("PlayerSetup");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            EnemyStat enemyStat = gameObject.GetComponent<EnemyStat>();
            GunStat gunStat = gun.GetComponent<GunStat>();
            PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();

            float enemyHealth = enemyStat.GetEnemyHealth();
            float gunDamage = gunStat.GetBulletDamage();
            enemyStat.SetEnemyHealth(enemyHealth -= gunDamage);

            if (enemyHealth <= 0)
            {
                playerShooting.SetEnemyDetectionStatus(false);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
