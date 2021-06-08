using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public float runMultiplier;
    public float gravity = -9.81f;    
    Vector3 velocity; 
    private CharacterController characterController;

    public LayerMask interactable;
    private RaycastHit lookInfo;
    public GameObject lookedAtObject;  

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        LookCast();
        Interact();
        Movement();
    }
    private void LookCast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out lookInfo, 6.5f, interactable))
        {
            lookedAtObject = lookInfo.collider.gameObject;
            Debug.DrawRay(transform.position, transform.forward * 6.5f, Color.green);
        }
        else
            lookedAtObject = null;
    }
    void Interact()
    {
        if (lookedAtObject != null)
        {
            if (lookedAtObject.name == "Boat" && Input.GetKeyDown(KeyCode.E))
            {
                print("Getting on boat");
                var lookedAtBoat = lookedAtObject.GetComponent<Boat>();
                lookedAtBoat.Driver = true;
                transform.SetParent(lookedAtObject.transform);
            }
        }
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.SetParent(transform);
        }

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        characterController.Move(movement * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.Move(movement * Time.deltaTime * runMultiplier);
        }
    }
}
