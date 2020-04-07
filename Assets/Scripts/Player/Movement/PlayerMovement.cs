using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float raycastDistance = 0f;
    private bool canDoubleJump;
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private Camera playerCamera = null;
    [SerializeField] private Vector3 rbVelocity = Vector3.zero;
    [SerializeField] private float crouchSlideSpeedMultiplier = 1.25f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool enoughVelocity;
    bool isCDashing = false;

    private Rigidbody rb;
    private CapsuleCollider cc;

    [SerializeField] float aux = 1f;
    [SerializeField] float aux_currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            rb.AddForce(0, jumpForce - rb.velocity.y, 0, ForceMode.Impulse);
            canDoubleJump = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position + new Vector3(0, .1f, 0), groundDistance, groundMask);                  //SIRVE PERO DEBO COLOCARLE CAPA A TODOS LOS OBJETOS EN LOS QUE SE PUEDA SALTAR
    }

    public void Move(Vector2 inputMovement)
    {
        float hAxis = inputMovement.x;
        float vAxis = inputMovement.y;

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * speedMultiplier;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    public void IsRunning(bool isRunning)
    {
        if (isRunning && CanCrouchSlide())
        {
            speedMultiplier = Mathf.Lerp(speedMultiplier, runSpeed, Time.fixedDeltaTime);
            //playerCamera.fieldOfView = Mathf.Lerp(60, 65, Time.deltaTime);
            if (speedMultiplier > crouchSlideSpeedMultiplier)
            {
                enoughVelocity = true;
            }
        }
        else
        {
            speedMultiplier = Mathf.Lerp(speedMultiplier, 1, Time.fixedDeltaTime / 1.5f);
            //playerCamera.fieldOfView = Mathf.Lerp(65, 60, Time.deltaTime);
            enoughVelocity = false;
        }
    }

    public void Crouch()
    {
        cc.height = 0.9f;
        if (IsGrounded())
        {
            if (enoughVelocity && !isCDashing)
            {
                speedMultiplier = 3f;
                StartCoroutine(IsCDashing());

                speedMultiplier = Mathf.Lerp(speedMultiplier, 0.5f, Time.fixedDeltaTime);
            }
            else
            {
                speedMultiplier = Mathf.Lerp(speedMultiplier, 0.5f, Time.fixedDeltaTime);
            }
        }
        
    }
    public bool CanCrouchSlide()
    {
        if (speedMultiplier > 1.22f)
        {
            enoughVelocity = true;
        }
        else
        {
            enoughVelocity = false;
        }
        return enoughVelocity;
    }
    public IEnumerator IsCDashing()
    {
            isCDashing = true;
            yield return new WaitForSeconds(2f);
            isCDashing = false;
    }

    private void OnDrawGizmos()             //TEMPORAL PARA VER CUANDO ESTÁ ACTIVO EL IsGrounded
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position + new Vector3(0, .1f, 0), groundDistance);
        //Gizmos.DrawRay(transform.position + new Vector3 (0,.1f,0), Vector3.down);   
    }

    public void ResetHeight() => cc.height = Mathf.Lerp(cc.height, 1.8f, Time.deltaTime * 10);

    public void ResetSpeedMultiplier() => speedMultiplier = Mathf.Lerp(speedMultiplier, 1f, Time.deltaTime);

}
