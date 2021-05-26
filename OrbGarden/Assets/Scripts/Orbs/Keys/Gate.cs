using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Gate : BaseOrbGameScript, IActivate
{
    [SerializeField]
    private float shockwaveForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IActivate()
    {
        
        //Disable Gate
        var PSEmission = gameObject.GetComponent<ParticleSystem>().emission;
        PSEmission.enabled = false;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Light2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = (collision.transform.position - transform.position);

        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shockwaveForce, ForceMode2D.Impulse);
                break;
            case "Ball":
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shockwaveForce/8, ForceMode2D.Impulse);
                break;
            case "Orb":
                if(collision.gameObject.GetComponent<BasicBlaster>() != null)
                {
                    collision.gameObject.GetComponent<BasicBlaster>().Use();
                }
                else
                {
                    var blastable = collision.gameObject.GetComponent<IBlast>();
                    if (blastable != null)
                    {
                        blastable.IBlast(AcCol.None);
                    }
                }
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * shockwaveForce, ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }
}
