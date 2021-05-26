using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicOrb : BaseOrbGameScript
{

    //AI
    [SerializeField]
    protected bool AIEnabled = true;


    [SerializeField]
    protected float minAITickCountdown = 3;
    [SerializeField]
    protected float maxAITickCountdown = 11;

    protected float currentAITickCountdown;
    protected bool AIActionTaken = false;
    [SerializeField]
    protected float ballTrackRange;
    protected float distanceDiff = 3000f;

    //Physics
    [SerializeField]
    protected float movementForce = 15;

    //Cooldown
    [SerializeField]
    protected float maxUseCooldown = 3;
    protected float useCooldown;
    [SerializeField]
    protected GameObject cooldownFailPS;
    [SerializeField]
    protected GameObject cooldownFinishedPS;
    protected bool cooldownParticlePlayed = false;
    [SerializeField]
    protected AudioClip cooldownFinishedPlink;
    [SerializeField]
    protected AudioClip cooldownFailPlink;


    //Managers
    protected GameObject speaker;

    //FuserLogic
    [SerializeField]
    private OrbType orbType;
    [SerializeField]
    private GameObject precursorOne = null;
    [SerializeField]
    private GameObject precursorTwo = null;

    //Misc
    [SerializeField]
    protected AudioClip collideNoise;
    protected bool held = false;
    protected bool knownTouch = false;

    protected virtual void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");
        currentAITickCountdown = Random.Range(minAITickCountdown, maxAITickCountdown);
        useCooldown = maxUseCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (AIEnabled == true)
        {
            if (currentAITickCountdown <= 0)
            {
                currentAITickCountdown = Random.Range(minAITickCountdown, maxAITickCountdown);
                tickAI();
            }
            else
            {
                currentAITickCountdown = currentAITickCountdown - Time.deltaTime;
            }
        }

        if(useCooldown >= 0)
        {
            useCooldown = useCooldown - Time.deltaTime;
        }
        else if(cooldownParticlePlayed == false)
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetHeldObject() == gameObject)
            {
                SpawnThenDestroyParticle(cooldownFinishedPS, transform);
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(cooldownFinishedPlink, SoundType.majorSFX, 0.5f);
            }
            cooldownParticlePlayed = true;
        }

        //Debug.Log(gameObject.name + " = " + useCooldown);
    }

    protected virtual void tickAI()
    {
        //Child AI HERE

        if(AIActionTaken == false)
        {
            //Check for nearby BALLS
            GameObject nearestball = null;
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, ballTrackRange);
            foreach (Collider2D nearbyObject in nearbyObjects)
            {
                if (nearbyObject.gameObject.tag == "Ball")
                {
                    if (distanceDiff >
                            Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                            new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y))
                    )
                    {
                        distanceDiff =
                            Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                            new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y));
                        nearestball = nearbyObject.gameObject;
                    }

                }
            }
            if (nearestball != null)
            {
                Vector2 direction = (nearestball.transform.position - transform.position);
                gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (movementForce*4), ForceMode2D.Impulse);
                AIActionTaken = true;
                distanceDiff = 3000f;
                nearestball = null;
            }
            else
            {

                MoveDirection(Random.Range(-1, 2));
                AIActionTaken = true;
            }

        }


        AIActionTaken = false;
    }

    protected void MoveDirection(int baseForce)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(baseForce*movementForce,0), ForceMode2D.Impulse);
    }

    public virtual void grabTrigger()
    {
        
        AIEnabled = false;
        held = !held;
        knownTouch = true;
    }

    public virtual void Use()
    {
        cooldownParticlePlayed = false;
    }

    protected virtual void FailUse()
    {
        SpawnThenDestroyParticle(cooldownFailPS, transform);
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(cooldownFailPlink, SoundType.majorSFX, 1);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        /*Too Loud/Annoying
         if (collision.relativeVelocity.magnitude >= 8)
        {
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(collideNoise, SoundType.minorSFX, (collision.relativeVelocity.magnitude / 10));
        } */
    }

    public OrbType GetOrbType()
    {
        return orbType;
    }
    public GameObject GetPrecursorOne()
    {
        return precursorOne;
    }
    public GameObject GetPrecursorTwo()
    {
        return precursorTwo;
    }
    public void SetPrecursors(GameObject pre1, GameObject pre2)
    {
        precursorOne = pre1;
        precursorTwo = pre2;
    }

    public bool aloneAndLost()
    {
        if (held == false && knownTouch == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

