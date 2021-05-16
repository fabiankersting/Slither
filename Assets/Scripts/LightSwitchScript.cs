using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    [SerializeField] private bool lightOn = false;
    [SerializeField] private GameObject connectedLight = null;

    [SerializeField] private AudioClip switchSound = null;

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

    public void ChangeLightState()
    {
        lightOn = !lightOn;
        connectedLight.SetActive(lightOn);
        PlaySFX(switchSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}