using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    GameObject gun;
    GameObject player;
    GameObject enemy;

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
            Animator playerAnimator = player.GetComponentInChildren<Animator>();

            float enemyHealth = enemyStat.GetEnemyHealth();
            float gunDamage = gunStat.GetBulletDamage();
            enemyStat.SetEnemyHealth(enemyHealth -= gunDamage);

            if (enemyHealth <= 0)
            {
                playerShooting.SetEnemyDetectionStatus(false);
                playerAnimator.SetBool("isEnemyDetected", false);
                PlayerPrefs.SetInt("PlayerMoney", PlayerPrefs.GetInt("PlayerMoney") + 10);
                if (gameObject.tag == "Boss") {
                    PlayerPrefs.SetInt("CurrentLevelFinished", 1);
                    switch (PlayerPrefs.GetInt("CurrentLevel")) {
                        case 1:
                            PlayerPrefs.SetInt("Level1Complete", 1);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("Level2Complete", 1);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("Level3Complete", 1);
                            break;
                        case 4:
                            PlayerPrefs.SetInt("Level4Complete", 1);
                            break;
                        case 5:
                            PlayerPrefs.SetInt("Level5Complete", 1);
                            break;
                        case 6:
                            PlayerPrefs.SetInt("Level6Complete", 1);
                            break;
                    }
                }
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            if (gameObject.tag == "MediumEnemy" || gameObject.tag == "SmallEnemy") {
                int playerArmor = PlayerPrefs.GetInt("PlayerArmor");
                int playerHealth = PlayerPrefs.GetInt("PlayerHealth");
                int playerVehicleLevel = PlayerPrefs.GetInt("PlayerVehicleLevel");
                int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
                EnemyStat enemyStat = gameObject.GetComponent<EnemyStat>();
                float reductionFactor = 1f - (0.1f * playerVehicleLevel); 
                reductionFactor = Mathf.Clamp(reductionFactor, 0.1f, 1f); 
                int damage = Mathf.CeilToInt(enemyStat.GetEnemyDamage() * reductionFactor);

                if (playerArmor >= damage)
                {
                    playerArmor -= damage;
                    PlayerPrefs.SetInt("PlayerArmor", playerArmor);
                } else if (playerArmor < damage && playerArmor > 0)
                {
                    int dif = damage - playerArmor;
                    playerArmor = 0;
                    PlayerPrefs.SetInt("PlayerArmor", playerArmor);
                    playerHealth -= dif;
                    PlayerPrefs.SetInt("PlayerHealth", playerHealth);
                } else if (playerArmor == 0)
                {
                    playerHealth -= damage;
                    PlayerPrefs.SetInt("PlayerHealth", playerHealth);
                }
                Destroy(gameObject);
            } else if (gameObject.tag == "Boss") {
                PlayerPrefs.SetInt("PlayerHealth", 0);
            }
        }
    }
}
