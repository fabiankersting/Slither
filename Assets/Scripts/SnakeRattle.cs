using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRattle : MonoBehaviour
{
    private AudioSource rattleSound;

    private void Start()
    {
        rattleSound = GetComponentInChildren<AudioSource>() ;
        rattleSound.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            GetComponentInParent<Animator>().speed = 1;
            rattleSound.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            GetComponentInParent<Animator>().speed = 0;
            rattleSound.Stop();
        }
    }
}
