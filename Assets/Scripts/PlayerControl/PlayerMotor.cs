using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the player's movement and camera rotation using a CharacterController.
public class PlayerMotor : MonoBehaviour
{
    public Camera cam;
    public Transform respawnLocation;

    public float speed = 6f;
    public float jumpSpeed = 6f;
    public float rotSpeed = 6f;
    public bool canMove = true;

    private CharacterController controller;
    private float upVelocity = 0f;

    private float yaw;
    private float pitch;

    [HideInInspector] public bool isDead = false;

    void Start()
    {
        Vector3 startAngle = cam.transform.eulerAngles;
        yaw = startAngle.y;
        float startPitch = startAngle.x;
        if (startPitch > 90f) {
            startPitch -= 360f;
        }
        pitch = startPitch;
        controller = GetComponent<CharacterController>();
        MouseLocker.LockMouse();
    }

    void Update()
    {
        if (canMove) {
            MovePlayer();
            RotatePlayer();
        }
        FixOutOfBounds();
    }

    // Prevent movement.
    public void Freeze()
    {
        canMove = false;
    }
    
    // Allow movement.
    public void Unfreeze()
    {
        canMove = true;
    }

    void MovePlayer()
    {
        // Get horizontal/vertical axis input
        Vector3 moveDir = Vector3.zero;
        moveDir.x = speed * Input.GetAxis("Horizontal");
        moveDir.z = speed * Input.GetAxis("Vertical");

        // Calculate jumping/gravity
        if (controller.isGrounded) {
            if (Input.GetButton("Jump")) {
                upVelocity = jumpSpeed;
            }
            else {
                upVelocity = 0f;
            }
        }
        else {
            upVelocity += Physics.gravity.y * Time.deltaTime;
        }

        moveDir.y = upVelocity;

        // Apply velocity
        controller.Move(Time.deltaTime * transform.TransformDirection(moveDir));
    }

    void RotatePlayer()
    {
        // Rotate camera with mouse
        yaw += Input.GetAxis("Mouse X") * rotSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotSpeed;
        pitch = Mathf.Clamp(pitch, -90, 90);

        transform.eulerAngles = new Vector3(0, yaw, 0);
        cam.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        // Toggle mouse
        //if (Input.GetKeyDown("return")) {
        //    MouseLocker.ToggleMouse();
        //}
    }

    void FixOutOfBounds()
    {
        if (transform.position.y < -100f) {
            transform.position = respawnLocation.position;
        }
    }
}