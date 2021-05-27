using UnityEngine;
using UnityEngine.SceneManagement;

public class BedScript : MonoBehaviour
{
    private GameManager gameManager = null;

    //[SerializeField] private AudioClip bedSound = null;

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

    public void ChangeScene()
    {
        if (gameManager.GetGeneratorOn() && gameManager.GetSnakeFed())
        {
            gameManager.SetNightState(true);
            SceneManager.LoadScene(1);
            //PlaySFX(bedSound);
        }
        else if (!gameManager.GetSnakeFed())
        {
            Debug.Log("Need to feed the snake first.");
        }
        else if (!gameManager.GetGeneratorOn())
        {
            Debug.Log("Need to get the generator running first.");
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
