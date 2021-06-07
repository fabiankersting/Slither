using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyScript : MonoBehaviour
{
    public GameObject objectToAnimate;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public IEnumerator Teddy()
    {
        audioSource.Play();
        objectToAnimate.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(10);

        audioSource.Stop();
        objectToAnimate.GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            StartCoroutine(Teddy());
        }
    }
}
