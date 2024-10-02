using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStat : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;  // Speed of the bullet
    [SerializeField] float fireRate = 1.0f; // Time in seconds between each shot
    [SerializeField] float bulletDamage = 50f; 
  
    // Getter and setter
    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetBulletFireRate()
    {
        return fireRate;
    }

    public float GetBulletDamage()
    {
        return bulletDamage;
    }

    public void SetBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed; 
    }

    public void SetBulletFireRate(float newRate)
    {
        fireRate = newRate;
    }

    public void SetBulletDamage(float newBulletDamage)
    {
        bulletDamage = newBulletDamage;
    }
}
