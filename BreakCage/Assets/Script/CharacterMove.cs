using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{


    public Rigidbody playerRb;
    public float speed = 2.0f;
    Vector3 moveDirection;
    public Transform playerTr;
    Rigidbody rb;
    public bool isGrounded = false;
    public int jumpCount = 20;
    public int jumpSpeed = 3;
    public float jumpPower = 0.4f;
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpCount = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 20;

        }

    }


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();

        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(new Vector3(0, jumpPower, 0) * jumpSpeed,ForceMode.Impulse);
                    jumpCount--;

                }
            }
        }
      

    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if(isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            Quaternion newRotation = Quaternion.LookRotation(lookForward);

            characterBody.rotation = Quaternion.Slerp(characterBody.transform.rotation, newRotation, 0.1f);
            //characterBody.forward = lookForward;
            transform.position += moveDir * Time.deltaTime * 5f;
        }

    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }

}
