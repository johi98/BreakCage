using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{


    public Rigidbody playerRb;
    public float speed = 2.0f;
    public Transform playerTr;
    Rigidbody rb;
    public int jumpSpeed = 3;
    public float jumpPowerY = 0.5f;
    public float jumpPowerX = 1f;
    public float jumpPowerZ = 1f;
    public float distToGround= 2f;
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private Transform jumpArm;
    [SerializeField]
    private Transform jumpDir;
    bool colEnter = false;
    bool isJump = false;

    public Animator animator;




    Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Cursor.visible = false;                     
        Cursor.lockState = CursorLockMode.Locked;



    }


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
      
      

    }

    private void Move()
    {
        if (isGrounded()||colEnter)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            if (isMove)
            {
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

                Vector3 caraForward = new Vector3(moveDir.x, 0f, moveDir.z).normalized;

                Quaternion newRotation = Quaternion.LookRotation(caraForward);

                characterBody.rotation = Quaternion.Slerp(characterBody.transform.rotation, newRotation, 0.1f);

                //characterBody.forward = lookForward;
                transform.position += moveDir * Time.deltaTime * 5f;


                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Anim_Leg_Run"))
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleB") || 
                        animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                    {
                        animator.Play("Anim_Leg_Run");
                    }
                   
                }



            }
            if (!isMove)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("IdleB"))
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
                    {
                        animator.Play("IdleB");
                    }
                    
                }
            }
        }
       

    }

    private void Jump()
    {
       

        if (isGrounded())
        {

            if (Input.GetMouseButton(0))
            {
                FindObjectOfType<AudioManager>().PlaySound("Jump"); // 점프 사운드
                if (jumpPowerY < 7f)
                    jumpPowerY += 0.05f;
                    jumpPowerX += 0.001f;
                    jumpPowerZ += 0.001f;

                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Quaternion newRotation = Quaternion.LookRotation(lookForward);

                characterBody.rotation = Quaternion.Slerp(characterBody.transform.rotation, newRotation, 0.1f);
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                
                isJump = true;
                rb.AddRelativeForce(new Vector3(0,
                                                10f,
                                                0) * jumpSpeed, ForceMode.Impulse);

                jumpPowerY = 0.5f;
                jumpPowerX = 1f;
                jumpPowerZ = 1f;

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
                {
                    animator.Play("Die");
                }

            }

        }

            
        if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<AudioManager>().PlaySound("Jump2"); // 점프2 사운드
            jumpPowerX = 7f;
            jumpPowerZ = 7f;
            if (isJump) 
            {
                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Quaternion newRotation = Quaternion.LookRotation(lookForward);

                characterBody.rotation = Quaternion.Slerp(characterBody.transform.rotation, newRotation, 1f);

                if (!isGrounded())
                {
                    rb.AddRelativeForce(new Vector3((jumpDir.position.x - characterBody.position.x) * jumpPowerX,
                                              0,
                                              (jumpDir.position.z - characterBody.position.z) * jumpPowerZ) * jumpSpeed, ForceMode.Impulse);

                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump_kick"))
                    {
                        animator.Play("Jump_kick");
                    }


                }
                isJump = false;
            }
                
        }
        
        
        
       
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
        jumpArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }
    private void OnCollisionStay(Collision collision)
    {
        colEnter = true;

    }
    private void OnCollisionExit(Collision collision)

    {

        colEnter = false;
    }


    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

 

}
