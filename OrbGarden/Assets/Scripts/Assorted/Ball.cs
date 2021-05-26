using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Sprite ball1Sprite;
    [SerializeField]
    private Sprite ball2Sprite;
    [SerializeField]
    private Sprite ball3Sprite;

    [SerializeField]
    private AudioClip collideNoise;


    private GameObject speaker;
    // Start is called before the first frame update
    void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");

        Sprite newSprite = ball1Sprite;
        switch (Random.Range(1, 4))
        {
            case 1:
                break;
            case 2:
                newSprite = ball2Sprite;
                break;
            case 3:
                newSprite = ball3Sprite;
                break;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*Too Loud/Annoying
         if (collision.relativeVelocity.magnitude >= 8)
        {
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(collideNoise, SoundType.minorSFX, (collision.relativeVelocity.magnitude / 20));
        } */
    }
}
