using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : BaseOrbGameScript, IBlast
{
    [SerializeField]
    private GameObject crumbleParticle;
    [SerializeField]
    private AudioClip crumbleSound;
    private GameObject speaker;

    void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");
    }

    public void IBlast(AcCol acCol)
    {
        Crumble();
    }

    private void Crumble()
    {
        SpawnThenDestroyParticle(crumbleParticle, transform);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(crumbleSound, SoundType.majorSFX, 1);
        Destroy(gameObject);
    }
}
