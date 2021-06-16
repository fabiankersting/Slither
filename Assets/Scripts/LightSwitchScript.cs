using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private bool lightState = false;
    private int i = 0;

    [SerializeField] private bool lightOn = false;
    [SerializeField] private int lightID = 0;
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

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        { 
            lightOn = gameManager.GetLightStateFromArray(lightID);

            foreach (var light in connectedLights)
                light.SetActive(lightOn);
        }
    }

    private void Update()
    {
        if (gameManager.GetLightsOut() && lightOn)
        {
            lightState = !lightState;

            foreach (var light in connectedLights)
                light.SetActive(lightState);

            i++;

            if (i >= 100)
            { 
                lightOn = false;

                foreach (var light in connectedLights)
                    light.SetActive(lightOn);
            }
        }
    }

    public void ChangeLightState()
    {
        if (gameManager == null)
        {
            lightOn = !lightOn;

            foreach (var light in connectedLights)
                light.SetActive(lightOn);

            PlaySFX(switchSound);
        }
        else
        {
            if (gameManager.GetGeneratorOn() && !gameManager.GetLightsOut())
            {
                lightOn = !lightOn;

                foreach (var light in connectedLights)
                    light.SetActive(lightOn);

                gameManager.ChangeLightStateInArray(lightID, lightOn);

                PlaySFX(switchSound);
            }
        }
    }

    public void ChangeLightStateForTrigger()
    {
        lightOn = true;

        foreach (var light in connectedLights)
            light.SetActive(lightOn);

        gameManager.ChangeLightStateInArray(lightID, lightOn);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}