using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatScript : MonoBehaviour
{
    [SerializeField] private DoorScript roomDoor;
    [SerializeField] private SnakeHead SnakeHead;

    AudioSource dyingSound;

    private void RatDies()
    {
        this.gameObject.SetActive(false);
        SnakeHead.ChangeSnakeFollowPlayer(true);
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
