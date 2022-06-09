using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShoot : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject bullet;
    [SerializeField] float shootForce;
    [SerializeField] float timeBtwShoots;
    bool readyToShoot;
    [SerializeField] Transform attackPoint;
    bool allowInvoke = true;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        readyToShoot = true;
    }
    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            if (readyToShoot)
            {
                Shoot();
            }
        }
    }
    #endregion

    #region Private Methods
    private void Shoot()
    {
        readyToShoot = false;

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(attackPoint.forward * shootForce, ForceMode.Impulse);

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBtwShoots);
            allowInvoke = false;
        }
    }
    void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    #endregion
}
