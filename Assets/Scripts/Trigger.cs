using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private UnityEvent triggerEvent;
    [SerializeField] private bool nightScene = false;
    [SerializeField] private int triggerID = 0;

    private void Awake()
    {
        if (nightScene)
            gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (nightScene)
            {
                if (triggerID == 3)
                    gameManager.SetSnakeChecked(true);
                else if (triggerID == 4)
                    gameManager.SetLightsOut(true);
                else if (triggerID == 6)
                    Debug.Log("Game is over.");
            }

            triggerEvent.Invoke();
        }
    }
}