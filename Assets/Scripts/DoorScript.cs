using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private bool doorUnlocked = true;
    [SerializeField] private bool openVertically = true;
    [SerializeField] private float doorOpenAngle = 90f; // -90 to rotate in the other direction
    [SerializeField] private float doorCloseAngle = 0f;
    [SerializeField] private float doorMovementSmooth = 2f;

    [SerializeField] private AudioClip doorSound = null;
    [SerializeField] private AudioClip doorLockedSound = null;

    AudioSource _source = null;
    AudioSource Source
    {
        get
        {
            if (_source == null)
            {
                _source = GetComponent<AudioSource>();
                if (_source == null) { _source = gameObject.AddComponent<AudioSource>(); }
                _source.playOnAwake = false;
            }
            return _source;
        }
    }

    private void Update()
    {
        if (doorOpen)
        {
            if (openVertically)
            { 
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, doorOpenAngle);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
            }
        }
        else
        {
            if (openVertically)
            {
                Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, doorCloseAngle);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
            }
        }
    }

    public void ChangeDoorState()
    {
        if (doorUnlocked)
        {
            doorOpen = !doorOpen;
            PlaySFX(doorSound);
        }
        else
            PlaySFX(doorLockedSound);
    }

    public void UnlockDoor()
    {
        doorUnlocked = true;
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}