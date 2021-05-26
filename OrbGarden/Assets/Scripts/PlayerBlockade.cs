using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockade : BaseOrbGameScript
{
    [SerializeField]
    private float power = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 direction = (collision.transform.position - transform.position);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (power), ForceMode2D.Impulse);
        }
    }
}
