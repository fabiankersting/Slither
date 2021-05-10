using UnityEngine;

public class LaddderScript : MonoBehaviour
{
	[SerializeField] private Transform PlayerController;
	[SerializeField] private float climbSpeed = 1f;

	private CharacterController FPSInput;
	private bool canClimb = false;

	private void Start()
	{
		FPSInput = GetComponent<CharacterController>();
	}

	private void Update()
	{
		if (canClimb)
		{
			if (Input.GetKey(KeyCode.W))
			{
				PlayerController.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * climbSpeed);
			}
			if (Input.GetKey(KeyCode.S))
			{
				PlayerController.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * climbSpeed);
			}
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Ladder")
		{
			FPSInput.enabled = false;
            canClimb = true;
		}
	}

	private void OnTriggerExit(Collider col) //Doesn't get triggered, dunno why
	{
		if (col.gameObject.tag == "Ladder")
		{
		    FPSInput.enabled = true;
            canClimb = false;
		}
	}
}
