using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag("Door")) //Door Mesh has to have the tag, because it has the collider
                {
                    hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
                }
            }
        }
    }
}