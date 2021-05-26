using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whole : BasicOrb, IBlast
{
    [SerializeField]
    private GameObject hollowSpawnPS;
    [SerializeField]
    private GameObject largeHollowPrefab;
    [SerializeField]
    private GameObject smallHollowPrefab;

    public void IBlast(AcCol acCol)
    {
        Break();
    }

    private void Break()
    {
        if (held == false)
        {
            SpawnThenDestroyParticle(hollowSpawnPS, transform);
            GameObject spawnedOrb = Instantiate(largeHollowPrefab, transform.position, transform.rotation);
            GameObject spawnedOrb2 = Instantiate(smallHollowPrefab, transform.position, transform.rotation);
            spawnedOrb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 5), Random.Range(25, 50)), ForceMode2D.Impulse);
            spawnedOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 1), Random.Range(25, 50)), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
