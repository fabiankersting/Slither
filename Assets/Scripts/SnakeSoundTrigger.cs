using System.Collections;
using UnityEngine;

public class SnakeSoundTrigger : MonoBehaviour
{

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public IEnumerator PlaySound()
    {
        audioSource.Play();

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            StartCoroutine(PlaySound());
        }
    }
}
