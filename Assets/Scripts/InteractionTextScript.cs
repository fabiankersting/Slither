using UnityEngine;
using UnityEngine.UI;

public class InteractionTextScript : MonoBehaviour
{
    private float timer = 0;

    [SerializeField] private string interactionText;
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Text textObject;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool displayInfo;
    [SerializeField] private bool allowDisplayInfo = true;

    private void Start()
    {
        textObject.color = Color.clear;
    }

    private void Update()
    {
        FadeText();
        FadeTimer();
    }

    public void ChangeTextState(bool changeState)
    {
        displayInfo = changeState;
    }

    public void ChangeInteractionText(string newText)
    {
        interactionText = newText;
    }

    public void ChangeAllowDisplayInfo()
    {
        allowDisplayInfo = !allowDisplayInfo;
    }

    private void FadeText()
    {
        if (!allowDisplayInfo)
            return;

        if (displayInfo)
        {
            textObject.text = interactionText;
            textObject.color = Color.Lerp(textObject.color, textColor, fadeTime * Time.deltaTime);
        }
        else
        {
            textObject.color = Color.Lerp(textObject.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }

    private void FadeTimer()
    {
        if (!displayInfo)
            return;

        timer += Time.deltaTime;

        if (timer >= fadeTime)
        {
            timer = 0;
            displayInfo = !displayInfo;
        }
    }
}