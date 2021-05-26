using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BasicOrb
{
    [SerializeField]
    private float activateRange;
    [SerializeField]
    private GameObject activatePS;
    [SerializeField]
    private AudioClip activateSound;
    [SerializeField]
    private AcCol acCol;



    public override void Use()
    {
        
        if (useCooldown <= 0)
        {
            base.Use();
            //gameObject.GetComponent<Animator>().SetTrigger("Explode");
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(activateSound, SoundType.majorSFX, 1);
            SpawnThenDestroyParticle(activatePS, transform);
            useCooldown = maxUseCooldown;

            
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, activateRange);
            foreach (Collider2D nearbyObject in nearbyObjects)
            {
                var zapable = nearbyObject.GetComponent<IZap>();
                if (zapable != null)
                {
                    zapable.IZap(acCol);
                }
            }
        }
        else
        {
            FailUse();
        }
    }
}
