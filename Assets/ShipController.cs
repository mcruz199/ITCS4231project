using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAccel = 2.5f, strafeAccel = 2f, hoverAccel = 2f;
    private float rollInput;
    public float rollSpeed = 90f, rollAccel = 3.5f;

    public float lookRotSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDist;
    TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        trail = gameObject.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if  (Input.GetKey(KeyCode.LeftShift))
        {
            trail.startWidth = 0.5f;
            trail.endWidth = 0.25f;

        }
        else
        {
            trail.startWidth = 0.0f;
            trail.endWidth = 0.0f;

        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDist.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDist.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDist = Vector2.ClampMagnitude(mouseDist, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAccel * Time.deltaTime);

        transform.Rotate(-mouseDist.y * lookRotSpeed * Time.deltaTime,
                        mouseDist.x * lookRotSpeed * Time.deltaTime,
                        rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccel * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAccel * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccel * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) 
        + (transform.up * activeHoverSpeed * Time.deltaTime);
    }
}
