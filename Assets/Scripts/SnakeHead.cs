using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameObject player = null;
    private bool followPlayer = true;

    [SerializeField] private float headMoveSpeed = 6;
    [SerializeField] private GameObject rat = null;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (followPlayer)
        {
            var lookPos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*headMoveSpeed);
        }
        else
        {
            var lookPos = rat.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * headMoveSpeed);
        }
    }

    public void ChangeSnakeFollowPlayer(bool state)
    {
        followPlayer = state;
    }
}
