using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    [SerializeField] private AudioSource phoneSound = null;

    public void PhoneRings(bool state)
    {
        if (state)
            phoneSound.Play();
        else
            phoneSound.Stop();
    }
}
