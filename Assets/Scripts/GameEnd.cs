using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private DoorScript ceilingDoor;
    private GameObject flashLight;
    AudioSource rattleSound;

    private void Awake()
    {
        rattleSound = this.GetComponent<AudioSource>();
        flashLight = GameObject.FindGameObjectWithTag("Flashlight");
    }

    IEnumerator waitForSound()
    {
        yield return new WaitForSeconds(3);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            ceilingDoor.ChangeDoorState();
            flashLight.SetActive(false);
            StartCoroutine(waitForSound());
            rattleSound.Play();
        }
    }
}
