using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField] private string objectName;
    [SerializeField] private Color textColor = Color.white;
    [SerializeField] private Text textObjectFront;
    [SerializeField] private Text textObjectBack;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool displayInfo;

    private void Start()
    {
        Text[] textObjects = GetComponentsInChildren<Text>();

        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                textObjectFront = textObjects[i];
                textObjectFront.color = Color.clear;
            }
            else if (i == 1)
            {
                textObjectBack = textObjects[i];
                textObjectBack.color = Color.clear;
            }
        }
    }

    private void Update()
    {
        FadeText();
    }

    public void ChangeTextState(bool changeState)
    {
        displayInfo = changeState;
    }

    public void DisableTextObjects()
    {
        textObjectBack.gameObject.SetActive(false);
        textObjectFront.gameObject.SetActive(false);
    }

    private void FadeText()
    {
        if (displayInfo)
        {
            textObjectFront.text = objectName;
            textObjectBack.text = objectName;
            textObjectFront.color = Color.Lerp(textObjectFront.color, textColor, fadeTime * Time.deltaTime);
            textObjectBack.color = Color.Lerp(textObjectBack.color, textColor, fadeTime * Time.deltaTime);
        }
        else
        {
            textObjectFront.color = Color.Lerp(textObjectFront.color, Color.clear, fadeTime * Time.deltaTime);
            textObjectBack.color = Color.Lerp(textObjectBack.color, Color.clear, fadeTime * Time.deltaTime);
        }
    }
}
