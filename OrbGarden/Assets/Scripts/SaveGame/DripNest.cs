using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripNest : BaseOrbGameScript
{
    [SerializeField]
    private float maxSpawnTime;
    [SerializeField]
    private int maxSpawnNumber;
    private float spawnTime;
    [SerializeField]
    private GameObject orbToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = maxSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnTime <= 0)
        {
            spawnTime = maxSpawnTime;
            spawnOrb();
        }
        else
        {
            spawnTime = spawnTime - Time.deltaTime;
        }
    }

    private void spawnOrb()
    {
        if (maxSpawnNumber > 0)
        {
            GameObject spawnedOrb = Instantiate(orbToSpawn, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            spawnedOrb.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-30,30);
            maxSpawnNumber = maxSpawnNumber - 1;
        }
    }
}
