using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lock : BaseOrbGameScript, IZap
{
    [SerializeField]
    private AcCol lockColor;
    [SerializeField]
    private GameObject failLockPS;
    [SerializeField]
    private AudioClip failsNoise;
    [SerializeField]
    private AudioClip successNoise;
    [SerializeField]
    private GameObject[] activateables;
    private bool activated = false;

    public void IZap(AcCol acCol)
    {
        if (activated == false)
        {

            if (lockColor == acCol)
            {
                GameObject speaker = GameObject.FindGameObjectWithTag("Speaker");
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(successNoise, SoundType.majorSFX, 2);
                var PSEmission = gameObject.GetComponent<ParticleSystem>().emission;
                PSEmission.enabled = true;
                gameObject.GetComponent<Light2D>().enabled = true;
                foreach (GameObject gameObject in activateables)
                {
                    var activateable = gameObject.GetComponent<IActivate>();
                    if (activateable != null)
                    {
                        activateable.IActivate();
                    }
                }
                activated = true;
            }
            else
            {
                SpawnThenDestroyParticle(failLockPS, transform);
                GameObject speaker = GameObject.FindGameObjectWithTag("Speaker");
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(failsNoise, SoundType.majorSFX, 2);

            }
        }
    }
}
