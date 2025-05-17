using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : AudioSystem
{
    public float moveSpeed;
    public float jumpForce;
    public Transform orientation;

    public LayerMask groundMask;

    bool isGrounded = true;
    int KeyCount = 0;
    int coinCount = 0;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MyInput();
        CheckGround();
        SpeedControl();
        if (gameObject.transform.position.y <= -15)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        


    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 5f);
    }
    void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlaySound(audio[0]);
            isGrounded = false;
            Jump();
            
        } 
    }

    
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, 0.5f, groundMask);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            KeyCount++;
            Destroy(collision.gameObject);
            Debug.Log("вы подобрали ключ!");
        }
        
        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinCount++;
            KeyCount = 0;
            Debug.Log("вы подобрали монету! у вас: {coinCount} монет!");
            SceneManager.LoadScene("Level2");
        }
        if (collision.gameObject.tag == "Chest" && KeyCount > 0)
        {
            Destroy(collision.gameObject);
            KeyCount--;
            Debug.Log("эй! это было мое!");
        }
        if (collision.gameObject.tag == "Trap")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "KillKey")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "Door" && KeyCount > 0)
        {
            SceneManager.LoadScene("Menu");
            KeyCount = 0;
        }
         

      






    }
       
}
