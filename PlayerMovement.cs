using UnityEngine;

[AddComponentMenu("Easy Player Movement/Camera Controller")]
public class PlayerMovement : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;
    Vector2 targetDirection;
    Vector2 targetCharacterDirection;

    [Header("Camera Settings")]
    public bool lockCursor;
    public Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(1.5f, 1.5f);
    public string cameraName = "Camera";

    [Space]
    [Header("Movement Settings")]
    //instance variables (tweak these to change the feel of the player controller)
    public float walkingSpeed = 5f;
    public float sprintingSpeed = 10f;
    public float jumpingSpeed = 3f;
    public float crouchingSpeed = 1f;
    public float jumpForce = 10f;
    public bool holdToCrouch = true;
    public bool jumpCrouching = true;
    Vector3 crouchScale = new Vector3(1, 0.5f, 1); //change for how large you want when crouching
    Vector3 standScale = new Vector3(1, 1, 1);
    public float extraGravity = 0.3f;

    [Space]
    [Header("Keyboard Settings")]
    public KeyCode jump = KeyCode.Space;
    public KeyCode sprint = KeyCode.LeftShift;
    public KeyCode crouch = KeyCode.Z;
    public KeyCode lockToggle = KeyCode.Q;

    //references
    private Rigidbody rb;
    private GameObject cam;

    [Space]
    [Header("Debug Info")]
    //states
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool isCrouching = false;
    public float currentSpeed;

    //getting some references, locking the mouse, and setting some defualt values
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = gameObject.transform.Find(cameraName).gameObject;
        currentSpeed = walkingSpeed;

        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;
        // Set target direction for the character body to its inital state.
        targetCharacterDirection = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        cam.transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
        transform.localRotation = yRotation * targetCharacterOrientation;
    }

    void FixedUpdate()
    {
        //Mouse lock toggle
        if (Input.GetKeyDown(lockToggle))
            lockCursor = !lockCursor;

        if (lockCursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        //WSAD movement
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        Vector3 forwardVel = transform.forward * currentSpeed * input.z;
        Vector3 horizontalVel = transform.right * currentSpeed * input.x;
        rb.velocity = horizontalVel + forwardVel + new Vector3(0, rb.velocity.y, 0);

        //Jumping
        if (Input.GetKey(jump) && isGrounded)
            Jump();

        //Sprinting
        if (Input.GetKey(sprint) && !isJumping && !isCrouching)
            currentSpeed = sprintingSpeed;
        else if (!isCrouching && !isJumping)
            currentSpeed = walkingSpeed;

        //Crouching (alot of if)
        if (Input.GetKey(crouch))
        {
            if (isCrouching && !holdToCrouch)
                Crouch(false);
            else if (!isCrouching && jumpCrouching)
                Crouch(true);
            else if (!isCrouching && !isJumping && !jumpCrouching)
                Crouch(true);
        }
        else if (holdToCrouch && isCrouching)
        {
            Crouch(false);
        }

        //Extra gravity for more realistic jumping
        rb.AddForce(new Vector3(0, -extraGravity, 0), ForceMode.Impulse);
    }

    //handling jumping
    void Jump()
    {
        currentSpeed = jumpingSpeed;
        isGrounded = false;
        isJumping = true;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        if (isCrouching)
            currentSpeed = crouchingSpeed;
    }

    //toggle crouch
    void Crouch(bool crouch)
    {
        isCrouching = crouch;
        if (crouch)
        {
            currentSpeed = crouchingSpeed;
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        else
        {
            currentSpeed = walkingSpeed;
            transform.localScale = standScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }

    //ground check
    //* make sure whatever you want to be the ground in your game matches the tag below called "Ground" or change it to whatever you want
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (isCrouching)
                currentSpeed = crouchingSpeed;
            else
                currentSpeed = walkingSpeed;
            isJumping = false;
            isGrounded = true;
        }
    }
}
