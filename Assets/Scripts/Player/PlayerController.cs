using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player_Controller : MonoBehaviour
{
    //Testmode Toggle
    public bool TestMode = true;
    
    // Component
     public Rigidbody2D rb;
     public SpriteRenderer sr;
     Animator anim;

    // Inspector balance variables 
    [SerializeField]  float speed = 7.0f;
    [SerializeField]  int jumpForce = 10;

    // Ground check
    [SerializeField] bool isGrounded;
    [SerializeField] Transform GroundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask isGroundLayer;
   



    // Start is called before the first frame update
    void Start()
    {
         rb =  GetComponent<Rigidbody2D>();
         sr = GetComponent<SpriteRenderer>();
         anim = GetComponent<Animator>();


        if (GroundCheck == null)
        {
            //GroundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;

            // Creates Ground Cehcl object if not assigned 
              CreateGroundCheckObject();
        }
    }

    // Create Ground Check Object. 
    private void CreateGroundCheckObject()
    {
            GameObject obj = new GameObject("GroundCheck");
            obj.transform.SetParent(gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.name = "GroundCheck";
            GroundCheck = obj.transform;
            if (TestMode) Debug.Log("Groundcheck object was created on " + gameObject.name);
    }

    // Update is called once per frame
    private void Update()
    {
        GroundChecked();
        MovementInput();
        JumpInput();
        AttackMeele_Strike();

        
    }

    // Update Player Movement
    void MovementInput()
    {
        float xInput = UnityEngine.Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        Debug.Log(xInput);

        // Animator Input Checking
        anim.SetFloat("Input", Mathf.Abs(xInput));

        //Sprite Fliiping
        if (xInput != 0) sr.flipX = (xInput < 0);

    }

    // Update Player Jump input
    void JumpInput()
    {
          if (UnityEngine.Input.GetButtonDown("Jump") && isGrounded)
           {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

           }
          
    }

    // Attack Input Animation Meele_Strike
    void AttackMeele_Strike()
    {
         if (UnityEngine.Input.GetButtonDown("Fire1") && isGrounded)
           {
            anim.SetTrigger("Meele_Strike");
           }
    }

    // Attack Lauch_Strike
    void AttackLaunch_Strike()
    {
        // Initialize the prefab Energy_Strike
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

        // Set Energy_Strike velocity based on player direction: TODO
        Vector2 direction = sr.flipX ? Vector2.left : Vector2.right;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    // Ground Check
    void GroundChecked()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, isGroundLayer);
        
        anim.SetBool("IsGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("GroundCheck"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Update grounded status when leaving the ground
        if (collision.gameObject.CompareTag("GroundCheck"))
        {
            isGrounded = false;
        }
    }


}
