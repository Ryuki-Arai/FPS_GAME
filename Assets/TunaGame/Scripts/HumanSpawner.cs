using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{

    public GameObject Human;
    /// <summary>1分間の出現頻度</summary>
    [SerializeField] int spawnPm;
    /// <summary>最大出現数</summary>
    [SerializeField] int maxSpawn;
    float time = 0f;
    int count;

    public int MaxHuman { get => maxSpawn; }
    public int HumanCount { get => count; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        count = GameObject.FindGameObjectsWithTag("Human").Length;
        if (60f / spawnPm < time && count < maxSpawn)
        {
            time = 0;
            Instantiate(Human, new Vector3(Random.Range(-20f,20f), 0f, Random.Range(-40f,80f)), Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
        }
        else if(60f / spawnPm < time)
        {
            time = 0;
        }
    }
}
