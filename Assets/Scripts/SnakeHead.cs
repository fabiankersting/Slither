using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameObject player = null;

    [SerializeField] private float headMoveSpeed = 6;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        var lookPos = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime*headMoveSpeed);
    }
}
