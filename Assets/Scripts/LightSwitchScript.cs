using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    [SerializeField] private bool lightOn = false;
    [SerializeField] private List<GameObject> connectedLights = new List<GameObject>();

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

        foreach(var light in connectedLights)
            light.SetActive(lightOn);

        PlaySFX(switchSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}