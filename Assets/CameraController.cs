using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] MouseLook walkMoveCam;
    [SerializeField] RotateAroundCam boatMoveCam;

    [SerializeField] Camera firstPersonCam;
    [SerializeField] Camera thirdPersonCam;

    [SerializeField] Boat boat;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCam.gameObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if (boat.Driver && !thirdPersonCam.gameObject.activeSelf)
        {
            firstPersonCam.gameObject.SetActive(false);
            thirdPersonCam.gameObject.SetActive(true);
        }
        else if (!boat.Driver && !firstPersonCam.gameObject.activeSelf)
        {
            thirdPersonCam.gameObject.SetActive(false);
            firstPersonCam.gameObject.SetActive(true);
        }
    }
}
