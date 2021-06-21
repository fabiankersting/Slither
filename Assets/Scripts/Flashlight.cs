using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]

public class Flashlight : MonoBehaviour
{
    #region Parameters

    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private KeyCode rechargeKey = KeyCode.E;

    [SerializeField] private bool allowedToUse = true;

    [SerializeField] private bool autoReduce = true;
    [SerializeField] private float reduceSpeed = 1.0f;

    [SerializeField] private bool autoIncrease = false;
    [SerializeField] private float increaseSpeed = 0.5f;

    [SerializeField][Range(0, 1)] private float toggleOnWaitPercentage = 0.05f;

    public const float minFlashlightLife = 0.0f;
    [SerializeField] private float maxFlashlightLife = 10.0f;

    [SerializeField] private float flashlightFollowSpeed = 5.0f;
    [SerializeField] private Quaternion offset = Quaternion.identity;

    #endregion

    #region References

    [SerializeField] private AudioClip onSound = null;
    [SerializeField] private AudioClip offSound = null;
    [SerializeField] private AudioClip rechargeSound = null;

    [SerializeField] new private Camera camera = null;
    [SerializeField] private GameObject flashlight = null;
    private GameManager gameManager = null;

    #endregion

    #region Statistics

    [SerializeField] private float flashlightLife = 0.0f;
    [SerializeField] private bool usingFlashlight = false;
    [SerializeField] private bool outOfCharge = false;

    #endregion

    #region Properties

    private IEnumerator IE_UpdateFlashlightLife = null;

    private Light _light = null;
    Light Light
    {
        get
        {
            if (_light == null)
            {
                _light = GetComponent<Light>();
                if (_light == null) { _light = gameObject.AddComponent<Light>(); }
                _light.type = LightType.Spot;
            }
            return _light;
        }
    }

    private float defaultIntensity = 0.0f;

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

    private float GetLifePercentage
    {
        get
        {
            return flashlightLife / maxFlashlightLife;
        }
    }
    private float GetLightIntensity
    { 
        get
        {
            return defaultIntensity * GetLifePercentage;
        }
    }

    private bool CanRecharge
    {
        get
        {
            return usingFlashlight && (flashlightLife < maxFlashlightLife);
        }
    }
    private bool MoreThanNeededPrecentage
    {
        get
        {
            return GetLifePercentage >= toggleOnWaitPercentage;
        }
    }

    #endregion

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey) && allowedToUse)
        {
            ToggleFlashlight(!usingFlashlight, true);
        }

        if (Input.GetKeyDown(rechargeKey) && CanRecharge)
        {
            Recharge();
        }

        if (usingFlashlight)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, camera.transform.localRotation * offset, flashlightFollowSpeed * Time.deltaTime);
            flashlight.transform.rotation = transform.rotation;
        }
    }

    private void Recharge()
    {
        flashlightLife = maxFlashlightLife;
        Light.intensity = GetLightIntensity;

        UpdateFlashlightState(false);
        UpdateFlashlightLifeProcess();

        PlaySFX(rechargeSound);
    }

    private void ToggleFlashlight(bool state, bool playSound)
    {
        usingFlashlight = state;
        flashlight.SetActive(state);

        state = (outOfCharge && usingFlashlight) ? false : usingFlashlight;
        ToggleObject(state);

        if (playSound)
        {
            PlaySFX(usingFlashlight ? onSound : offSound);
        }

        UpdateFlashlightLifeProcess();
    }

    private void UpdateFlashlightLifeProcess()
    {
        if (IE_UpdateFlashlightLife != null) { StopCoroutine(IE_UpdateFlashlightLife); }

        if (usingFlashlight && !outOfCharge)
        {
            if (autoReduce)
            {
                IE_UpdateFlashlightLife = ReduceCharge();
                StartCoroutine(IE_UpdateFlashlightLife);
            }
            return;
        }

        if (autoIncrease)
        {
            IE_UpdateFlashlightLife = IncreaseCharge();
            StartCoroutine(IE_UpdateFlashlightLife);
        }
    }

    private IEnumerator IncreaseCharge()
    {
        while (flashlightLife < maxFlashlightLife)
        {
            var newValue = flashlightLife + increaseSpeed * Time.deltaTime;
            flashlightLife = Mathf.Clamp(newValue, minFlashlightLife, maxFlashlightLife);
            Light.intensity = GetLightIntensity;

            FlashlightCheck();

            yield return null;
        }
    }

    private void FlashlightCheck()
    {
        if (MoreThanNeededPrecentage && outOfCharge)
        {
            UpdateFlashlightState(false);
            UpdateFlashlightLifeProcess();
        }
    }

    private IEnumerator ReduceCharge()
    {
        while (flashlightLife > 0.0f)
        {
            var newValue = flashlightLife - reduceSpeed * Time.deltaTime;
            flashlightLife = Mathf.Clamp(newValue, minFlashlightLife, maxFlashlightLife);

            Light.intensity = GetLightIntensity;

            yield return null;
        }

        UpdateFlashlightState(true);
        UpdateFlashlightLifeProcess();
    }

    private void UpdateFlashlightState(bool isDead)
    {
        outOfCharge = isDead;

        var state = outOfCharge ? false : usingFlashlight;
        ToggleObject(state);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }

    private void ToggleObject(bool state)
    {
        Light.enabled = state;
    }

    private void Init()
    {
        if (gameManager.GetNightState())
            allowedToUse = false;

        defaultIntensity = Light.intensity;
        flashlightLife = maxFlashlightLife;

        UpdateFlashlightState(false);

        ToggleFlashlight(false, false);

        if (!camera)
        {
            camera = Camera.main;
        }
    }

    public void ActiveFlashlight()
    {
        allowedToUse = true;
        ToggleFlashlight(!usingFlashlight, true);
    }
}