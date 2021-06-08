using UnityEngine;

public class TVScript : MonoBehaviour
{
    private GameManager gameManager = null;
    private GameObject videoPlane;
    private AudioSource videoPlaneAudioSource;

    [SerializeField] private GameObject ladderRope;
    [SerializeField] private GameObject staticImage;
    [SerializeField] private GameObject TVSoundTrigger;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            staticImage.gameObject.SetActive(gameManager.GetTVState());
        }

        videoPlane = GameObject.FindGameObjectWithTag("VideoPlane");
        videoPlaneAudioSource = videoPlane.GetComponent<AudioSource>();
        videoPlane.SetActive(false);
    }

    //switches between a static noise image and turned off
    public void ChangeTVState()
    {
        if (gameManager.GetTvSnake())
        {
            videoPlane.SetActive(true);
            ladderRope.SetActive(true);
            TVSoundTrigger.SetActive(true);
        }
        else if (gameManager.GetGeneratorOn() && !gameManager.GetLightsOut())
        {
            staticImage.gameObject.SetActive(!staticImage.gameObject.activeSelf);
            gameManager.SetTVState(staticImage.gameObject.activeSelf);
        }
       
    }

    public void ChangeSoundState()
    {
        videoPlaneAudioSource.enabled = !videoPlaneAudioSource.enabled;
    }
}