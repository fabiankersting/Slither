using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private bool generatorOn = false;

    private TVScript TV;

    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject fuelGauge = null;
    [SerializeField] private GameObject key = null;
    [SerializeField] private GameObject phoneLight = null;
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

        TV = FindObjectOfType<TVScript>();
    }

    public void ChangeGeneratorState()
    {
        if (!generatorOn && !gameManager.GetLightsOut())
        {
            generatorOn = !generatorOn;
            gameManager.SetGeneratorOn(true);
            generatorSound.enabled = true;

            if(gameManager.GetSnakeFed())
                player.GetComponent<InteractionTextScript>().ChangeTextState(true);

            foreach (var interactText in lightSwitches)
            {
                interactText.ChangeAllowDisplayInfo();
            }

            phoneLight.GetComponent<LightSwitchScript>().ChangeLightStateForTrigger();

            if (!GetComponent<InteractionTextScript>().GetAllowDisplayInfo())
                GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();
        }
        else if (gameManager.GetLightsOut())
        {
            gameManager.SetGeneratorChecked(true);
            gameManager.SetTvSnake(true);
            TV.ChangeTVState();
            TV.GetComponent<InteractionTextScript>().ChangeInteractionText("Won't turn off");

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