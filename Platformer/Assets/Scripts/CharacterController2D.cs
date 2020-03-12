using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
//-////////////////////////////////////////////////////
///
/// CharacterController2D handles the core logic of the player's:
/// -States such as: grounded, immune, air jumps lefts, and facing right.
/// -Properties such as: how many air jumps, jump power, gravity force, movement, and air control
///
/// CharacterController2D is often getting called by other scripts that want to gather/modify information from the player(Ex: PlayerMovement)
public class CharacterController2D : MonoBehaviour
{

    public GameObject basicBullet, fireBullet, iceBullet, upBasicBullet, upFireBullet, upIceBullet;
    public int whichWeapon = 1;
    public Transform firePoint;
    public Transform UpFirePoint;
    public string iceWeaponScene;
    public string iceWeaponScene2;
    public string fireWeaponScene;
    public string fireWeaponScene2;
    public bool hasFire = false;
    public bool hasIce = false;
    SpriteRenderer m_SpriteRenderer;

    public float basicFireRate = 0.5F;
    private float basicNextFire = 0.0F;

    public float iceFireRate = 0.5F;
    private float iceNextFire = 0.0F;

    public float fireFireRate = 0.5F;
    private float fireNextFire = 0.0F;


    [SerializeField]
    private float m_JumpForce = 800f;
    [SerializeField]
    private int m_AirJumps = 0;
    [SerializeField]
    private float m_FallGravity = 4f;
    [SerializeField, Range(0, 1.0f)]
    private float m_SlowFall = 0.5f;
    [SerializeField, Range(0, .3f)]
    private float m_MovementSmoothing = .05f;
    [SerializeField]
    private LayerMask m_GroundLayer;
    [SerializeField]
    private Transform m_GroundCheck;
    [SerializeField]
    private bool m_AirControl = false;
    [SerializeField]
    private float m_JumpForceOnEnemies = 20;
    private bool m_Grounded;
    private bool m_FacingRight = true;
    private bool m_Damaged;
    public bool m_Immune = false;
    private int m_AirJumpsLeft;
    private Vector3 m_Velocity = Vector3.zero;

    [HideInInspector] public Rigidbody2D m_RigidBody2D;
    //private Animator animator; //If using animations

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == fireWeaponScene || sceneName == fireWeaponScene2)
        {
            hasFire = true;
            hasIce = true;
        }
        if (sceneName == iceWeaponScene || sceneName == iceWeaponScene2)
        {
            hasIce = true;
        }
    }
    //-////////////////////////////////////////////////////
    ///
    void Awake()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        //animator = GetComponent<Animator>(); //get animator component
    }

    //-////////////////////////////////////////////////////
    ///
    void FixedUpdate()
    {
        m_Grounded = Physics2D.Linecast(transform.position, m_GroundCheck.position, m_GroundLayer);
        if (m_Grounded)
        {
            m_AirJumpsLeft = m_AirJumps;
            OnLandEvent.Invoke();
        }
            
    }

    //-////////////////////////////////////////////////////
    ///
    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        if (Input.GetButton("Fire2"))
        {
            ShootUp();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(hasFire)
            {
                if (whichWeapon == 1)
                    whichWeapon = 3;
                else
                    whichWeapon--;
            }
            else if(hasIce)
            {
                if (whichWeapon == 1)
                    whichWeapon = 2;
                else
                    whichWeapon--;
            }   
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (hasFire)
            {
                if (whichWeapon == 3)
                    whichWeapon = 1;
                else
                    whichWeapon++;
            }
            else if (hasIce)
            {
                if (whichWeapon == 2)
                    whichWeapon = 1;
                else
                    whichWeapon++;
            }
        }

        if (whichWeapon == 1)
            m_SpriteRenderer.color = Color.white;
        if (whichWeapon == 2)
            m_SpriteRenderer.color = Color.blue;
        if (whichWeapon == 3)
            m_SpriteRenderer.color = Color.red;

    }

    //-////////////////////////////////////////////////////
    ///
    /// Handles the player movement and their jumping, called in PlayerMovement.cs
    ///
    public void Move(float move, bool jump)
    {

        if (m_Grounded || m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_RigidBody2D.velocity.y);

            m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
                Flip();

            else if (move < 0 && m_FacingRight)
                Flip();

        }

        JumpGravity(jump);

        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(m_RigidBody2D.velocity.x, m_JumpForce));
        }

        //Air Jump
        else if (jump && m_AirJumpsLeft > 0)
        {
            m_Grounded = false;
            m_RigidBody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_AirJumpsLeft--;
        }
    }

    //-////////////////////////////////////////////////////
    ///
    /// Enhances the Jump by adding gravity when falling, short hop, and full hop
    ///
    void JumpGravity(bool jump)
    {
        if (jump && m_AirJumpsLeft >= 1)
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0); //resets gravity if player jumps in the air so we the momentum doesnt kill the jump force

        if (m_RigidBody2D.velocity.y < 0) //we are falling, therefore increase gravity down
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;

        else if (m_RigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))//Tab Jump
            m_RigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (m_FallGravity - 1) * Time.deltaTime;

        if (m_RigidBody2D.velocity.y < 0 && Input.GetButton("Jump"))
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y * m_SlowFall);
    }

    //-////////////////////////////////////////////////////
    ///
    /// Turns around the gameObject attach to this script
    ///
    void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //-////////////////////////////////////////////////////
    ///
    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "hurtbox" && this.gameObject.transform.position.y - collide.gameObject.transform.position.y >= 0)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, m_JumpForceOnEnemies);
        }
    }

    //-////////////////////////////////////////////////////
    ///
    /// Used by other scripts to check Character status
    ///
    public bool IsGrounded()
    {
        return m_Grounded;
    }

    //-////////////////////////////////////////////////////
    ///
    public void SetPlayerImmune(bool isImmune)
    {
        m_Immune = isImmune;
    }

    void Shoot()
    {
        if (whichWeapon == 1 && Time.time > basicNextFire)
        {
            basicNextFire = Time.time + basicFireRate;
            Instantiate(basicBullet, firePoint.position, firePoint.rotation);
        }
            
        else if (whichWeapon == 2 && Time.time > iceNextFire)
        {
            iceNextFire = Time.time + iceFireRate;
            Instantiate(iceBullet, firePoint.position, firePoint.rotation);
        }   
        else if(whichWeapon == 3 && Time.time > fireNextFire)
        {
            fireNextFire = Time.time + fireFireRate;
            Instantiate(fireBullet, firePoint.position, firePoint.rotation);
        }
            
    }
    void ShootUp()
    {
        if (whichWeapon == 1 && Time.time > basicNextFire)
        {
            basicNextFire = Time.time + basicFireRate;
            Instantiate(upBasicBullet, UpFirePoint.position, UpFirePoint.rotation);
        }
        else if (whichWeapon == 2 && Time.time > iceNextFire)
        {
            iceNextFire = Time.time + iceFireRate;
            Instantiate(upIceBullet, UpFirePoint.position, UpFirePoint.rotation);
        }
        else if (whichWeapon == 3 && Time.time > fireNextFire)
        {
            fireNextFire = Time.time + fireFireRate;
            Instantiate(upFireBullet, UpFirePoint.position, UpFirePoint.rotation);
        }
            
    }
    public void changeWeapon()
    {
        if (whichWeapon == 1)
            whichWeapon = 2;
        else if (whichWeapon == 2)
            whichWeapon = 3;
        else
            whichWeapon = 1;
    }

}
