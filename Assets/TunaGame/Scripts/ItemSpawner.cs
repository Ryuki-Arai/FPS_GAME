using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] int maxSpawn;
    [SerializeField] int spawnPm;
    [SerializeField] GameObject[] items;
    [SerializeField] int[] emissionLate;
    [SerializeField] float posx, posz;
    [SerializeField] int scalex, scalez;
    float time = 0f;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        count = GameObject.FindGameObjectsWithTag("Item").Length;
        if (60f / spawnPm < time && count < maxSpawn)
        {
            time = 0;
            //var idx = (int)Random.Range(0,items.Length);
            Instantiate(items[GetRandomIndex(emissionLate)], new Vector3(Random.Range(-(scalex/2f)+posx, (scalex/2f)+posx), 1f, Random.Range(-(scalez/2f)+posz, (scalez/2)+posz)), Quaternion.identity);
        }
        else if (60f / spawnPm < time)
        {
            time = 0;
        }
    }

    public static int GetRandomIndex(params int[] late)
    {
        var totalLate = late.Sum();
        var value = Random.Range(1, totalLate + 1);
        var retIndex = -1;
        for (var i = 0; i < late.Length; ++i)
        {
            if (late[i] >= value)
            {
                retIndex = i;
                break;
            }
            value -= late[i];
        }
        return retIndex;
    }

    void OnDrawGizmosSelected()
    {
        //出現範囲を赤い線でシーンビューに表示する
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(posx,0,posz), new Vector3(scalex,0,scalez));
    }
}
