using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    Rigidbody body;
    Vector2 movementInput;
    public Vector2 GetMovementInput() { return movementInput; }

    public float movementSpeed = 0.0f;
    public float turnForce = 4;  // modifying currently not advised
    public float maxAngularVel = 3f;

    [Header("Rudder")]
    [SerializeField] Transform rudder;
    private float rudder_Y = 0f;


    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.maxAngularVelocity = maxAngularVel;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move(movementInput);
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Steer();
    }

    public void Move(Vector2 movementInput)
    {
        // forwards movement
        body.AddForce(movementInput.y * body.transform.forward * (movementSpeed + movementInput.y * 0.25f), ForceMode.Acceleration);

        Vector3 localVelocity = body.transform.InverseTransformDirection(body.velocity);

        // turning
        float yTorque = movementInput.x * turnForce * Mathf.Pow(Mathf.Abs(localVelocity.z), 0.25f) * 0.9f * Mathf.Clamp(localVelocity.z, -1, 1);
        //body.AddTorque(Vector3.up * yTorque, ForceMode.Acceleration);

        //print("yTorque: " + yTorque);
        //yTorque = Mathf.Clamp(yTorque, 0f, 3f);

        //float x = body.angularVelocity.x;
        //float y = body.angularVelocity.y;
        //float z = body.angularVelocity.z;


        //print("y bef clamp: " + y);
        //y = Mathf.Clamp(y, 0f, 2f);
        //print("y aft clamp: " + y);
        //body.angularVelocity = new Vector3(x, y, z);

        body.AddTorque(Vector3.up * yTorque, ForceMode.VelocityChange);

        DriftCompensation(localVelocity);


    }

    private void DriftCompensation(Vector3 localVelocity)
    {
        Vector3 driftDrag = -localVelocity.x * body.transform.right * movementSpeed;
        body.AddForce(driftDrag, ForceMode.Acceleration);
    }

    void Steer()
    {
        //Steer left
        if (Input.GetKey(KeyCode.D))
        {
            rudder_Y = rudder.localEulerAngles.y + 0.1f;

            if (rudder_Y > 30f && rudder_Y < 270f)
            {
                rudder_Y = 30f;
            }

            Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);

            rudder.localEulerAngles = newRotation;
        }
        //Steer right
        else if (Input.GetKey(KeyCode.A))
        {
            rudder_Y = rudder.localEulerAngles.y - 0.1f;

            if (rudder_Y < 330f && rudder_Y > 90f)
            {
                rudder_Y = 330f;
            }

            Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);

            rudder.localEulerAngles = newRotation;
        }
    }
}
