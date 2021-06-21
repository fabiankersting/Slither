using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzSpawnScript : MonoBehaviour
{
    public GameObject objectToActivate;
    float randomfloat;
    public AudioClip thunder;
    AudioSource audioSource;

   void Start()
   {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Deactivate());
   }

    private IEnumerator Deactivate()
    {
        randomfloat = Random.Range(25, 40);
        yield return new WaitForSeconds(randomfloat);

        audioSource.PlayOneShot(thunder);
        objectToActivate.SetActive(true);

        yield return new WaitForSeconds(7);

        objectToActivate.SetActive(false);

        yield return Deactivate();
  
    }
}
