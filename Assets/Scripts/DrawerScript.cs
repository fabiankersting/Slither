using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    [SerializeField] private bool drawerOpen = false;
    [SerializeField] private float drawerOpenDistance = 2f;
    [SerializeField] private float drawerCloseDistance = 0f;
    [SerializeField] private float drawerMovementSmooth = 2f;

    [SerializeField] private AudioClip drawerSound = null;

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
        if (drawerOpen)
        {
            Vector3 targetTransform = new Vector3(drawerOpenDistance, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetTransform, drawerMovementSmooth * Time.deltaTime);
        }
        else
        {
            Vector3 targetTransform = new Vector3(drawerCloseDistance, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetTransform, drawerMovementSmooth * Time.deltaTime);
        }
    }

    public void ChangeDrawerState()
    {
        drawerOpen = !drawerOpen;
        PlaySFX(drawerSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}