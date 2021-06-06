using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private bool generatorOn = false;

    private TVScript TV;

    [SerializeField] private GameObject fuelGauge = null;
    [SerializeField] private GameObject key = null;
    [SerializeField] private List<InteractionTextScript> lightSwitches = new List<InteractionTextScript>();

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
        TV = GameObject.FindObjectOfType<TVScript>();
    }

    public void ChangeGeneratorState()
    {
        if (!generatorOn && !gameManager.GetLightsOut())
        {
            generatorOn = !generatorOn;
            gameManager.SetGeneratorOn(true);
            generatorSound.enabled = true;
            GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();

            foreach (var interactText in lightSwitches)
            {
                interactText.ChangeAllowDisplayInfo();
            }
        }
        else if (gameManager.GetLightsOut())
        {
            gameManager.SetGeneratorChecked(true);
            gameManager.SetTvSnake(true);
            TV.ChangeTVState();
            if (key.GetComponent<InteractionTextScript>().GetAllowDisplayInfo())
                key.GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();
        }
    }

    public void TurnOffGenerator()
    {
        generatorOn = !generatorOn;
        generatorSound.enabled = false;
        fuelGauge.transform.rotation = Quaternion.Euler(0, 0, -42);

        foreach (var interactText in lightSwitches)
        {
            interactText.ChangeAllowDisplayInfo();
        }
    }
}