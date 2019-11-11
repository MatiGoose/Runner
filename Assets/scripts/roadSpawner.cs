using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadSpawner : MonoBehaviour
{
    public GameObject[] RoadBlockPrefabs; //префабы дорог
    public GameObject startBlock; //стартовоый блок дороги

    float blockZpos = 0; //
    int blockCount = 2; //количество изначально генерируемых блоков
    float blockLength = 0; //длина блока
    float safeZone = 170; //

    public Transform PlayerTransf;
    List<GameObject> CurrentBlock = new List<GameObject>();

    void Start()
    {
        blockZpos = startBlock.transform.position.z;
        blockLength = startBlock.GetComponent<BoxCollider>().bounds.size.z;
        for (int i = 0; i < blockCount; i++)
            SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpawn();
    }
    void SpawnBlock()
    {
        GameObject block = Instantiate(RoadBlockPrefabs[Random.Range(0, RoadBlockPrefabs.Length)], transform);

        blockZpos += blockLength;

        block.transform.position = new Vector3(0, 0, blockZpos);

        CurrentBlock.Add(block);
    }
    void CheckForSpawn()
    {
        if(PlayerTransf.position.z - safeZone > (blockZpos - blockCount * blockLength))
        {   
            SpawnBlock();
            DestroyBlock();
        }
    }
    void DestroyBlock()
    {
        Destroy(CurrentBlock[0].gameObject);
        CurrentBlock.RemoveAt(0);
    }
}

