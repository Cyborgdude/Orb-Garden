using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuser : BasicOrb
{
    [SerializeField]
    private float defuseRange;
    [SerializeField]
    private GameObject defusePS;
    [SerializeField]
    private AudioClip defuseSound;

    [SerializeField]
    private GameObject largeHollowPF, smallHollowPF, redKeyPF, blueKeyPF, yellowKeyPF, greenKeyPF, stonePlatformerPF, sandstonePlatformerPF;

    public override void Use()
    {
        if (useCooldown <= 0)
        {
            base.Use();
            useCooldown = maxUseCooldown;
            SpawnThenDestroyParticle(defusePS, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(defuseSound, SoundType.majorSFX, 1);
            Defuse();
        }
        else
        {
            FailUse();
        }
    }

    private void Defuse()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, defuseRange);
        foreach (Collider2D nearbyObject in nearbyObjects)
        {

            if (nearbyObject.gameObject.tag == "Orb")
            {
                if(nearbyObject.gameObject.GetComponent<BasicOrb>().GetPrecursorOne() != null || nearbyObject.gameObject.GetComponent<BasicOrb>().GetPrecursorTwo() != null)
                {
                    GameObject oldOrb1 = nearbyObject.gameObject.GetComponent<BasicOrb>().GetPrecursorOne();
                    GameObject oldOrb2 = nearbyObject.gameObject.GetComponent<BasicOrb>().GetPrecursorTwo();
                    oldOrb1.transform.position = new Vector2(nearbyObject.transform.position.x, nearbyObject.transform.position.y);
                    oldOrb2.transform.position = new Vector2(nearbyObject.transform.position.x, nearbyObject.transform.position.y);
                    Destroy(nearbyObject.gameObject);
                    oldOrb1.SetActive(true);
                    oldOrb1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5, 11), Random.Range(5, 10)), ForceMode2D.Impulse);
                    oldOrb2.SetActive(true);
                    oldOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, -6), Random.Range(5, 10)), ForceMode2D.Impulse);
                }
                else
                {
                    //Use seperate splitter Logic
                    OrbType orbType = nearbyObject.GetComponent<BasicOrb>().GetOrbType();
                    switch(orbType)
                    {
                        case OrbType.whole:
                            SpawnOldOrbs(largeHollowPF, smallHollowPF, nearbyObject.gameObject);
                            break;
                        case OrbType.rainbowKey:
                            SpawnOldOrbs(greenKeyPF, redKeyPF, nearbyObject.gameObject);
                            break;
                        case OrbType.purpleKey:
                            SpawnOldOrbs(redKeyPF, blueKeyPF, nearbyObject.gameObject);
                            break;
                        case OrbType.greenKey:
                            SpawnOldOrbs(yellowKeyPF, blueKeyPF, nearbyObject.gameObject);
                            break;
                        case OrbType.orangeKey:
                            SpawnOldOrbs(redKeyPF, yellowKeyPF, nearbyObject.gameObject);
                            break;
                        case OrbType.obsidianPlatformer:
                            SpawnOldOrbs(sandstonePlatformerPF, sandstonePlatformerPF, nearbyObject.gameObject);
                            break;
                        case OrbType.sandstonePlatformer:
                            SpawnOldOrbs(stonePlatformerPF, stonePlatformerPF, nearbyObject.gameObject);
                            break;
                        default:
                            //Should never reach default
                            break;
                    }
                }


            }
        }
    }

    private void SpawnOldOrbs(GameObject oldOrbPrefab1, GameObject oldOrbPrefab2, GameObject nearbyObject)
    {
        GameObject spawnedOrb1 = Instantiate(oldOrbPrefab1, nearbyObject.transform.position, nearbyObject.transform.rotation);
        spawnedOrb1.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5, 11), Random.Range(5, 10)), ForceMode2D.Impulse);
        GameObject spawnedOrb2 = Instantiate(oldOrbPrefab2, nearbyObject.transform.position, nearbyObject.transform.rotation);
        spawnedOrb2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-10, -6), Random.Range(5, 10)), ForceMode2D.Impulse);
        Destroy(nearbyObject);
    }


}
