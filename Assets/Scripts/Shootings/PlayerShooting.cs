using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Data member
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunTransform;
    [SerializeField] GameObject gun;
    [SerializeField] LayerMask enemyLayerMask; // LayerMask for enemies only

    [SerializeField] float firstAirRayDistance = 35f;
    [SerializeField] float secondAirRayDistance = 40f;
    [SerializeField] float thirdAirRayDistance = 45f;
    [SerializeField] float fourthAirRayDistance = 50f;
    [SerializeField] float fifthAirRayDistance = 50f;
    [SerializeField] float sixthAirRayDistance = 30f;
    [SerializeField] float seventhAirRayDistance = 30f;
    [SerializeField] float groundRayDistance = 55f;

    [SerializeField] float firstAirRayAngle = 55f;
    [SerializeField] float secondAirRayAngle = 45f;
    [SerializeField] float thirdAirRayAngle = 35f;
    [SerializeField] float fourthAirRayAngle = 25f;
    [SerializeField] float fifthAirRayAngle = 15f;
    [SerializeField] float sixthAirRayAngle = 60f;
    [SerializeField] float seventhAirRayAngle = 65f;
    [SerializeField] float groundRayAngle = 0f;
    
    GunStat gunStat;
    float nextFireTime = 0f; // Tracks when the gun can shoot next
    bool enemyDetected = false; // Tracks if an enemy is detected
    bool isGroundEnemy = false;
    float activeShootAngle = 0f; // Store the angle of the ray that detected the enemy
    float bulletSpeed;
    float fireRate;
    float bulletDamage;
    int weaponLevelTrack = 1;

    void Start()
    {
        gunStat = gun.GetComponent<GunStat>();
        bulletSpeed = gunStat.GetBulletSpeed();
        fireRate = gunStat.GetBulletFireRate();
        bulletDamage = gunStat.GetBulletDamage();
    }

    public void SetEnemyDetectionStatus(bool newEnemyDetectionStatus)
    {
        enemyDetected = newEnemyDetectionStatus;
    }

    void Update()
    {
        UpdateGunStat();
        RaycastDetection();
        ShootingControl();
        PressToShoot(); // For Testing
    }

    void UpdateGunStat()
    {
        if (PlayerPrefs.GetInt("PlayerWeaponLevel") > weaponLevelTrack && fireRate > 0f)
        {
            ++weaponLevelTrack;
            gunStat.SetBulletFireRate(fireRate - 0.03f);
            fireRate = gunStat.GetBulletFireRate();
        }
        if(fireRate <= 0f)
        {
            gunStat.SetBulletFireRate(0.1f);
        }
        bulletSpeed = gunStat.GetBulletSpeed();
        bulletDamage = gunStat.GetBulletDamage();
        //print($"Fire Rate: {fireRate}");
    }

    void PressToShoot()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            FireBullet();
        }
    }

    void ShootingControl()
    {
        // Check if enough time has passed to shoot again and if an enemy is detected
        if (Time.time >= nextFireTime && enemyDetected)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // Determine the bullet's rotation based on the enemy's position
        Quaternion bulletRotation;

        if (isGroundEnemy)
        {
            // For ground enemies, shoot straight (no rotation needed)
            bulletRotation = gunTransform.rotation;
        }
        else
        {
            // For air enemies, rotate the bullet to match the firstAirRayAngle
            bulletRotation = Quaternion.Euler(0, 0, activeShootAngle);
        }

        // Instantiate the bullet with the determined rotation
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, bulletRotation);

        // Get the Rigidbody2D of the bullet and apply force to move it forward
        Rigidbody2D bullet_rigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bullet_rigidbody != null)
        {
            // Always apply force in the bullet's forward (right) direction
            bullet_rigidbody.AddForce(bullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
        nextFireTime = Time.time + fireRate; // Set the next time the gun can shoot
    }

    void RaycastDetection()
    {
        RaycastHit2D hit;

        // Cast a ray at an adjustable angle to the right and draw it
        Vector2 firstAirDirection = Quaternion.Euler(0, 0, firstAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, firstAirDirection * firstAirRayDistance, Color.red);
        hit = Physics2D.Raycast(gunTransform.position, firstAirDirection, firstAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = firstAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log("Hit enemy at " + activeShootAngle + " degrees: " + hit.collider.name);
        }

        Vector2 secondAirDirection = Quaternion.Euler(0, 0, secondAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, secondAirDirection * secondAirRayDistance, Color.blue);
        hit = Physics2D.Raycast(gunTransform.position, secondAirDirection, secondAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = secondAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log("Hit enemy at " + activeShootAngle + " degrees: " + hit.collider.name);
        }

        Vector2 thirdAirDirection = Quaternion.Euler(0, 0, thirdAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, thirdAirDirection * thirdAirRayDistance, Color.yellow);
        hit = Physics2D.Raycast(gunTransform.position, thirdAirDirection, thirdAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = thirdAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log("Hit enemy at " + activeShootAngle + " degrees: " + hit.collider.name);
        }

        Vector2 fourthAirDirection = Quaternion.Euler(0, 0, fourthAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, fourthAirDirection * fourthAirRayDistance, Color.cyan);
        hit = Physics2D.Raycast(gunTransform.position, fourthAirDirection, fourthAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = fourthAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log("Hit enemy at " + activeShootAngle + " degrees: " + hit.collider.name);
        }

        Vector2 fifthAirDirection = Quaternion.Euler(0, 0, fifthAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, fifthAirDirection * fifthAirRayDistance, Color.white);
        hit = Physics2D.Raycast(gunTransform.position, fifthAirDirection, fifthAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = fifthAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log("Hit enemy at " + activeShootAngle + " degrees: " + hit.collider.name);
        }

        Vector2 sixthAirDirection = Quaternion.Euler(0, 0, sixthAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, sixthAirDirection * sixthAirRayDistance, Color.green);
        hit = Physics2D.Raycast(gunTransform.position, sixthAirDirection, sixthAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = sixthAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log($"Distance {groundRayDistance}");
        }

        Vector2 seventhAirDirection = Quaternion.Euler(0, 0, seventhAirRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, seventhAirDirection * seventhAirRayDistance, Color.green);
        hit = Physics2D.Raycast(gunTransform.position, seventhAirDirection, seventhAirRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = false;
            activeShootAngle = seventhAirRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log($"Distance {groundRayDistance}");
        }

        Vector2 groundDirection = Quaternion.Euler(0, 0, groundRayAngle) * Vector2.right;
        Debug.DrawRay(gunTransform.position, groundDirection * groundRayDistance, Color.green);
        hit = Physics2D.Raycast(gunTransform.position, groundDirection, groundRayDistance, enemyLayerMask);
        if (hit.collider != null && (hit.collider.CompareTag("SmallEnemy") || hit.collider.CompareTag("MediumEnemy") || hit.collider.CompareTag("Boss")))
        {
            isGroundEnemy = true;
            activeShootAngle = groundRayAngle;
            enemyDetected = true; // An enemy is detected, so we can shoot
            //Debug.Log($"Distance {groundRayDistance}");
        }
    }
}