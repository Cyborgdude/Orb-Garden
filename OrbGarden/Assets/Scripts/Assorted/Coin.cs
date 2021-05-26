using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : BaseOrbGameScript
{
    [SerializeField]
    private GameObject collectPS;
    [SerializeField]
    private int value;
    [SerializeField]
    private AudioClip[] collectSFX;


    //Managers
    private GameObject speaker;

    void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            collectCoin();
        }
    }

    private void collectCoin()
    {
        SpawnThenDestroyParticle(collectPS, transform);

        int i = Random.Range(0, collectSFX.Length);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(collectSFX[i], SoundType.minorSFX, 1);

        Game.Current.GData.Coins = Game.Current.GData.Coins + value;
        Destroy(gameObject);
    }
}
