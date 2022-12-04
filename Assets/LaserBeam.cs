using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float speed = 200f;
    private float endOfLife;
    private float force = 1000f;
    public AudioSource hitSource;
    public AudioClip hit;
    public GameObject laserPrefab;
    // Start is called before the first frame update
    void Start()
    {
        endOfLife = Time.time + (150f / speed);
    }

    // Update is called once per frame
    void Update()
    {
        hitSource.clip = hit;
        hitSource.Play();
        if (Time.time > endOfLife) Destroy(laserPrefab);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyShip")
        {

            Debug.Log("hit");
            other.gameObject.GetComponent<EnemyShipAI>().damage(100f);
        }
    }
}

