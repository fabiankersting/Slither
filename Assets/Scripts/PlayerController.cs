using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform PlayerCamera = null;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = -13.0f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.03f;
    [SerializeField] private bool lockCursor = true;
    [SerializeField] private AudioClip footstepsGrass = null;
    [SerializeField] private AudioClip footstepsWood = null;

    private GameManager gameManager = null;
    private CharacterController Controller = null;
    private AudioSource audioSource;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;
    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private bool onGrass = true;
    private bool changedFootstepSoundOnSceneChange = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        Controller = GetComponent<CharacterController>();

        if (lockCursor)
        { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

        ChangeFootstepSoundOnce();
    }

    private void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime); //Smoothes mouse movement

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        PlayerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    private void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime); //Smoothes movement

        if (Controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        if (Controller.enabled == true)
            Controller.Move(velocity * Time.deltaTime);

        PlayFootstepSound();
    }

    private void PlayFootstepSound()
    {
        AudioClip clip;

        if (onGrass)
            clip = footstepsGrass;
        else
            clip = footstepsWood;

        if (Controller.velocity.magnitude > 1f && !audioSource.isPlaying)
            audioSource.PlayOneShot(clip);
    }

    private void ChangeFootstepSoundOnce()
    {
        if (gameManager.GetNightState() && !changedFootstepSoundOnSceneChange)
            onGrass = false;
    }

    public void ChangeFootstepSound()
    {
        onGrass = !onGrass;
    }
}