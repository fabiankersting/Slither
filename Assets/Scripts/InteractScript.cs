using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] private float interactDistance = 5f;
    private GameObject lastHitGO = null;
    private GameObject lastHitNote = null;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                hit.collider.transform.parent.GetComponent<DisplayText>().ChangeTextState(true);

                if(lastHitGO != null && lastHitGO != hit.collider.gameObject)
                    lastHitGO.transform.parent.GetComponent<DisplayText>().ChangeTextState(false);

                lastHitGO = hit.collider.gameObject;
            }
            else
            {
                if (lastHitGO != null)
                    lastHitGO.transform.parent.GetComponent<DisplayText>().ChangeTextState(false);
            }
        }
        else
        {
            if (lastHitGO != null)
                lastHitGO.transform.parent.GetComponent<DisplayText>().ChangeTextState(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag("Door")) //Door Mesh has to have the tag, because it has the collider
                {
                    hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
                }
                if (hit.collider.CompareTag("Drawer"))
                {
                    hit.collider.transform.parent.GetComponent<DrawerScript>().ChangeDrawerState();
                }
                if (hit.collider.CompareTag("Note"))
                {
                    hit.collider.transform.parent.GetComponent<NoteScript>().ChangeNoteVisibility();
                    lastHitNote = hit.collider.gameObject;
                }
                if (hit.collider.CompareTag("NoteClose"))
                {
                    lastHitNote.transform.parent.GetComponent<NoteScript>().ChangeNoteVisibility();
                }
                if (hit.collider.CompareTag("Key"))
                {
                    hit.collider.transform.parent.GetComponent<KeyScript>().SetDoorUnlocked();
                }
            }
        }
    }
}