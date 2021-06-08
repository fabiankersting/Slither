using UnityEngine;

public class SnakeFeedingScript : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField] private SnakeHead SnakeHead;
    [SerializeField] private GameObject Player = null;
    [SerializeField] private GameObject Bed = null;
    [SerializeField] private GameObject Rat;

    [SerializeField] private AudioClip feedingSound = null;

    AudioSource _source = null;
    AudioSource Source
    {
        get
        {
            if (_source == null)
            {
                _source = GetComponent<AudioSource>();
                if (_source == null) { _source = gameObject.AddComponent<AudioSource>(); }
                _source.playOnAwake = false;
            }
            return _source;
        }
    }

    private void Awake()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    public void FeedSnake()
    {
        if (gameManager.GetFoodPickedUp() && !gameManager.GetSnakeFed() && !gameManager.GetNightState())
        {
            gameManager.SetSnakeFed(true);
            Rat.SetActive(true);
            SnakeHead.ChangeSnakeFollowPlayer(false);

            if (!GetComponent<InteractionTextScript>().GetAllowDisplayInfo())
                GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();

            GetComponent<InteractionTextScript>().ChangeInteractionText("The snake has been fed");

            if (!gameManager.GetGeneratorOn())
            {
                if (!Bed.GetComponent<InteractionTextScript>().GetAllowDisplayInfo())
                    Bed.GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();

                Bed.GetComponent<InteractionTextScript>().ChangeInteractionText("Should get the generator running");

            }
            else
            {
                if (Bed.GetComponent<InteractionTextScript>().GetAllowDisplayInfo())
                    Bed.GetComponent<InteractionTextScript>().ChangeAllowDisplayInfo();

                Player.GetComponent<InteractionTextScript>().ChangeTextState(true);
            }

            PlaySFX(feedingSound);
        }
        else if (gameManager.GetFoodPickedUp() && gameManager.GetSnakeFed() && gameManager.GetNightState())
        {
            GetComponent<InteractionTextScript>().ChangeInteractionText("The snake is gone!");
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) { return; }

        Source.clip = clip;
        Source.Play();
    }
}
