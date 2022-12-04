using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShipAI : MonoBehaviour
{
    public AudioSource hitSource;
    public AudioClip hit;
    public float health = 300f;
    //public GameObject explosion;
    public static float torque = 5f;
    private static float thrust = 60f;
    private Rigidbody rb;
    public Transform player;
    private float endOfLife;
    public Text scoreText;
    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Fly();
    }
    void Fly()
    {
        Vector3 targetDir = player.position - transform.position;

        float xyAngle = vector3AngleOnPlane(player.position, transform.position, transform.forward, transform.up);
        float yzAngle = vector3AngleOnPlane(player.position, transform.position, transform.right, transform.forward);

        if (Mathf.Abs(xyAngle) >= 1f && Mathf.Abs(yzAngle) >= 1f)
        {
            rb.AddRelativeTorque(Vector3.forward * -torque * (xyAngle / Mathf.Abs(xyAngle)));

        }
        else if (yzAngle > 1f)
        {
            rb.AddRelativeTorque(Vector3.right * -torque);

        }
        rb.AddRelativeForce(Vector3.forward * thrust);

    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toOrientation)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toOrientation, planeNormal);

        return projectedVectorAngle;
    }

    public void damage (float damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            hitSource.clip = hit;
            hitSource.Play();
            score.updateScore(100);
            //Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }
}
