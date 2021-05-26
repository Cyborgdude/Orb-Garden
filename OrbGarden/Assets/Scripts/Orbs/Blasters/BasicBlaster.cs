using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlaster : BasicOrb
{
    [SerializeField]
    private float explodeRange;
    [SerializeField]
    private float explodeForce = 30;
    [SerializeField]
    private GameObject explodePS;
    [SerializeField]
    private AudioClip explodeSound;
    // Start is called before the first frame update

    public override void Use()
    {
        if (useCooldown <= 0)
        {
            base.Use();
            useCooldown = maxUseCooldown;
            Explode();
        }
        else
        {
            FailUse();
        }
    }

    public void Explode()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Explode");
        speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(explodeSound, SoundType.majorSFX, 1);
        SpawnThenDestroyParticle(explodePS, transform);


        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, explodeRange);
        foreach (Collider2D nearbyObject in nearbyObjects)
        {
            var blastable = nearbyObject.GetComponent<IBlast>();
            if (blastable != null)
            {
                blastable.IBlast(AcCol.None);
            }

            if (nearbyObject.gameObject.tag == "Orb" || nearbyObject.gameObject.tag == "Ball")
            {

                Vector2 direction = (nearbyObject.transform.position - transform.position);

                switch (nearbyObject.tag)
                {
                    case "Orb":
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * explodeForce, ForceMode2D.Impulse);
                        break;
                    case "Ball":
                        nearbyObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (explodeForce / 8), ForceMode2D.Impulse);
                        break;
                }

            }
        }
    }


}
