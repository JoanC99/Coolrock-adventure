using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private HUDManager hudManager;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpSpeed = 7f;
    private float dirX;
    public float facingDirection = 1;
    private bool isMoving = false;


    private GameObject projectile;
    [SerializeField] private GameObject projectilePrefab;

    //private int lifes;
    public bool isDead;
    public bool isPlayerReady;

    private void Awake()
    {
        facingDirection = 1;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();

        Invoke("InitPlayer", 0.75f);
        gameManager.numVidas = 3;

    }

    public void InitPlayer()
    {
        anim.Play("player_spawn");
        isDead = false;
        isPlayerReady = isDead = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isPlayerReady = true;
        facingDirection = 1;
        dirX = 0;
    }

    void Update()
    {
        UpdateMovement();
        UpdateAnimator();

    }

    void UpdateMovement()
    {
        if (isPlayerReady)
        {
            if (dirX != 0)
                facingDirection = dirX;
            //GetAxis
            if (!isDead)
                rb.linearVelocity = new Vector2(dirX * speed, rb.linearVelocity.y);
 
            /*if ((transform.position.y < -12f) && !isDead){
                KillPlayer();
            }*/
        }
    }
    public void Move(InputAction.CallbackContext callbackContent)
    {
        if (!isDead)
            if (callbackContent.performed)
            {
                isMoving = true;
                dirX = callbackContent.ReadValue<Vector2>().x;
            }
            else
            {
                dirX = 0;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                isMoving = false;
            }
    }

    public void MoveL()
    {
        if (!isDead)
        {
            isMoving = true;
            dirX = -1;
        }
    }

    public void MoveR()
    {
        if (!isDead)
        {
            isMoving = true;
            dirX = 1;
        }
    }
    public void MoveNO()
    {
        if (!isDead)
        {
            isMoving = false;
            dirX = 0;
        }
    }

    public void Jump()
    {
        Debug.Log("JUMP");
        if (IsGrounded())
        {
            Debug.Log("JUMP2");

            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, jumpSpeed);
        }
    }

    public void Attack(InputAction.CallbackContext callbackContent)
    {
        Debug.Log("ATTACK");
        if (callbackContent.performed)
        {
            if (gameManager.items > 0)
            {
                Debug.Log("ATTACK2");
                anim.SetTrigger("Attack");
                Shoot();
                gameManager.items--;
                hudManager.UpdateItems();
            }
        }
    }

    private void Shoot()
    {
        Vector3 pos = new Vector3(this.transform.position.x + (Input.GetAxisRaw("Horizontal") * (this.GetComponent<Renderer>().bounds.size.x / 16)), this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 8, this.transform.position.z);

        projectile = Instantiate(projectilePrefab, pos, this.transform.rotation);
    }

    void UpdateAnimator()
    {
        if (dirX > 0f)
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0f)
        {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (rb.linearVelocity.y > .1f)
        {
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        }
        else if (rb.linearVelocity.y < -.1f)
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", true);
        }
        else
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
    }

    bool IsGrounded()
    {
        //return (rb.velocity.y == 0f)?true:false;
        /*
        if (rb.velocity.y == 0f) 
        {
            return true;
        } else {
            return false;
        }
        */
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void KillPlayer()
    {
        Debug.Log("kill player");
        dirX = 0;
        facingDirection = 1;
        isDead = true;
        isPlayerReady = false;
        gameManager.numVidas -= 1;
        gameManager.items = 0;
        hudManager.UpdateHUD();
        hudManager.UpdateItems();
        anim.SetTrigger("dead");
        rb.bodyType = RigidbodyType2D.Static;

        if (gameManager.numVidas > 0)
        {
            Invoke("RestartLevel", 2f);
        }
        else
        {
            //TEXT GAMEOVER
            Invoke("GameOver", 2f);
        }
    }

    void CompleteLevel()
    {
        gameManager.GetComponent<GameManager>().CompleteLevel();
    }

    void RestartLevel()
    {
        dirX = 0;
        facingDirection = 1;
        gameManager.GetComponent<GameManager>().RestartLevel();
    }

    void GameOver()
    {
        gameManager.GetComponent<GameManager>().GameOver();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Trap") || c.gameObject.CompareTag("Enemy"))
        {
            if (!isDead)
            {
                KillPlayer();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Finish"))
        {
            isPlayerReady = false;
            rb.bodyType = RigidbodyType2D.Static;
            Invoke("CompleteLevel", 2f);
        }
        else if (c.gameObject.CompareTag("Death"))
        {
            KillPlayer();

        }
    }
}