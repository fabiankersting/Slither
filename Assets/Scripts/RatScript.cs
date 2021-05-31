using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    [SerializeField] DoorScript roomDoor;
    AudioSource dyingSound;


    private void RatDies()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            dyingSound = this.GetComponent<AudioSource>();
            roomDoor.ChangeDoorState();
            dyingSound.Play();
            Invoke("RatDies", dyingSound.clip.length);
        }
    }
}
