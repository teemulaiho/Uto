using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] Transform light;
    [SerializeField] DayNightCycle timeOfDay;

    [SerializeField] float rotateSpeed = 10.0f;

    [SerializeField] GameObject lightBulb1;
    [SerializeField] GameObject lightBulb2;

    public void Init()
    {
        timeOfDay = FindObjectOfType<DayNightCycle>();

        lightBulb1.SetActive(false);
        lightBulb2.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOfDay.GetTime() > 0.75f ||
            timeOfDay.GetTime() < 0.2f)
        {
            if (!lightBulb1.activeInHierarchy)
            {
                lightBulb1.SetActive(true);
            }

            if (!lightBulb2.activeInHierarchy)
            {
                lightBulb2.SetActive(true);
            }

            light.Rotate(0, Time.deltaTime * rotateSpeed, 0);
        }
    }
}
