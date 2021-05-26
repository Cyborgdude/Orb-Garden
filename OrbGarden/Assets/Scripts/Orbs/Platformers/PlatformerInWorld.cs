using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerInWorld : BaseOrbGameScript, IBlast
{
    [SerializeField]
    private GameObject platformerOrb;
    [SerializeField]
    private GameObject crackParticle;
    [SerializeField]
    private GameObject crumbleParticle;
    [SerializeField]
    private float maxTimeAlive = 2;
    private float timeAlive;
    private bool cracked = false;
    [SerializeField]
    private AudioClip crackSound;
    [SerializeField]
    private AudioClip crumbleSound;
    private GameObject speaker;
    [SerializeField]
    private bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = maxTimeAlive;
        speaker = GameObject.FindGameObjectWithTag("Speaker");
    }

    // Update is called once per frame
    void Update()
    {
        if(timeAlive > 0)
        {
            if(cracked == false && timeAlive < maxTimeAlive/2)
            {
                cracked = true;
                SpawnThenDestroyParticle(crackParticle, transform);
                //speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(crackSound, SoundType.minorSFX, 1);
            }

            if(locked == false)
            {
                timeAlive = timeAlive - Time.deltaTime;
            }
        }
        else
        {
            Crumble();
        }
    }

    public void IBlast(AcCol acCol)
    {
        Crumble();
    }

    private void Crumble()
    {
        SpawnThenDestroyParticle(crumbleParticle, transform);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(crumbleSound, SoundType.majorSFX, 1);
        GameObject spawnedOrb = Instantiate(platformerOrb, transform.position,transform.rotation);
        spawnedOrb.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5, 5), Random.Range(25,50)), ForceMode2D.Impulse);
        Destroy(gameObject);
    }
}
