using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //instance variables
    //tweak these for whatever feel you want
    public float walkSpeed = 5f;
    public float sprintingSpeed = 10f;
    public float jumpingSpeed = 3f;
    public float crouchingSpeed = 1f;
    public float jumpForce = 200f;
    public float mouseSens = 15f;
    Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    public float extraGravity = 0.3f;
    
    //don't change these unless you are seriously changing functionality
    private float currentSpeed;
    public float cameraXrotation = 0f;
    Vector3 playerScale = new Vector3(1, 1, 1);

    //references
    private Rigidbody rb;
    public Camera camera;
    public Transform cameraHolder;

    //states
    bool isJumping = false;
    bool isGrounded = true;
    bool isCrouching = false;


    //getting some references, locking the mouse, and setting some defualt values
    void Start() {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkingSpeed;
        cameraHolder.eulerAngles = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

       //handle keyboard and mouse input
       HandleKeys(); 
       LookAtMouse();
       
       //jumping
       if (Input.GetButtonDown("Jump") && isGrounded) {
           Jump();
       }

       //sprinting
       if (Input.GetKey(KeyCode.LeftShift) && !isJumping && !isCrouching) {
            currentSpeed = sprintingSpeed;
       } else if (!isCrouching && !isJumping) {
            currentSpeed = walkingSpeed;
       }

       //crouching (this is toggled)
       if (Input.GetKeyDown(KeyCode.LeftCommand)) {
            if (isCrouching) {
                UnCrouch();   
            } else if(!isCrouching && !isJumping) {
                Crouch();   
            }
       }

       //extra gravity for more realistic jumping
       rb.AddForce(new Vector3(0, -extraGravity, 0), ForceMode.Impulse);       
    }

    //handling user input (wasd) for moving
    void HandleKeys() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        Vector3 forwardVel = transform.forward * currentSpeed * input.z;
        Vector3 horizontalVel = transform.right * currentSpeed * input.x;
        rb.velocity = horizontalVel + forwardVel + new Vector3(0, rb.velocity.y, 0);
    }

    //handling jumping
    void Jump() {
        currentSpeed = jumpingSpeed;
        isGrounded = false;
        isJumping = true;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        if (isCrouching) {
            currentSpeed = crouchingSpeed;
        }
    }

    //toggle crouch
    void Crouch() {
        currentSpeed = crouchingSpeed;
        isCrouching = true;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    //untoggle crouch
    void UnCrouch() {
        currentSpeed = walkingSpeed;
        isCrouching = false;
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    //look at mouse
    void LookAtMouse() {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * Mathf.Rad2Deg;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * Mathf.Rad2Deg;
        
        transform.Rotate(transform.up * mouseX * mouseSens);

        cameraXrotation -= mouseY * mouseSens;
        //change these two values for however much you want to clamp looking up and down.
        cameraXrotation = Mathf.Clamp(cameraXrotation, -61f, 90f);
        cameraHolder.localRotation = Quaternion.Euler(cameraXrotation, 0f, 0f);
    
    }

    //ground check
    //* make sure whatever you want to be the ground in your game matches the tag below called "Ground" or change it to whatever you want
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            currentSpeed = walkingSpeed;
            isJumping = false;
            isGrounded = true;
        }
    }
}
