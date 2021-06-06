using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGenerator : MonoBehaviour
{
    static int width = 10;
    static int height = 10;

    [SerializeField] GameObject water;
    //[SerializeField] GameObject[] waterArr = new GameObject[width * height];
    List<GameObject> waterList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // generate sea
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        GameObject seaGridParent = new GameObject("SEAGRID");
        for (int i = 0; i < width * height; i++)
        {
            float x = (i % width) * 7;
            float z = (i / width) * 7;

            var w = Instantiate(water);

            w.transform.position = new Vector3(x, 0, z);
            w.transform.SetParent(seaGridParent.transform);

            w.name += i.ToString();

            waterList.Add(w);
        }
    }
}
