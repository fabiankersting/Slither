using UnityEngine;

public class SnakeFeedingScript : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private AudioClip feedingSound = null;

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

    public void FeedSnake()
    {
        if (gameManager.GetFoodPickedUp() && !gameManager.GetSnakeFed() && !gameManager.GetNightState())
        {
            gameManager.SetSnakeFed(true);
            PlaySFX(feedingSound);
        }
        else if (gameManager.GetFoodPickedUp() && gameManager.GetSnakeFed() && !gameManager.GetNightState())
        {
            Debug.Log("The snake has been fed.");
        }
        else if (gameManager.GetFoodPickedUp() && gameManager.GetSnakeFed() && gameManager.GetNightState())
        {
            Debug.Log("The snake is gone!");
        }
        else
        {
            Debug.Log("It seems to be watching me.");
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
