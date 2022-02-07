using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cat : MonoBehaviour
{
    // [SerializeField] private Rigidbody2D rigidcat;
    private Rigidbody2D rigidcat;

    public bool hasKey;
    public float maxSpeed;

    public float jump = 5; // the jump force value
    public float move = 100; // move speed (amplitude) for addforce
    public Sprite catL; // these four sprites will be given to the sprite renderer's displayed sprite depending on the walking direction
    public Sprite catR;
    public Sprite catUp;
    public Sprite catDwn;

    public bool isGrounded = true; 
    // [SerializeField] private Animator animator; 

    public enum CatState {Climbing, Walking,
        Jumping
    }
    public CatState currentCatState = CatState.Walking;
    
    private Transform cam;
        private void Awake()
        {
            rigidcat = GetComponent<Rigidbody2D>();
            cam = Camera.main.transform;
        }
	// Update is called once per frame
    private void Update()
    {
        switch (currentCatState)
        {
            case CatState.Climbing:
                Climbing();
                break;
            case CatState.Walking:
                Movement();
                break;
            case CatState.Jumping:
                Movement();
                break;
        }
    }

    void FixedUpdate()
    {
        Vector3 offsetPos = transform.position + Vector3.back;
        Vector3 smoothFollow = Vector3.Lerp(cam.position, offsetPos, .1f);// * cameraSpeedUp);
        cam.transform.position = smoothFollow;
    }
    private void Climbing()
    {
        JumpCheck();

        if (Input.GetKey(KeyCode.RightArrow) && rigidcat.velocity.y < maxSpeed)
        {
            rigidcat.AddForce(Vector2.up * move, ForceMode2D.Force); //adding the force (left) with an amplitude of move (the variable move)
        }
        
        if (Input.GetKey(KeyCode.LeftArrow) && rigidcat.velocity.y > -maxSpeed)
        {
            rigidcat.AddForce(Vector2.down * move, ForceMode2D.Force); //adding the force (left) with an amplitude of move (the variable move)
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            rigidcat.velocity *= .9f;   
        }
    }

    private void Movement()
    {
        // to make sure the Cat stops exactly when we release the button
        if (Input.GetKey(KeyCode.LeftArrow) && rigidcat.velocity.x > -maxSpeed) // detecting left arrow button
        {
            rigidcat.AddForce(Vector2.left * move,
                ForceMode2D.Force); //adding the force (left) with an amplitude of move (the variable move)
            GetComponent<SpriteRenderer>().sprite = catL; // giving the sprite renderer's sprite the left sprite picture
            // GetComponent<SpriteRenderer>().flipX = false; // setting the sprite renderer's flipX value to false to make sure the Cat's sprite is facing left
        }

        if (Input.GetKey(KeyCode.RightArrow) && rigidcat.velocity.x < maxSpeed) //detecting right arrow button
        {
            rigidcat.AddForce(Vector2.right * move,
                ForceMode2D.Force); // adding the force (right) with an amplitude of move (the variable move)
            GetComponent<SpriteRenderer>().sprite = catR; //  giving the sprite renderer's sprite the right sprite picture
            // GetComponent<SpriteRenderer>().flipX=false; // / setting the sprite renderer's flipX value to true to make sure the Cat's sprite is facing right
        }

        if (Input.GetKey(KeyCode.DownArrow)) // detecting down arrow button
        {
            // rigidcat.AddForce(Vector2.down * move, ForceMode2D.Force);
            GetComponent<SpriteRenderer>().sprite = catDwn; // giving the sprite renderer's sprite the down sprite picture
            // GetComponent<SpriteRenderer>().flipX = false;
        }

        JumpCheck();
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // detecting spacebar button
        {
            rigidcat.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }
    }

    public void SwitchState(CatState newState)
    {
        if (currentCatState == newState) return;
        currentCatState = newState;

        switch (newState)
        {
            case CatState.Climbing:
                rigidcat.gravityScale = 0;
                isGrounded = true;
                rigidcat.velocity *= 0.2f;
                break;
            case CatState.Walking:
                rigidcat.gravityScale = 1;
                isGrounded = true;
                break;
            case CatState.Jumping:
                rigidcat.gravityScale = 1;
                isGrounded = false;
                break;
        }
    }

    public CatState GetState()
    {
        return currentCatState;
    }
}
