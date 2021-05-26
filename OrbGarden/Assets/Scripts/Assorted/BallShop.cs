using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShop : BaseOrbGameScript, IPurchase
{
    [SerializeField]
    private GameObject ballRef;

    private List<GameObject> balls = new List<GameObject>();

    [SerializeField]
    private int ballCost;

    //Flair
    [SerializeField]
    private GameObject failBuyPS;
    [SerializeField]
    private AudioClip failBuySFX;
    [SerializeField]
    private GameObject upgradePS;
    [SerializeField]
    private AudioClip upgradeSFX;

    //Managers
    private GameObject speaker;

    private void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");
    }

    public void IPurchase()
    {
       
        if (Game.Current.GData.Coins >= ballCost)
        {
            if (balls.Count > 2)
            {
                GameObject oldball = balls[0];
                balls.Remove(oldball);
                Destroy(oldball);
            }
            GameObject spawnedBall = Instantiate(ballRef, transform.position, transform.rotation);
            spawnedBall.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 3), Random.Range(1, 4)), ForceMode2D.Impulse);
            balls.Add(spawnedBall);
            Game.Current.GData.Coins = Game.Current.GData.Coins - ballCost;

            SpawnThenDestroyParticle(upgradePS, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(upgradeSFX, SoundType.majorSFX, 1);

            SaveLoad.Save();
        }
        else
        {
            //Not enough money
            SpawnThenDestroyParticle(failBuyPS, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(failBuySFX, SoundType.majorSFX, 1);
        }
    }
}
