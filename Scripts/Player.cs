using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public BoxCollider2D col;

    public KeyCode explode = KeyCode.Mouse0;
    public KeyCode interact = KeyCode.Mouse1;

    public float movementSpeed;
    public float smooth = .01f;

    public float throwForce;

    public GameObject satchel;
    public GameObject poof;
    public List<Satchel> satchels = new List<Satchel>();
    public int maxSatchels;
    public int currentSatchels;

    public int currentLevel;

    public GameObject[] feet;
    public GameObject exlamationPoint;
    public GameObject plug;

    public LayerMask groundLayer;

    float inputX;

    public Vector2 velocity;

    public bool finished = false;
    bool grounded;
    bool falling;
    bool spriteSide;

    public float totalTime;

    Animator anim;
    public SoundManager sm;
    SpriteRenderer spriteR;
    public BetterDialogueManager bdm;

    AudioSource audioS;
    [HideInInspector]
    public float multiplierSound = 1;
    [HideInInspector]
    public bool inMenu; 

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
        spriteR = GetComponentInChildren<SpriteRenderer>();
        sm = GetComponent<SoundManager>();
        audioS = GetComponent<AudioSource>();
        if(bdm == null)
        bdm = GameObject.Find("DialogueBox").GetComponent<BetterDialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = checkIfGrounded();
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle" || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "")
        {
            if(!finished )
            GetInput();
        }
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Open" && Input.GetKeyDown(interact))
        {
            Destroy(plug);
            anim.Play("Idle");
        }

        CurrentSpriteSide();

        //currentLevel = Mathf.FloorToInt(transform.position.y + 6) / 12;
        //Camera.main.transform.position = new Vector3(0.5f, currentLevel * 12, -10);
    }

    private void FixedUpdate()
    {
        Vector2 targetVel = rb2d.velocity;
        Vector2 currentVel = rb2d.velocity;

        velocity = Vector2.SmoothDamp(rb2d.velocity, targetVel, ref currentVel, smooth * Time.fixedDeltaTime);


        if (velocity.y < -18)
        {
            falling = true;
            anim.SetBool("Falling", true);
        }
        else
        {
            falling = false;
            anim.SetBool("Falling", false);
        }

        totalTime += Time.fixedDeltaTime;
        rb2d.velocity = velocity;
    }


    public void ToggleExlamation(bool val)
    {
        exlamationPoint.SetActive(val);
    }

    void CurrentSpriteSide()
    {
        bool currentSide = (velocity.x > 0)? false : true;

        if (currentSide != spriteSide && velocity.magnitude > .3f)
        {
            spriteR.flipX = currentSide;
            spriteSide = currentSide;
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(explode) && !bdm.inEvent && !inMenu)
        {
            if (satchels.Count != 0)
            {
                ExplodeBomb();
            }
            else
            { 
                if (currentSatchels < maxSatchels)
                {
                    if (currentSatchels == 2)
                    {
                        ThrowBomb(throwForce, true);
                        PlaySound(sm.poofSound, 1);
                    }
                    else
                    {
                        ThrowBomb(throwForce, false);
                        PlaySound(sm.explosionSound, 1);
                    }
                    ExplodeBomb();
                }
            }
        }
    }

    void ExplodeBomb()
    {
        satchels[0].Explode();
        satchels.RemoveAt(0);
    }

    public void PlaySound(AudioClip clip, float value)
    {
        float finalValue = value * multiplierSound;
        audioS.PlayOneShot(clip, finalValue);
    }

    void ThrowBomb(float throwF, bool isPoof)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 mousePosW = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        mousePosW.z = 0;

        Vector3 direction = mousePosW - transform.position;

       
        GameObject newSatchel = Instantiate((isPoof)? poof : satchel, transform.position + direction.normalized/2.5f, Quaternion.identity);

        Satchel satS = newSatchel.GetComponent<Satchel>();

        satS.player = this;

        satS.rb2d = satS.GetComponent<Rigidbody2D>();

        satS.rb2d.AddForce(direction.normalized * throwF);

        currentSatchels++;
        grounded = false;

        satchels.Add(satS);
    }

    private bool checkIfGrounded()
    {
        for (int i = 0; i < feet.Length; i++)
        {
            Collider2D groundCol = Physics2D.OverlapCircle(feet[i].transform.position, 0.01f, groundLayer);
            if (groundCol != null)
            {
                if (falling)
                {
                    anim.Play("Fell");
                    PlaySound(sm.hurtSound, 1);
                }if (!grounded)
                {
                    Invoke("PlayLand", .05f);
                }
                falling = false;
                Invoke("Refill", .2f);
                return true;
            }
        }
        return false;
    }

    void PlayLand()
    {
        if (grounded)
        {
            PlaySound(sm.landSound,.5f);
        }
    }

    void Refill()
    {
        if (grounded)
        {
            currentSatchels = 0;
        }
    }
}
