using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : BasicOrb
{
    [SerializeField]
    private AudioClip launchSound;
    [SerializeField]
    private GameObject launchPS;
    [SerializeField]
    private float sidewaysPower;
    [SerializeField]
    private float upwardPower;

    public override void Use()
    {
        if (useCooldown <= 0)
        {
            base.Use();
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(launchSound, SoundType.majorSFX, 1);
            SpawnThenDestroyParticle(launchPS, transform, false);
            useCooldown = maxUseCooldown;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(sidewaysPower * player.GetComponent<Player>().GetFaceDirection(), upwardPower), ForceMode2D.Impulse);
        }
        else
        {
            FailUse();
        }
    }

}
