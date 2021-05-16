using UnityEngine;
using UnityEngine.SceneManagement;

public class BedScript : MonoBehaviour
{
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

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
        //PlaySFX(bedSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
