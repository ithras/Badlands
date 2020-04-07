using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GrapplingHookMP : NetworkBehaviour
{
	public Transform shootingPoint;
	public GameObject line;
	public float speed;
	private float momentum;
	private List<GameObject> ropeLine = new List<GameObject>();
	private float step;
	private Rigidbody rb;
	private bool attached = false;
	private RaycastHit hit;
	private PlayerControllerRBMP playerController;

	void Start()
	{
		shootingPoint.transform.parent = Camera.main.transform;
		rb = GetComponent<Rigidbody>();
		playerController = GetComponent<PlayerControllerRBMP>();
	}

	void Update()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetButtonDown("Fire2"))
		{
			CreateRopeLine();

			if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit))
			{
				attached = true;
				rb.isKinematic = true;
			}
		}

		if (attached)
		{
			momentum += Time.deltaTime * speed;
			step = momentum * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, shootingPoint.forward + hit.point, step);
		}

		if (Input.GetButtonUp("Fire2"))
		{
			attached = false;
			rb.isKinematic = false;
			
		}

		if (!attached)
		{
			for (int i = ropeLine.Count - 1 ; i >= 0; i--)
				Destroy(ropeLine[i].gameObject);

			ropeLine = new List<GameObject>();

			if(momentum > 0)
			{
				momentum -= Time.deltaTime * speed / 2;
				step = 0;
			}
		}

		if (playerController.IsGrounded() && momentum > 0)
		{
			momentum = 0;
			step = 0;
		}
	}

	public void CreateRopeLine()
	{
		StartCoroutine("CreateRopeLineCoroutine");
	}

	public IEnumerator CreateRopeLineCoroutine()
	{
		float size = 0;
		float distance = 1;
		Vector3 linePosition = shootingPoint.position;

		while (size < distance)
		{
			linePosition = Vector3.MoveTowards(linePosition, hit.point, 1f);
			ropeLine.Add(Instantiate(line, linePosition, shootingPoint.rotation));
			distance = Vector3.Distance(linePosition, hit.point) - size;
			size++;
			yield return null;
		}
	}
}
