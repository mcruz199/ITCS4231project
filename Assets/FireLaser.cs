using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public float lastFire;
    public float fireRate;
    public AudioSource hitSource;
    public AudioClip hit;
    public GameObject laserPrefab;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > lastFire + fireRate)
        {
            hitSource.clip = hit;
            hitSource.Play();
            lastFire = Time.time;
            fireLaser();
        }
    }

    // Update is called once per frame
    void fireLaser()
    {
        GameObject laser = GameObject.Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
    }
}
