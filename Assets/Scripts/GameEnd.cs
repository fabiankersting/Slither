using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private DoorScript ceilingDoor;
    [SerializeField] private CharacterController controller;

    private GameObject flashLight;
    private AudioSource rattleSound;

    private void Awake()
    {
        rattleSound = GetComponent<AudioSource>();
        flashLight = GameObject.FindGameObjectWithTag("Flashlight");
    }

    IEnumerator waitForSound(int duration)
    {
        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            ceilingDoor.ChangeDoorState();
            flashLight.SetActive(false);
            controller.enabled = false;
            rattleSound.Play();
            StartCoroutine(waitForSound(5));
        }
    }
}
