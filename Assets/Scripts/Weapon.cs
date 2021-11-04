using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool;
    public Transform muzzle;

    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;

    public float shootRate;
    private float lastShootTime;
    private bool isPlayer;

    public AudioClip shootSfx;
    private AudioSource audioSource;

    private void Awake()
    {
        if (GetComponent<Player>())
            isPlayer = true;

        audioSource = GetComponent<AudioSource>();
    }

    public bool CanShoot()
    {
        return Time.time - lastShootTime >= shootRate && (currentAmmo > 0 || infiniteAmmo);
    }

    public void Shoot()
    {
        lastShootTime = Time.time;
        currentAmmo--;

        if (isPlayer)
            GameUI.instance.UpdateAmmoText(currentAmmo, maxAmmo);

        audioSource.PlayOneShot(shootSfx);

        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;

    }
}
