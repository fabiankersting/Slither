using UnityEngine;

public class TVScript : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private GameObject staticImage;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            staticImage.gameObject.SetActive(gameManager.GetTVState());
        }
    }

    //switches between a static noise image and turned off
    public void ChangeTVState()
    {
        if (gameManager.GetGeneratorOn() && !gameManager.GetLightsOut())
        {
            staticImage.gameObject.SetActive(!staticImage.gameObject.activeSelf);
            gameManager.SetTVState(staticImage.gameObject.activeSelf);
        }
        else
        {
            Debug.Log("No Power.");
        }

        if (gameManager.GetTvSnake())
        {
            Debug.Log("snakeimage");
        }
    }

 
}