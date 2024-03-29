using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public bool Driver { get; set; } = false;
    //public GetDriver(Get,)
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
        if (Driver)
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
        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            //rudder_Y = rudder.localEulerAngles.y + 1f;
            rudder_Y = rudder.localEulerAngles.y + movementSpeed * 50f * Time.deltaTime;

            if (rudder_Y > 30f && rudder_Y < 270f)
            {
                rudder_Y = 30f;
            }

            Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);

            rudder.localEulerAngles = newRotation;
        }
        //Steer right
        else if (Input.GetKey(KeyCode.A) ||
                 Input.GetKey(KeyCode.LeftArrow))
        {
            //rudder_Y = rudder.localEulerAngles.y - 1f;
            rudder_Y = rudder.localEulerAngles.y - movementSpeed * 50f * Time.deltaTime;

            if (rudder_Y < 330f && rudder_Y > 90f)
            {
                rudder_Y = 330f;
            }

            Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);

            rudder.localEulerAngles = newRotation;
        }
        else
        {
            RudderRotateToCenter();
        }
    }

    void RudderRotateToCenter()
    {
        rudder_Y = rudder.localEulerAngles.y;

        if (rudder_Y < 0.25f || rudder_Y > 359.75f) return;

        if (rudder_Y > 0f && rudder_Y < 180f)
        {
            rudder_Y = Mathf.Lerp(rudder_Y, 0f, Time.deltaTime * movementSpeed);
        }
        else if (rudder_Y >= 180f)
        {
            rudder_Y = Mathf.Lerp(rudder_Y, 360f, Time.deltaTime * movementSpeed);
        }

        Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);
        rudder.localEulerAngles = newRotation;



        //if (rudder_Y > -0.5f && rudder_Y < 0.5f)
        //{
        //    return;
        //}

        //if (rudder_Y <= 180)
        //    rudder_Y = rudder.localEulerAngles.y - movementSpeed * 10f * Time.deltaTime;
        //else if (rudder_Y > 180)
        //    rudder_Y = rudder.localEulerAngles.y + movementSpeed * 10f * Time.deltaTime;

        //print(rudder_Y);

        //Vector3 newRotation = new Vector3(0f, rudder_Y, 0f);

        //rudder.localEulerAngles = newRotation;
    }
}
