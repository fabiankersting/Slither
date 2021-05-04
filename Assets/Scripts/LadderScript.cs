using UnityEngine;

public class LadderScript : MonoBehaviour
{
	[SerializeField] private Transform PlayerController;
	[SerializeField] private float climbSpeed = 3f;

	private CharacterController FPSInput;
	private bool canClimb = false;

	private void Start()
	{
		FPSInput = PlayerController.transform.GetComponent<CharacterController>();
	}

	private void Update()
	{
		if (canClimb)
		{
			if (Input.GetKey(KeyCode.W))
			{
				PlayerController.transform.Translate(new Vector3(0, 1, 0.1f) * Time.deltaTime * climbSpeed);
			}
			if (Input.GetKey(KeyCode.S))
			{
				PlayerController.transform.Translate(new Vector3(0, -1, -0.1f) * Time.deltaTime * climbSpeed);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.layer == 6 || col.gameObject.layer == 7)
		{
			FPSInput.enabled = false;
            canClimb = true;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if (col.gameObject.layer == 7)
		{
		    FPSInput.enabled = true;
            canClimb = false;
		}
	}
}
