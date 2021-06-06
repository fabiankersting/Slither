using System.Collections;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private GameObject doorToUnlock = null;
    [SerializeField] private GameObject keyModel = null;

    [SerializeField] private AudioClip keySound = null;

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
    }

    public void SetDoorUnlocked()
    {
        if (gameManager.GetGeneratorChecked())
        {
            doorToUnlock.GetComponent<DoorScript>().UnlockDoor();
            doorToUnlock.GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();
            PlaySFX(keySound);
            StartCoroutine(DelayedDeactivate(1));
        }
    }

    private IEnumerator DelayedDeactivate(float duration)
    {
        keyModel.SetActive(false);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
