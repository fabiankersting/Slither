using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool doorOpen = false;
    [SerializeField] private float doorOpenAngle = 90f; // -90 to rotate in the other direction
    [SerializeField] private float doorCloseAngle = 0f;
    [SerializeField] private float doorMovementSmooth = 2f;

    [SerializeField] private AudioClip doorSound = null;

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
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, doorMovementSmooth * Time.deltaTime);
        }
    }

    public void ChangeDoorState()
    {
        doorOpen = !doorOpen;
        PlaySFX(doorSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}