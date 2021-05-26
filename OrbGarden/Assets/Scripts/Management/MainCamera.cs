using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainCamera : BaseOrbGameScript
{
    [SerializeField]
    private GameObject Player;

    private float smoothing = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Player = GameObject.FindGameObjectWithTag("Player");
        Vector3 desiredPos = new Vector3(Player.transform.position.x, Player.transform.position.y + 2, -200);
        transform.position = desiredPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = new Vector3(Player.transform.position.x, Player.transform.position.y+2,-200);
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothing);
        transform.position = smoothPos;
    }
}
