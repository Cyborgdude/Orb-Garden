using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollow : BasicOrb
{
    [SerializeField]
    private bool isLarge;
    [SerializeField]
    private float succRange = 30;
    [SerializeField]
    private float succPower = 30;
    [SerializeField]
    private GameObject succPS;
    [SerializeField]
    private GameObject grabPS;
    [SerializeField]
    private AudioClip succSound;
    private GameObject nearestHollow = null;
    private float hollowDiff = 3000f;

    [SerializeField]
    private bool isLifeless;
    [SerializeField]
    private float crabSpawnTime;
    [SerializeField]
    private GameObject crabSpawnPS;
    [SerializeField]
    private GameObject crabPrefab;
    [SerializeField]
    private GameObject rangeFailPS;
    [SerializeField]
    private AudioClip rangeFailSFX;


    protected override void Start()
    {
        base.Start();
        if (isLifeless == true)
        {
            Invoke("CrabSpawn", crabSpawnTime);
        }
    }
    public override void grabTrigger()
    {
        base.grabTrigger();
        if(held == true)
        {
            SpawnThenDestroyParticle(grabPS, GameObject.FindGameObjectWithTag("Player").transform);
        }
        else
        {

        }

    }
    public override void Use()
    {
        if (useCooldown <= 0)
        {

            Succ();
        }
        else
        {
            FailUse();
        }
    }

    private void Succ()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, succRange);
        foreach (Collider2D nearbyObject in nearbyObjects)
        {
            if (nearbyObject.gameObject.GetComponent<Hollow>() != null)
            {
                if (nearbyObject.gameObject.GetComponent<Hollow>().isLarge != isLarge)
                {
                    {
                        //First run hollowDiff must always be bigger than the distance, each success after this first run lowers the size of hollowDiff meaning the closest value wins out in the end
                        if (hollowDiff >
                Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y))
                            )
                        {
                            hollowDiff =
                                Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                                new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y));
                            nearestHollow = nearbyObject.gameObject;

                        }



                    }


                }
            }
        }




        if (nearestHollow != null)
        {
            //Either suck or be sucked based on Hollow Type (Smaller sucked to bigger)
            if (isLarge == true)
            {
                //This object is the large Hollow
                Vector2 direction = (transform.position - nearestHollow.transform.position);
                nearestHollow.GetComponent<Rigidbody2D>().AddForce(direction.normalized * succPower, ForceMode2D.Impulse);
                SpawnThenDestroyParticle(succPS, transform);
                SpawnThenDestroyParticle(grabPS, nearestHollow.transform);
            }
            else
            {
                //This object is the small Hollow
                Vector2 direction = (nearestHollow.transform.position - transform.position);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Rigidbody2D>().AddForce(direction.normalized * succPower, ForceMode2D.Impulse);
                SpawnThenDestroyParticle(grabPS, transform);
                SpawnThenDestroyParticle(succPS, nearestHollow.transform);
            }

            base.Use();
            useCooldown = maxUseCooldown;
        }
        else
        {
            SpawnThenDestroyParticle(rangeFailPS, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(rangeFailSFX, SoundType.majorSFX, 1);
        }

        //Reset for the next selection loop
        nearestHollow = null;
        hollowDiff = 3000f;


    }

    public bool CheckIfLarge()
    {
        return isLarge;
    }

    public bool CheckIfLifeless()
    {
        return isLifeless;
    }

    private void CrabSpawn()
    {
        if(held == false && isActiveAndEnabled == true)
        {
            SpawnThenDestroyParticle(crabSpawnPS, transform);
            GameObject spawnedOrb = Instantiate(crabPrefab, transform.position, transform.rotation);
            spawnedOrb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(5, 10)), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }

}
