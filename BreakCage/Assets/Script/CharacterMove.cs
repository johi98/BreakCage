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
    public float jumpPower = 0f;
    public float distToGround= 2f;
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private Transform jumpArm;
    [SerializeField]
    private Transform jumpDir;


    public Animator animator;




    Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
     

    }


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();

        if (isGrounded())
        {
           
                if (Input.GetMouseButton(0))
                {
                jumpPower += 0.01f;

                Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
                Quaternion newRotation = Quaternion.LookRotation(lookForward);

                characterBody.rotation = Quaternion.Slerp(characterBody.transform.rotation, newRotation, 0.1f);
            }
                if (Input.GetMouseButtonUp(0))
                {
                 rb.AddRelativeForce(new Vector3(jumpDir.position.x-characterBody.position.x,jumpPower, jumpDir.position.z - characterBody.position.z) * jumpSpeed, ForceMode.Impulse);

                jumpPower = 0f;

                }

        }
      

    }

    private void Move()
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
                animator.Play("Anim_Leg_Run");
            }
        

    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
        jumpArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

}
