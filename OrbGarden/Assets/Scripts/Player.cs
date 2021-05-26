using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseOrbGameScript
{
    //Movment Vars
    [SerializeField]
    private float baseMovementSpeed = 30f;
    [SerializeField]
    private float maxMovementSpeed = 5f;
    [SerializeField]
    private float movementSpeed;
    
    [SerializeField]
    private float accelAmount = 5f;
    [SerializeField]
    private float dccelAmount = 2f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float horiJumpForce = 5f;
    [SerializeField]
    private float airControlReduction = 2f;
    private bool grounded = false;
    [SerializeField]
    private LayerMask landableMask;
    [SerializeField]
    private GameObject JumpParticle;
    [SerializeField]
    private AudioClip jumpSound;

    private bool facingRight = true;

    [SerializeField]
    private float maxVelocity = 10f;

    //Movement Vars (Slopes)
    [SerializeField]
    private float distanceToSlopeV;
    [SerializeField]
    private float distanceToSlopeH;
    private Vector2 slopePerp;
    private float slopeAngle;

    private bool isOnSlope;
    private float slopeAngleOld;

    //Grab Vars
    [SerializeField]
    private float grabRange = 300;
    [SerializeField]
    private float playerHeight = 1;
    [SerializeField]
    private float throwPower = 10;
    private bool itemHeld = false;
    private GameObject nearestGrabAble;
    private float distanceDiff = 3000;
    [SerializeField]
    private AudioClip grabSound;
    [SerializeField]
    private AudioClip throwSound;

    //Managers
    private GameObject speaker;

    //Misc 
    private Animator animator;
    private GameObject foundAnalysisMachine = null;
    private GameObject foundUpgradeTrigger = null;
    private bool enterEnabled = false;
    private bool controlsLocked = true;
    private float currentEnterTimer;
    private float maxEnterTimer = 5;
    private bool restarting = false;


    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.Load();
        speaker = GameObject.FindGameObjectWithTag("Speaker");
        DontDestroyOnLoad(gameObject);
        nearestGrabAble = null;
        movementSpeed = baseMovementSpeed;
        animator = gameObject.GetComponent<Animator>();


    }

    /* AngleCheck() looks to see if the player is standing on an angle. It then passes that information through to the movement controls. */
    private void AngleCheck()
    {
        CapsuleCollider2D cc = GetComponent<CapsuleCollider2D>();
        Vector2 capsuleColliderSize = cc.size;

        Vector2 checkPosition = transform.position - new Vector3(0.0f, capsuleColliderSize.y / 2);

        //Look for slope
        RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, distanceToSlopeV, landableMask);
        Debug.DrawRay(hit.point, hit.normal, Color.magenta);
        if (hit)
        {

            //Calculate Slopes Angle
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            if(facingRight == true)
            {
                slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            }
            else
            {
                slopeAngle = Vector2.Angle(hit.normal, Vector2.up)*-1;
            }



        }
    }

    /* FacingSlopCheck() looks to see if the player is facing an angle. It then passes that information through to the movement controls.
       It does this to avoid the angle movement assist kicking in when it is not needed. */
    private bool FacingSlopeCheck()
    {
        CapsuleCollider2D cc = GetComponent<CapsuleCollider2D>();
        Vector2 capsuleColliderSize = cc.size;

        Vector2 checkPosition = transform.position - new Vector3(0.0f, capsuleColliderSize.y / 2f);
       
        //Look for slope
        RaycastHit2D hit;

        if (facingRight)
        {
            hit = Physics2D.Raycast(checkPosition, transform.right, distanceToSlopeH, landableMask);
        }
        else
        {
            hit = Physics2D.Raycast(checkPosition, transform.right*-1, distanceToSlopeH, landableMask);
        }

        if (hit)
        {
            //Debug.Log("HIT");
            //Debug.DrawRay(checkPosition, transform.right * distanceToSlopeH, Color.red);
            //Debug.DrawRay(checkPosition, transform.right*-1 * distanceToSlopeH, Color.red);
            return true;

        }
        else
        {
            //Debug.Log("Miss");
            //Debug.DrawRay(checkPosition, transform.right * distanceToSlopeH, Color.cyan);
            //Debug.DrawRay(checkPosition, transform.right*-1 * distanceToSlopeH, Color.cyan);
            return false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (controlsLocked == false)
        {
            grounded = IsGrounded();
            AngleCheck();


            float movementDirection = (Input.GetAxis("Horizontal"));



            float xangle = Mathf.Cos(slopeAngle * Mathf.PI / 180) * (movementDirection * (Time.deltaTime * movementSpeed * 1.7f));
            float yangle = Mathf.Sin(slopeAngle * Mathf.PI / 180) * (movementDirection * (Time.deltaTime * movementSpeed * 1.7f));

            if (grounded == true)
            {
                if (FacingSlopeCheck() == true)
                {
                    //Custom Left/Right movement. Will move in the direction a slop is facing. Will cause jumping when going down slopes
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xangle, yangle), ForceMode2D.Impulse);
                    Debug.DrawRay(transform.position, new Vector2(xangle, yangle), Color.yellow);
                }
                else
                {

                    // V is a solid direction left OR right
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(movementDirection * (Time.deltaTime * movementSpeed), 0f), ForceMode2D.Impulse);

                }

            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(movementDirection * (Time.deltaTime * (movementSpeed / airControlReduction)), 0f), ForceMode2D.Impulse);

            }




            if (movementDirection > 0)
            {
                facingRight = true;
            }
            else if (movementDirection < 0)
            {
                facingRight = false;
            }

            //Jump
            Jump();

            //Accelerate
            Accelerate();

            //Debug.Log(movementSpeed);

            if (Input.GetButtonDown("Grab"))
            {
                // Debug.Log(itemHeld);

                grabObject();
            }

            if (Input.GetButtonDown("Use"))
            {
                Use();
            }

            if (currentEnterTimer <= 0)
            {
                if (Input.GetButtonDown("Enter"))
                {
                    setEnterEnabled(true);
                    currentEnterTimer = maxEnterTimer;
                }
            }
            else
            {
                currentEnterTimer = currentEnterTimer - Time.deltaTime;
                
            }


            if (facingRight == true)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

 

            //Animations
            animator.SetFloat("Speed", Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude));
            animator.SetBool("HoldingItem", itemHeld);


            LimitSpeed();


            if (Input.GetButtonDown("Restart"))
            {
                SaveLoad.Load();
                GameObject SCM = GameObject.FindGameObjectWithTag("SceneChangeManager");
                controlsLocked = true;
                if (Game.Current.GData.redKeyUnlocked == false)
                {
                    SCM.GetComponent<SceneChangeManager>().changeLevel(0, 3, -1);
                }
                else
                {
                    SCM.GetComponent<SceneChangeManager>().changeLevel(Game.Current.GData.levelNumber, Game.Current.GData.doorNumber, 0);
                }

                if (itemHeld == true)
                {
                    grabObject();
                    Destroy(nearestGrabAble);
                }
                Invoke("unlockControls", 2f);
            }

            if (Input.GetButtonDown("Cancel") && restarting == false)
            {
                Application.Quit();
            }
        }
        else
            {
            //Menu Controls
            if (Input.GetButtonDown("Jump") && restarting == false)
            {
                SaveLoad.Load();

                GameObject SCM = GameObject.FindGameObjectWithTag("SceneChangeManager");
                if (Game.Current.GData.redKeyUnlocked == false)
                {
                    SCM.GetComponent<SceneChangeManager>().changeLevel(0, 3, -1);
                }
                else
                {
                    SCM.GetComponent<SceneChangeManager>().changeLevel(Game.Current.GData.levelNumber, Game.Current.GData.doorNumber, 0);
                }
                Invoke("unlockControls", 2f);
                restarting = true;
            }
            if (Input.GetButtonDown("Cancel") && restarting == false)
            {
                Application.Quit();
            }
            //end of menu controls
        }



        


    }

    private void LimitSpeed()
    {
        Vector2 nudge = Vector2.zero;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb.velocity.x > maxVelocity)
        {
            nudge.x = nudge.x -1;
        }
        if(rb.velocity.x < -maxVelocity)
        {
            nudge.x = nudge.x    + 1;
        }
        rb.velocity = rb.velocity + nudge;
    }


    private void Jump()
    {

        if (Input.GetButtonDown("Jump") && grounded == true)
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * Input.GetAxis("Horizontal")) * horiJumpForce, jumpForce), ForceMode2D.Impulse);

            SpawnThenDestroyParticle(JumpParticle, transform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(jumpSound, SoundType.minorSFX, 0.1f);

            //movementSpeed = baseMovementSpeed;
        }
    }

    private void Accelerate()
    {
        float currentTravelSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (currentTravelSpeed > 8)
        {
            movementSpeed = movementSpeed + (Time.deltaTime * accelAmount);

        }
        else if (movementSpeed > baseMovementSpeed)
        {
            
            movementSpeed = movementSpeed - (Time.deltaTime * dccelAmount); 

        }
        else
        {
            movementSpeed = baseMovementSpeed;
        }

        if(movementSpeed > maxMovementSpeed)
        {
            movementSpeed = maxMovementSpeed;
        }

    }

    private bool IsGrounded()
    {
        float heightBonus = 0.1f;
        Collider2D col = gameObject.GetComponent<CapsuleCollider2D>();
        RaycastHit2D boxcastHit = Physics2D.BoxCast(col.bounds.center, col.bounds.size - new Vector3(0.1f,0,0), 0f, Vector2.down, heightBonus, landableMask);

        //Debug
        Color castColor;
        if (boxcastHit.collider != null)
        {
            castColor = Color.green;
        }
        else
        {
            castColor = Color.red;
        }

       // Debug.DrawRay(col.bounds.center + new Vector3(col.bounds.extents.x, 0), Vector2.down * (col.bounds.extents.y + heightBonus), castColor);
       // Debug.DrawRay(col.bounds.center - new Vector3(col.bounds.extents.x, 0), Vector2.down * (col.bounds.extents.y + heightBonus), castColor);
       // Debug.DrawRay(col.bounds.center - new Vector3(0, col.bounds.extents.y), Vector2.right * (col.bounds.extents.x), castColor);
        return boxcastHit.collider != null;
    }


    private void grabObject()
    {


        if (itemHeld == false)
        {
            //Get Closest Orb In Range


            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, grabRange);
            foreach (Collider2D nearbyObject in nearbyObjects)
            {
                if (nearbyObject.gameObject.tag == "Orb")
                {
                    if (distanceDiff >
                            Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                            new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y))
                    )
                    {
                        distanceDiff =
                            Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                            new Vector2(nearbyObject.gameObject.transform.position.x, nearbyObject.gameObject.transform.position.y));
                        nearestGrabAble = nearbyObject.gameObject;
                    }

                }
            }
            //nearbyObjects = null;

            if (nearestGrabAble != null)
            {
                //Attatch Object
                nearestGrabAble.GetComponent<BasicOrb>().grabTrigger();
                nearestGrabAble.GetComponent<Rigidbody2D>().angularVelocity = 0;
                nearestGrabAble.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                nearestGrabAble.GetComponent<Rigidbody2D>().isKinematic = true;
                nearestGrabAble.transform.parent = gameObject.transform;
                nearestGrabAble.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + playerHeight, nearestGrabAble.transform.position.z);
                nearestGrabAble.transform.rotation = new Quaternion(0, 0, 0, 0);

                if(nearestGrabAble.GetComponent<CircleCollider2D>() != null)
                {
                    nearestGrabAble.GetComponent<CircleCollider2D>().enabled = false;
                }
                else if(nearestGrabAble.GetComponent<BoxCollider2D>() != null)
                {
                    nearestGrabAble.GetComponent<BoxCollider2D>().enabled = false;
                }



                //InternalLogic
                itemHeld = true;
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(grabSound, SoundType.majorSFX, 0.2f);

            }
        }
        else
        {
            if (nearestGrabAble != null)
            {
                //De-attatch Object
                nearestGrabAble.GetComponent<BasicOrb>().grabTrigger();
                nearestGrabAble.transform.parent = null;
                nearestGrabAble.GetComponent<Rigidbody2D>().isKinematic = false;
                nearestGrabAble.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + throwPower);
                if (nearestGrabAble.GetComponent<CircleCollider2D>() != null)
                {
                    nearestGrabAble.GetComponent<CircleCollider2D>().enabled = true;
                }
                else if (nearestGrabAble.GetComponent<BoxCollider2D>() != null)
                {
                    nearestGrabAble.GetComponent<BoxCollider2D>().enabled = true;
                }


                //InternalLogic
                nearestGrabAble = null;
                distanceDiff = 3000;
                itemHeld = false;
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(throwSound, SoundType.majorSFX, 0.2f);

            }
        }
    }

    private void Use()
    {
        //Check if Analysis Machine is nearby
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, grabRange);

        foreach (Collider2D nearbyObject in nearbyObjects)
        {
            if (nearbyObject.gameObject.tag == "AnalysisMachine")
            {
                //Debug.Log("PlayerScript Spotted Machine");
                foundAnalysisMachine = nearbyObject.gameObject;
                if (foundAnalysisMachine != null && nearestGrabAble != null)
                {
                    foundAnalysisMachine.GetComponent<AnalysisMachine>().processOrb(nearestGrabAble.GetComponent<BasicOrb>().GetOrbType(), nearestGrabAble.transform);
                }
            }
            else if (nearbyObject.gameObject.tag == "UpgradeTrigger")
            {

                foundUpgradeTrigger = nearbyObject.gameObject;
                foundUpgradeTrigger.GetComponent<IPurchase>().IPurchase();
            }

        }

        if (itemHeld == true && foundAnalysisMachine == null && foundUpgradeTrigger == null)
        {
            //No Machine, No Upgrade Trigger, Use Item
            nearestGrabAble.GetComponent<BasicOrb>().Use();
        }

        foundUpgradeTrigger = null;
        foundAnalysisMachine = null;
    }

    public void ChangeHeldState(bool newState)
    {
        if(newState == false)
        {
            itemHeld = false;
            nearestGrabAble = null;
            distanceDiff = 3000f;
        }
    }

    public GameObject GetHeldObject()
    {
        return nearestGrabAble;
    }


    public int GetFaceDirection()
    {
        if(facingRight == true)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public bool getEnterEnabled()
    {
        return enterEnabled;
    }

    public void setEnterEnabled(bool enabled = false)
    {
        enterEnabled = enabled;
    }

    public void unlockControls()
    {
        controlsLocked = false;
    }
}
