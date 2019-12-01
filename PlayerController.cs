using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public Text intro;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    public float walkSpeed = 10.0f;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    Transform cameraT;
    float verticalLookRotation;

    Rigidbody rigidbodyR;

    float jumpForce = 250.0f;
    bool grounded;
    public LayerMask groundedMask;
    private float distToGround;
    bool cursorVisible=false;

    // Use this for initialization
    void Start()
    {
        Invoke("DisableIntro", 2f);
        cameraT = Camera.main.transform;
        rigidbodyR = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        //LockMouse();
    }
    void DisableIntro()
    {
        intro.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        // rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;

        // movement
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        

        if (Physics.Raycast(transform.position,
                    -Vector3.up,
                    distToGround+0.1f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidbodyR.AddForce(transform.up * jumpForce);
               
            }
        }

        if (transform.position.y <= -5)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
            cursorVisible = true;
            

        }


        if(!cursorVisible&& SceneManager.GetActiveScene().name!="GameOver"&& SceneManager.GetActiveScene().name!="YouWin")
        LockMouse();
           
    }

    void FixedUpdate()
    {
        rigidbodyR.MovePosition(rigidbodyR.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        
    }


    void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorVisible = false;
    }
}



