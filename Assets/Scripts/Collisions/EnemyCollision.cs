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
                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") + 10);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            int playerArmor = PlayerPrefs.GetInt("PlayerArmor");
            int playerHealth = PlayerPrefs.GetInt("PlayerHealth");
            if (playerArmor >= 10)
            {
                playerArmor -= 10;
                PlayerPrefs.SetInt("PlayerArmor", playerArmor);
            } else if (playerArmor < 10 && playerArmor > 0)
            {
                int dif = 10 - playerArmor;
                playerArmor = 0;
                PlayerPrefs.SetInt("PlayerArmor", playerArmor);
                playerHealth -= dif;
                PlayerPrefs.SetInt("PlayerHealth", playerHealth);
            } else if (playerArmor == 0)
            {
                playerHealth -= 10;
                PlayerPrefs.SetInt("PlayerHealth", playerHealth);
            }
            Destroy(gameObject);
        }
    }
}
