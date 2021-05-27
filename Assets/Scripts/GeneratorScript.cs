using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private bool generatorOn = false;

    [SerializeField] private AudioSource generatorSound = null;

    private void Awake()
    {
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            generatorOn = gameManager.GetGeneratorOn();
            generatorSound.enabled = gameManager.GetGeneratorOn();
        }
    }

    public void ChangeGeneratorState()
    {
        if (!generatorOn && !gameManager.GetLightsOut())
        {
            generatorOn = !generatorOn;
            gameManager.SetGeneratorOn(true);
            generatorSound.enabled = true;
        }
        else if (generatorOn && !gameManager.GetLightsOut())
        {
            Debug.Log("The generator is running.");
        }
        else if (gameManager.GetLightsOut())
        {
            gameManager.SetGeneratorChecked(true);
            Debug.Log("Needs fuel.");
        }
        else
        {
            Debug.Log("A generator.");
        }
    }

    public void TurnOffGenerator()
    {
        generatorOn = !generatorOn;
        generatorSound.enabled = false;
    }
}