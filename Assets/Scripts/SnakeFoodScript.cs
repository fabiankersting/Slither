using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFoodScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private bool used = false;

    [SerializeField] private GameObject snake = null;
    [SerializeField] private List<GameObject> foodModel = null;

    [SerializeField] private AudioClip foodSound = null;

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

    public void SetSnakeFeedable()
    {
        if(!used)
        {
            gameManager.SetFoodPickedUp(true);
            snake.GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();
            used = true;
            PlaySFX(foodSound);
            StartCoroutine(DelayedDeactivate(1));
        }
    }

    private IEnumerator DelayedDeactivate(float duration)
    {
        foreach (var part in foodModel)
        {
            part.SetActive(false);
        }

        GetComponent<DisplayText>().DisableTextObjects(); //New
        
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false); //Old
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
