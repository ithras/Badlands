using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkTransform))]
public class PlayerControllerRBMP : NetworkBehaviour
{
	[SerializeField] private float speed = 0f;
	[SerializeField] private float speedMultiplier = 1f;
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private float raycastDistance = 0f;
	private bool canDoubleJump;
	[SerializeField] private float runSpeed = 2f;
	[SerializeField] private Vector3 rbVelocity = Vector3.zero;
	[SerializeField] private float crouchSlideSpeedMultiplier = 1.25f;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	bool enoughVelocity;
	bool isCDashing = false;

	[SerializeField] private AmmoMP ammoManager = null;
	[SerializeField] private ShooterMP shooter = null;
	[SerializeField] private ChatWindowUI chatUI = null;


	private Vector2 inputMovement;
	private bool isRunning = false;
	private bool isCrouching = false;

	private Rigidbody rb;
	private BoxCollider bc;


	private Controls controls;
	private Controls Controls
	{
		get
		{
			if (controls != null)
				return controls;

			return controls = new Controls();
		}
	}

	public override void OnStartAuthority()
	{
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<BoxCollider>();

		enabled = true;

		Controls.Player.Movement.performed += ctx => inputMovement = ctx.ReadValue<Vector2>();
		Controls.Player.Jump.performed += ctx => Jump();
		Controls.Player.Crouch.performed += _ => isCrouching = true;
		Controls.Player.Running.performed += _ => isRunning = true;
		Controls.Player.Fire.performed += _ => shooter.Shoot(ammoManager);
		Controls.Player.Chat.performed += _ => chatUI.chatHandler();
	}

	[ClientCallback]
	private void OnEnable() => Controls.Enable();

	[ClientCallback]
	private void OnDisable() => Controls.Disable();

	void Update()
	{
		bc.size = new Vector3(bc.size.x, Mathf.Lerp(bc.size.y, 1.8f, Time.deltaTime * 10), bc.size.z); 
		speedMultiplier = Mathf.Lerp(speedMultiplier, 1f, Time.deltaTime);
		if (isCrouching)
		{
			Crouch();
		}
	}

	private void FixedUpdate()
	{
		Move();
		IsRunning();
		rbVelocity = rb.velocity;
	}

	private void Jump()
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

	private void Move()
	{
		float hAxis = inputMovement.x;
		float vAxis = inputMovement.y;

		Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * speedMultiplier;

		Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

		rb.MovePosition(newPosition);
	}

	private void IsRunning()
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
			speedMultiplier = Mathf.Lerp(speedMultiplier, 1, Time.fixedDeltaTime/1.5f);
			//playerCamera.fieldOfView = Mathf.Lerp(65, 60, Time.deltaTime);
			enoughVelocity = false;
		}
	}
	private void Crouch()
	{
		bc.size = new Vector3(bc.size.x, 0.9f, bc.size.z);
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

	public bool IsGrounded()
	{
		return Physics.CheckSphere(groundCheck.position + new Vector3(0, .1f, 0), groundDistance, groundMask);                  //SIRVE PERO DEBO COLOCARLE CAPA A TODOS LOS OBJETOS EN LOS QUE SE PUEDA SALTAR
	}

	private bool CanCrouchSlide()
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
	private IEnumerator IsCDashing()
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
	}
