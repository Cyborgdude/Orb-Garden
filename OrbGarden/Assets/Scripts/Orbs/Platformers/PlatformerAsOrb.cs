using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerAsOrb : BasicOrb
{
    [SerializeField]
    private GameObject platformerPlatform;
    [SerializeField]
    private GameObject spawnParticle;
    [SerializeField]
    private AudioClip placeSound;



    public override void Use()
    {

        //base.Use();
        Instantiate(platformerPlatform, transform.position, new Quaternion(0,0,0,0));
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ChangeHeldState(false);
        SpawnThenDestroyParticle(spawnParticle, transform);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(placeSound, SoundType.minorSFX, 1);
        Destroy(gameObject);
    }
}
