using UnityEngine;

public class TVScript : MonoBehaviour
{
    private GameManager gameManager = null;
    [SerializeField] private GameObject videoPlane;

    [SerializeField] private GameObject staticImage;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            staticImage.gameObject.SetActive(gameManager.GetTVState());
        }
        videoPlane = GameObject.FindGameObjectWithTag("VideoPlane");
        videoPlane.SetActive(false);
       
    }

    //switches between a static noise image and turned off
    public void ChangeTVState()
    {
        if (gameManager.GetTvSnake())
        {
            videoPlane.SetActive(true);
        }
        else if (gameManager.GetGeneratorOn() && !gameManager.GetLightsOut())
        {
            staticImage.gameObject.SetActive(!staticImage.gameObject.activeSelf);
            gameManager.SetTVState(staticImage.gameObject.activeSelf);
        }
       
    }

 
}