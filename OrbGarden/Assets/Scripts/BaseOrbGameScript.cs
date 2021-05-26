using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOrbGameScript : MonoBehaviour
{
    protected void SpawnThenDestroyParticle(GameObject particleObject, Transform spawnTransform, bool useRotation = true)
    {
        GameObject ParticleObjectSpawned = null;

        if (useRotation == true)
        {
            ParticleObjectSpawned = Instantiate(particleObject, new Vector2(spawnTransform.position.x, spawnTransform.position.y), spawnTransform.rotation);
        }
        else
        {
            ParticleObjectSpawned = Instantiate(particleObject, new Vector2(spawnTransform.position.x, spawnTransform.position.y), new Quaternion(0, 0, 0, 0));
        }


        float ParticleObjectSpawnedDuration = ParticleObjectSpawned.GetComponent<ParticleSystem>().main.duration + ParticleObjectSpawned.GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
        Destroy(ParticleObjectSpawned, ParticleObjectSpawnedDuration);
    }
}
