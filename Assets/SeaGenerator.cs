using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGenerator : MonoBehaviour
{
    static int width = 20;
    static int height = 20;

    [SerializeField] GameObject water;
    [SerializeField] Lighthouse lightHouse;
    //[SerializeField] GameObject[] waterArr = new GameObject[width * height];
    List<GameObject> waterList = new List<GameObject>();
    List<Lighthouse> lightHouseList = new List<Lighthouse>();

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
        GameObject lightHouseParent = new GameObject("LIGHTHOUSES");
        for (int i = 0; i < width * height; i++)
        {
            InitWater(i, seaGridParent);
            InitLightHouses(i, lightHouseParent);
        }
    }

    void InitWater(int i, GameObject parent)
    {
        float x = (i % width) * 7;
        float z = (i / width) * 7;
        var w = Instantiate(water);
        w.transform.position = new Vector3(x, 0, z);
        w.transform.SetParent(parent.transform);
        w.name += i.ToString();
        waterList.Add(w);
    }

    void InitLightHouses(int i, GameObject parent)
    {
        if (i % 25 == 0)
        {
            float x = (i % width) * 7;
            float z = (i / width) * 7;
            var w = Instantiate(lightHouse);

            w.Init();

            w.transform.position = new Vector3(x, 0, z);
            w.transform.SetParent(parent.transform);
            w.name += i.ToString();
            lightHouseList.Add(w);
        }
    }
}
