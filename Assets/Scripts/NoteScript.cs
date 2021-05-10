using UnityEngine;
using UnityEngine.UI;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private Image noteImage;
    [SerializeField] private GameObject noteCloseCollider;
    [SerializeField] private GameObject player;

    [SerializeField] private AudioClip noteSound = null;

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

    public void ChangeNoteVisibility()
    {
        bool noteVisible = noteImage.gameObject.activeSelf;
        noteImage.gameObject.SetActive(!noteVisible);
        PlaySFX(noteSound);
        EnableNoteCloseCollider(!noteVisible);

        //Disable movement
        player.GetComponent<CharacterController>().enabled = noteVisible;

        //Disable mouse movement
        player.GetComponent<PlayerController>().enabled = noteVisible;
    }

    private void EnableNoteCloseCollider(bool state)
    {
        noteCloseCollider.SetActive(state);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
