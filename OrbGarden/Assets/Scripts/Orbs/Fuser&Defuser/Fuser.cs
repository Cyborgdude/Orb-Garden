using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuser : BasicOrb
{
    [SerializeField]
    private float fuseRange;
    private GameObject currentClosestOrb;
    private float fuserDiff = 3000f;
    [SerializeField]
    private GameObject orb1;
    [SerializeField]
    private GameObject orb2;
    [SerializeField]
    private GameObject moreOrbsNeededPS;
    [SerializeField]
    private AudioClip moreOrbsNeededSFX;
    [SerializeField]
    private GameObject fusePS;
    [SerializeField]
    private AudioClip fuseSFX;
    [SerializeField]
    private GameObject marked1PS;
    [SerializeField]
    private GameObject marked2PS;
    [SerializeField]
    private AudioClip markedSFX;
    [SerializeField]
    private GameObject deletePS;

    //List of spawnable Prefabs
    [SerializeField]
    private GameObject wastePF, sandstonePlatformerPF, obsidianPlatformerPF, orangeKeyPF, greenKeyPF, purpleKeyPF, rainbowKeyPF, wholePF;



    //Misc
    [SerializeField]
    private GameObject crabDeathPS;
    [SerializeField]
    private AudioClip crabDeathSFX;

    public override void Use()
    {
        if(orb1 != null && orb2 != null)
        {
            if (useCooldown <= 0)
            {
                base.Use();
                useCooldown = maxUseCooldown;
                Fuse();
            }
            else
            {
                FailUse();
            }
        }
        else
        {
            //One of the Orb Slots is not filled
            FillOrbSlot();
        }
    }

    private void Fuse()
    {
        /*
         Debug.Log("I Fused: " + orb1 + " And " + orb2 + " Together");
        */
        OrbType orb1Type = orb1.GetComponent<BasicOrb>().GetOrbType();
        OrbType orb2Type = orb2.GetComponent<BasicOrb>().GetOrbType();

        if(orb1Type == OrbType.defuser || orb2Type == OrbType.defuser || orb1Type == OrbType.trophy || orb2Type == OrbType.trophy)
        {
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(moreOrbsNeededSFX, SoundType.majorSFX, 1);
        }
        //Orbs Below
        else if(orb1Type == OrbType.stonePlatformer || orb2Type == OrbType.stonePlatformer)
        {
            if(orb1Type == OrbType.stonePlatformer && orb2Type == OrbType.stonePlatformer)
            {
                SpawnNewOrb(OrbType.sandstonePlatformer);
            }
            else if(orb1Type == OrbType.sandstonePlatformer || orb2Type == OrbType.sandstonePlatformer)
            {
                SpawnNewOrb(OrbType.sandstonePlatformer);
            }
            else if (orb1Type == OrbType.obsidianPlatformer || orb2Type == OrbType.obsidianPlatformer)
            {
                SpawnNewOrb(OrbType.obsidianPlatformer);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }
        else if (orb1Type == OrbType.sandstonePlatformer || orb2Type == OrbType.sandstonePlatformer)
        {
            if (orb1Type == OrbType.sandstonePlatformer && orb2Type == OrbType.sandstonePlatformer)
            {
                SpawnNewOrb(OrbType.obsidianPlatformer);
            }
            else if (orb1Type == OrbType.obsidianPlatformer || orb2Type == OrbType.obsidianPlatformer)
            {
                SpawnNewOrb(OrbType.obsidianPlatformer);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }

        //Keys Below
        else if (orb1Type == OrbType.redKey || orb2Type == OrbType.redKey)
        {
            
            //RedKey
            if (orb1Type == OrbType.blueKey || orb2Type == OrbType.blueKey)
            {
                SpawnNewOrb(OrbType.purpleKey);
            }
            else if (orb1Type == OrbType.yellowKey || orb2Type == OrbType.yellowKey)
            {
                SpawnNewOrb(OrbType.orangeKey);
            }
            else if (orb1Type == OrbType.greenKey || orb2Type == OrbType.greenKey)
            {
                SpawnNewOrb(OrbType.rainbowKey);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }
        else if (orb1Type == OrbType.blueKey || orb2Type == OrbType.blueKey)
        {
            //Blue Key -Red Key Include
            if (orb1Type == OrbType.yellowKey || orb2Type == OrbType.yellowKey)
            {
                SpawnNewOrb(OrbType.greenKey);
            }
            else if (orb1Type == OrbType.orangeKey || orb2Type == OrbType.orangeKey)
            {
                SpawnNewOrb(OrbType.rainbowKey);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }
        else if (orb1Type == OrbType.yellowKey || orb2Type == OrbType.yellowKey)
        {
            //Yellow Key -Red, Blue Key Include
            if (orb1Type == OrbType.purpleKey || orb2Type == OrbType.purpleKey)
            {
                SpawnNewOrb(OrbType.rainbowKey);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }
        //Hollow Listings
        else if(orb1Type == OrbType.largeHollow || orb2Type == OrbType.largeHollow)
        {
            if(orb1Type == OrbType.smallHollow || orb2Type == OrbType.smallHollow)
            {
                if((orb1.GetComponent<Hollow>().CheckIfLifeless() == false && orb1.GetComponent<Hollow>().CheckIfLarge() == true) || (orb2.GetComponent<Hollow>().CheckIfLifeless() == false && orb2.GetComponent<Hollow>().CheckIfLarge() == true))
                {
                    SpawnThenDestroyParticle(crabDeathPS, orb2.transform);
                    speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(crabDeathSFX, SoundType.voice, 1);
                }
                SpawnNewOrb(OrbType.whole, true);
            }
            else
            {
                //Incompatible Orbs
                SpawnNewOrb(OrbType.waste);
            }
        }

        //Final Step
        else
        {
            //Incompatible Orbs
            SpawnNewOrb(OrbType.waste);
        }

        //Reset Old Ref, passed along to spawned orb
        orb1 = null;
        orb2 = null;
    }

    private void SpawnNewOrb(OrbType orbType, bool skipPrecursors = false)
    {
        GameObject orbPrefab = null;
        //Use orbType to pick spawnable Prefab
        switch(orbType)
        {
            case OrbType.waste:
                orbPrefab = wastePF;
                break;
            case OrbType.sandstonePlatformer:
                orbPrefab = sandstonePlatformerPF;
                break;
            case OrbType.obsidianPlatformer:
                orbPrefab = obsidianPlatformerPF;
                break;
            case OrbType.orangeKey:
                orbPrefab = orangeKeyPF;
                break;
            case OrbType.greenKey:
                orbPrefab = greenKeyPF;
                break;
            case OrbType.purpleKey:
                orbPrefab = purpleKeyPF;
                break;
            case OrbType.rainbowKey:
                orbPrefab = rainbowKeyPF;
                break;
            case OrbType.whole:
                orbPrefab = wholePF;
                break;
        }

        GameObject spawnedOrb = Instantiate(orbPrefab, orb2.transform.position, orb2.transform.rotation);
        SpawnThenDestroyParticle(fusePS, orb2.transform);
        SpawnThenDestroyParticle(deletePS, orb1.transform);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(fuseSFX, SoundType.majorSFX, 1);
        spawnedOrb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(25, 50)), ForceMode2D.Impulse);

        if (skipPrecursors == false)
        {
            //This is for use in the defuser V - WARNING: Wont enable accurate defusing through scene changes
            spawnedOrb.GetComponent<BasicOrb>().SetPrecursors(orb1, orb2);
            orb1.SetActive(false);
            orb2.SetActive(false);
        }
        else
        {
            Destroy(orb1);
            Destroy(orb2);
        }
    }

    private void FillOrbSlot()
    {
        //Reset for Loop
        currentClosestOrb = null;
        fuserDiff = 3000f;

        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, fuseRange);
        foreach (Collider2D nearbyObject in nearbyObjects)
        {
            //Make Sure Object is an Orb that is not already in the fusing system
            if(nearbyObject.gameObject.tag == "Orb" && nearbyObject.gameObject != orb1 && nearbyObject.gameObject != orb2)
            {

                //First run Diff must always be bigger than the distance, each success after this first run lowers the size of Diff meaning the closest value wins out in the end
                if (fuserDiff >
        Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
        new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y))
                    )
                {
                    fuserDiff =
                        Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                        new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y));
                    currentClosestOrb = nearbyObject.gameObject;

                }

            }
        }
        if(currentClosestOrb != null)
        {
            //One orb slot will ALWAYS be empty
            if(orb1 == null)
            {
                //Fill Orb1 Slot
                orb1 = currentClosestOrb;
                SpawnThenDestroyParticle(marked1PS, orb1.transform);
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(markedSFX, SoundType.majorSFX, 1);
            }
            else
            {
                //Fill Orb2 Slot as Orb1 Slot is Full
                orb2 = currentClosestOrb;
                SpawnThenDestroyParticle(marked2PS, orb2.transform);
                SpawnThenDestroyParticle(marked1PS, orb1.transform);
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(markedSFX, SoundType.majorSFX, 1);
            }
        }
        else
        {
            //Need two orbs failure (Range Failure)
            SpawnThenDestroyParticle(moreOrbsNeededPS, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(moreOrbsNeededSFX, SoundType.majorSFX, 1);
        }
    }
}
