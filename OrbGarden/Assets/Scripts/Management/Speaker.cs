using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{

    private AudioSource globalSource;

    //Volume Settings
    [SerializeField]
    private float majorSFXMulti = 1;
    [SerializeField]
    private float minorSFXMulti = 1;
    [SerializeField]
    private float voiceMulti = 1;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        globalSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundFromSpeaker(AudioClip sound, SoundType soundType, float multi)
    {
        switch(soundType)
        {
            case SoundType.majorSFX:
                multi = multi * majorSFXMulti;
                break;
            case SoundType.minorSFX:
                multi = multi * minorSFXMulti;
                break;
            case SoundType.voice:
                multi = multi * voiceMulti;
                break;
        }

        globalSource.pitch = Random.Range(0.9f, 1.1f);
        globalSource.PlayOneShot(sound, multi);
    }
}
